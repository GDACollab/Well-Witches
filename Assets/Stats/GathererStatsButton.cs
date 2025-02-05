using UnityEngine;
using UnityEngine.UI;

public class GathererStatButton : MonoBehaviour
{
    [SerializeField] private GameObject statsPrefab; // Assign Stats UI Prefab in Inspector
    private GameObject spawnedStats;
    private bool isDisplayed = false;

    private void Start()
    {
        if (statsPrefab == null)
        {
            Debug.LogError("Stats Prefab not assigned in GathererStatButton!");
        }

        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(ToggleStatsDisplay);
        }
        else
        {
            Debug.LogError("No Button component found on GathererStatButton!");
        }
    }

    private void ToggleStatsDisplay()
    {
        if (spawnedStats == null)
        {
            spawnedStats = Instantiate(statsPrefab, transform.position, Quaternion.identity);
            Debug.Log("Stats Prefab Instantiated.");
        }
        else
        {
            isDisplayed = !isDisplayed;
            spawnedStats.SetActive(isDisplayed);
            Debug.Log($"Stats Prefab is now {(isDisplayed ? "Visible" : "Hidden")}");
        }

        // ✅ Tell the display script to refresh stats when UI is shown
        if (isDisplayed)
        {
            GathererStatDisplay.Instance.RefreshStats();
        }
    }
}
