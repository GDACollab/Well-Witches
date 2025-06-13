using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public QuestState activeQuestState;
    public string activeQuestID = null;
    public GameObject activeQuestPrefab;
    public int activeQuestItemCount = 0;

    // used for parcella and quest complete logic
    public bool diedOnLastRun = false;

    public int currentKeyItem = 1;

    private void OnEnable()
    {
        EventManager.instance.questEvents.onQuestStateChange += QuestStateChange;
        EventManager.instance.playerEvents.onPlayerDeath += PlayerDeath;
        SceneManager.activeSceneChanged += SceneChange;
    }

    private void OnDisable()
    {
        EventManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
        EventManager.instance.playerEvents.onPlayerDeath -= PlayerDeath;
        SceneManager.activeSceneChanged -= SceneChange;
    }

    public void PlayerDeath()
    {
        diedOnLastRun = true;
    }

    public void SceneChange(Scene before, Scene after)
    {
        Camera.main.clearFlags = CameraClearFlags.SolidColor;
        Camera.main.backgroundColor = Color.black;
        if (after.buildIndex == 2) // ID for GAMEPLAY scene/ Forest Scene
        {
            diedOnLastRun = false;
        }
    } 

    public void QuestStateChange(Quest quest)
    {
        if(quest.state == QuestState.CAN_FINISH && SceneManager.GetActiveScene().buildIndex == 2)
        {
            activeQuestState = QuestState.CAN_FINISH;
        }
    }

    private void Awake()
    {
        Camera.main.clearFlags = CameraClearFlags.SolidColor;
        Camera.main.backgroundColor = Color.black;
#if UNITY_WEBGL
        Cursor.SetCursor(Resources.Load<Texture2D>("Cursor_Sai"), Vector2.zero, CursorMode.ForceSoftware);
#endif
        if (instance != null) Debug.LogWarning("Found more than one GameManager in the scene. Please make sure there is only one");
        else instance = this;
    }
}
