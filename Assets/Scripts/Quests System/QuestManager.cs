using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestManager : MonoBehaviour
{
    private Dictionary<string, Quest> questMap;

    // quest requirements
    // TODO - setup so on starting a new run after the first one, this increments
    private int currentRunCount = 0;

    // Quest UI TMP Objects
    // NOTE: MUST DRAG THIS IN FOR THIS TO WORK
    [Header("Quest UI Text")]
    public TextMeshProUGUI questDisplay; // this is set by this script
    public TextMeshProUGUI questDescription; // this is passed onto the instantiated step object

    private bool isQuestAlreadyActive = false;
    private List<string> UnlockedAbilities = new List<string>();

    public static QuestManager Instance { get; private set; }

    private void Awake()
    {
        if (!Instance) Instance = this;
        questMap = CreateQuestMap();
    }

    private void OnEnable()
    {
        EventManager.instance.questEvents.onStartQuest += StartQuest;
        EventManager.instance.questEvents.onAdvanceQuest += AdvanceQuest;
        EventManager.instance.questEvents.onFinishQuest += FinishQuest;
        EventManager.instance.questEvents.onCancelQuest += CancelQuest;
        EventManager.instance.questEvents.onQuestStateChange += UpdateAbilityUnlocks;
        SceneManager.activeSceneChanged += ChangedActiveScene;
    }

    private void OnDisable()
    {
        EventManager.instance.questEvents.onStartQuest -= StartQuest;
        EventManager.instance.questEvents.onAdvanceQuest -= AdvanceQuest;
        EventManager.instance.questEvents.onFinishQuest -= FinishQuest;
        EventManager.instance.questEvents.onCancelQuest -= CancelQuest;
        EventManager.instance.questEvents.onQuestStateChange -= UpdateAbilityUnlocks;
        SceneManager.activeSceneChanged -= ChangedActiveScene;
    }

    private void Start()
    {
        // send out the signal for the initial state of every quest so that other systems can update their info to match
        foreach(Quest quest in questMap.Values)
        {
            EventManager.instance.questEvents.QuestStateChange(quest);
        }
    }

    private bool CheckRequirementsMet(Quest quest)
    {
        bool meetsRequirements = true;

        if(currentRunCount < quest.info.runRequirement || isQuestAlreadyActive)
        {
            meetsRequirements = false;
        }

        foreach(QuestInfo questInfo in quest.info.questPrerequisites)
        {
            if(GetQuestByID(questInfo.id).state != QuestState.FINISHED)
            {
                meetsRequirements = false;
                break;
            }
        }

        return meetsRequirements;
    }

    private void Update()
    {
        // check ALL quests
        foreach(Quest quest in questMap.Values)
        {
            // if requirements are met, update state
            if(quest.state == QuestState.REQUIREMENTS_NOT_MET && CheckRequirementsMet(quest))
            {
                ChangeQuestState(quest.info.id, QuestState.CAN_START);
            }
        }
    }

    private void ChangeQuestState(string id, QuestState state)
    {
        Quest quest = GetQuestByID(id);
        quest.state = state;
        EventManager.instance.questEvents.QuestStateChange(quest);
    }

    private void DisableOtherQuests(Quest activeQuest)
    {
        foreach(Quest quest in questMap.Values)
        {
            if(quest != activeQuest)
            {
                ChangeQuestState(quest.info.id, QuestState.REQUIREMENTS_NOT_MET);
            }
        }
    }

    private void StartQuest(string id)
    {
        if (isQuestAlreadyActive)
        {
            return;
        }
        GameManager.instance.activeQuestState = QuestState.IN_PROGRESS;
        GameManager.instance.activeQuestID = GetQuestByID(id).info.id;
        Quest quest = GetQuestByID(id);
        questDisplay.text = "Quest: " + quest.info.displayName;
        quest.InstantiateCurrentQuestStep(this.transform, questDescription);
        ChangeQuestState(quest.info.id, QuestState.IN_PROGRESS);
        isQuestAlreadyActive = true;
        DisableOtherQuests(quest);
    }

    private void AdvanceQuest(string id)
    {
        Quest quest = GetQuestByID(id);

        // advance to next step
        quest.AdvanceToNextStep();

        // if there are more steps, instantiate the next one
        if (quest.CurrentStepExists())
        {
            quest.InstantiateCurrentQuestStep(this.transform, questDescription);
        }
        // if no more steps, the quest is done!
        else
        {
            ChangeQuestState(quest.info.id, QuestState.CAN_FINISH);
            GameManager.instance.activeQuestState = QuestState.CAN_FINISH;
        }
    }

    private void FinishQuest(string id)
    {
        Quest quest = GetQuestByID(id);
        ResetQuestText();
        ChangeQuestState(quest.info.id, QuestState.FINISHED);
        GameManager.instance.activeQuestState = QuestState.FINISHED;
        GameManager.instance.activeQuestID = null;
        GameManager.instance.activeQuestPrefab = null;
        GameManager.instance.activeQuestItemCount = 0;
        isQuestAlreadyActive = false;
    }

    private void CancelQuest()
    {
        // currently, this cancels all quests that are in progress
        // considering we can only have one quest at a time, this should be fine, however if we choose to not do that, then this needs to be reworked a tad
        foreach(Quest q in questMap.Values)
        {
            if(q.state == QuestState.IN_PROGRESS) // changing this to in progress and/or finished will make it so you can cancel finishable quests too
            {
                ChangeQuestState(q.info.id, QuestState.CAN_START);
                q.Reset();
                ResetQuestText();
            }
        }
        foreach (Transform child in transform) // additionally if we were to change it so we can have multiple quests, this needs to check if the object it found == current quest id
        {
            child.gameObject.GetComponent<QuestStep>().CancelQuestStep();
        }
        GameManager.instance.activeQuestPrefab = null;
        GameManager.instance.activeQuestItemCount = 0;
        isQuestAlreadyActive = false;
        GameManager.instance.activeQuestID = null;
    }

    private void ResetQuestText()
    {
        questDescription.text = "";
        questDisplay.text = "Quest: ";
    }

    private Dictionary<string, Quest> CreateQuestMap()
    {
        // loads all QuestInfo Scriptable Objects from the Assets/Resources/Quests folder
        QuestInfo[] allQuests = Resources.LoadAll<QuestInfo>("Quests");
        Dictionary<string,Quest> idToQuestMap = new Dictionary<string, Quest>();
        foreach(QuestInfo questInfo in allQuests)
        {
            if(idToQuestMap.ContainsKey(questInfo.id))
            {
                Debug.LogWarning("Duplicate ID found when creating quest map. ID: " + questInfo.id);
            }
            idToQuestMap.Add(questInfo.id, new Quest(questInfo));
        }

        return idToQuestMap;
    }

    // Use this method rather than directly accsessing the map just so we can check for invalid IDs
    private Quest GetQuestByID(string id)
    {
        Quest quest = questMap[id];
        if (quest == null)
        {
            Debug.LogWarning("ID not found in the Quest Map: " + id);
        }
        return quest;

    }

    private void UpdateAbilityUnlocks(Quest Q)
    {
        GameObject AbilityUI = GameObject.Find("AbilitySelectionUI");

        if (AbilityUI != null)
        {
            if (Q != null)
            {
                if (Q.state == QuestState.FINISHED)
                {
                    string AbilityToUnlock = "";
                    print("quest: " + Q.info.id);

                    switch (Q.info.id)
                    {
                            case "CollectGarlicQuest":
                                AbilityToUnlock = "HealthTransfer";
                            UnlockedAbilities.Add(AbilityToUnlock);
                            break;
                        default:
                            print("Quest ID not recognized: " + Q.info.id);
                            break;
                    }

                    AbilitySelectIndividualAbilities[] abilityList = AbilityUI.GetComponent<AbilitySelectManager>().abilitiesList;
                    foreach (var ability in abilityList)
                    {
                        if (ability.getAbilityID() == AbilityToUnlock)
                        {
                            ability.setLocked(false);
                            print("set " + ability + "to unlocked");
                        }
                    }
                }
            }
            else
            {
                print("Quest was null");
            }
        }
    }

    private void ChangedActiveScene(Scene current, Scene next)
    {
        if (next.name == "Hub Scene")
        {
            GameObject AbilityUI = GameObject.Find("AbilitySelectionUI");

            AbilitySelectIndividualAbilities[] abilityList = AbilityUI.GetComponent<AbilitySelectManager>().abilitiesList;
            foreach(string UnlockedAbility in UnlockedAbilities)
            {
                foreach (var ability in abilityList)
                {
                    if (ability.getAbilityID() == UnlockedAbility)
                    {
                        ability.setLocked(false);
                        //print("set " + ability + "to unlocked");
                    }
                }
            }
        }
    }
    
    public static List<Quest> GetQuests() 
    {
        if (Instance == null) return new();
        return Instance.questMap.Values.ToList();
    }
}
