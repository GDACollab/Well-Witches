using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public enum PauseState {Settings = 0, Quests = 1, Journal1 = 2, Journal2 = 3, Journal3 = 4};
    
    [Header("Specific Elements")]
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject hubButton;
    [SerializeField] private GameObject leftArrow;
    [SerializeField] private GameObject rightArrow;
    [SerializeField] private TextMeshProUGUI questDisplay;
    [SerializeField] private GameObject questCancelButton;
    
    [Header("UI Groups")]
    [SerializeField] private List<GameObject> pauseWindows = new List<GameObject>();
    [SerializeField] private List<GameObject> itemEntry = new List<GameObject>();
    [SerializeField] private List<TextMeshProUGUI> questNames = new List<TextMeshProUGUI>();
    [SerializeField] private List<TextMeshProUGUI> questStatuses = new List<TextMeshProUGUI>();
    
    private bool _isPaused = false;
    
    private Dictionary<string, bool> _oldStates = new Dictionary<string, bool>();
    
    private PauseState _currentState = PauseState.Settings;
    private PauseState _pageUnlocked = PauseState.Journal1;
    
    private PlayerMovement _gatherer, _warden;
    
    private Controls Controls => WardenAbilityManager.Controls;

    #region Unity Functions
    private void Start()
    {
        pauseScreen.SetActive(false);
        Controls.Ui_Navigate.Enable();
        Controls.Ui_Navigate.Pause.performed += TogglePause;
        
        _oldStates["Init"] = false;
        
        _warden = StatsManager.Instance.players["Warden"].GetComponent<PlayerMovement>();
        _gatherer = StatsManager.Instance.players["Gatherer"].GetComponent<PlayerMovement>();
    }

    void OnDisable()
    {
        Controls.Gameplay_Gatherer.Disable();
        Controls.Gameplay_Warden.Disable();
        Controls.Ui_Navigate.Pause.performed -= TogglePause;
    }
    #endregion
    
    #region Private Functions
    private void SetPaused(bool paused)
    {
        _isPaused = paused;
        Time.timeScale = paused ? 0 : 1;
        pauseScreen.SetActive(paused);
        foreach (GameObject window in pauseWindows) window.SetActive(false);
        if (paused) 
        {
            _oldStates["Init"] = true;
            _oldStates["gathererMove"] = _gatherer.canMove;
            _oldStates["wardenMove"] = _warden.canMove;
            _oldStates["gathererControl"] = GathererAbilityManager.Controls.Gameplay_Gatherer.enabled;
            _oldStates["wardenControl"] = Controls.Gameplay_Warden.enabled;
            
            _gatherer.canMove = false;
            _warden.canMove = false;
            Controls.Gameplay_Warden.Disable();
            GathererAbilityManager.Controls.Gameplay_Gatherer.Disable();
            GathererAbilityManager.Controls.Ui_Navigate.Submit.Disable();
            
            MoveTo(PauseState.Settings);
            Controls.Ui_Navigate.PageLeft.performed += MoveLeft;
            Controls.Ui_Navigate.PageRight.performed += MoveRight;
            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.pauseMenuOpen, GameObject.Find("Gatherer").transform.position);
        }
        else 
        {
            if (_oldStates["Init"])
            {
                _oldStates["Init"] = false;
                _gatherer.canMove = _oldStates["gathererMove"];
                _warden.canMove = _oldStates["wardenMove"];
                if (_oldStates["gathererControl"]) GathererAbilityManager.Controls.Gameplay_Gatherer.Enable();
                if (_oldStates["wardenControl"]) Controls.Gameplay_Warden.Enable();
            }
            else 
            {
                _gatherer.canMove = true;
                _warden.canMove = true;
                Controls.Gameplay_Warden.Enable();
                GathererAbilityManager.Controls.Gameplay_Gatherer.Enable();
            }
            
            GathererAbilityManager.Controls.Ui_Navigate.Submit.Enable();
            Controls.Ui_Navigate.PageLeft.performed -= MoveLeft;
            Controls.Ui_Navigate.PageRight.performed -= MoveRight;
            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.pauseMenuClose, GameObject.Find("Gatherer").transform.position);
        }
        
        UpdateEntries(GameManager.instance.currentKeyItem);
        UpdateQuests();
    }
    
    private void TogglePause(InputAction.CallbackContext context = new())
    {
        SetPaused(!_isPaused);
    }
    
    private void UpdateEntries(int entryNum)
    {
        hubButton.SetActive(SceneManager.GetActiveScene().buildIndex != SceneHandler.Instance.GetHubSceneIndex());
        
        for (int i = 0; i < entryNum - 1 && i < itemEntry.Count; i++)
        {
            itemEntry[i].SetActive(true);
        }
        
        if (entryNum > 9) _pageUnlocked = PauseState.Journal3;
        else if (entryNum > 5) _pageUnlocked = PauseState.Journal2;
        else _pageUnlocked = PauseState.Journal1;
    }
    
    private void UpdateQuests()
    {
        questDisplay.text = QuestManager.Instance.questDisplay;
        
        List<Quest> quests = QuestManager.GetQuests();
        
        for (int i = 0; i < quests.Count; i++)
        {
            Quest quest = quests[i];
            questNames[i].text = quest.info.displayName;
            
            switch (quest.state)
            {
                case QuestState.REQUIREMENTS_NOT_MET:
                    questStatuses[i].text = "Not Started";
                    break;
                case QuestState.CAN_START:
                    questStatuses[i].text = "Available";
                    break;
                case QuestState.IN_PROGRESS:
                    questStatuses[i].text = "In Progress";
                    break;
                case QuestState.FINISHED:
                    questStatuses[i].text = "Finished";
                    break;
                case QuestState.CAN_FINISH:
                    questStatuses[i].text = "Report Back";
                    break;
                default:
                    questStatuses[i].text = "Not Started";
                    break;
            }
        }

        switch (GameManager.instance.activeQuestState)
        {
            case QuestState.IN_PROGRESS:
                questCancelButton.SetActive(true);
                break;
            default:
                questCancelButton.SetActive(false);
                break;
        }
    }

    private void MoveTo(PauseState state)
    {
        pauseWindows[(int)_currentState].SetActive(false);
        _currentState = state;
        pauseWindows[(int)_currentState].SetActive(true);
        
        rightArrow.SetActive(!(_currentState == PauseState.Journal3 || _currentState == _pageUnlocked));
        leftArrow.SetActive(!(_currentState == PauseState.Settings));
    }
    
    private void MoveLeft(InputAction.CallbackContext context) => Move(false);
    private void MoveRight(InputAction.CallbackContext context) => Move(true);
    #endregion
    
    #region Buttons
    public void Move(bool right)
    {
        if (right)
        {
            if (_currentState == PauseState.Journal3 || _currentState == _pageUnlocked) return;
            pauseWindows[(int)_currentState].SetActive(false);
            _currentState++;
            pauseWindows[(int)_currentState].SetActive(true);
        }
        else 
        {
            if (_currentState == PauseState.Settings) return;
            pauseWindows[(int)_currentState].SetActive(false);
            _currentState--;
            pauseWindows[(int)_currentState].SetActive(true);
        }
        
        rightArrow.SetActive(!(_currentState == PauseState.Journal3 || _currentState == _pageUnlocked));
        leftArrow.SetActive(!(_currentState == PauseState.Settings));
    }
    
    public void MoveTo(int state)
    {
        MoveTo((PauseState)state);
    }
    
    public void CancelQuest()
    {
        EventManager.instance.questEvents.CancelQuest();
        UpdateQuests();
    }
    
    public void Resume() => SetPaused(false);
    
    public void ToHub()
    {
        SetPaused(false);
        SceneHandler.Instance.ToHubScene();
    }
    
    public void MainMenu()
    {
        SetPaused(false);
        SceneHandler.Instance.ToMainMenuScene();
    }
    #endregion
}
