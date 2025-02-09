using UnityEngine;
using UnityEngine.UI;

public class GathererStatButton : MonoBehaviour
{
    [SerializeField] private GameObject statsPrefab;
    private GameObject spawnedStats;
    private bool isDisplayed = false;

    private void Start()
    {
        //Confirm components assigned
        if (statsPrefab == null) { Debug.LogError("Stats Prefab not found "); }

        //Show stats on click
        Button button = GetComponent<Button>();
        if (button != null) { button.onClick.AddListener(ToggleStatsDisplay); }
        else { Debug.LogError("No Button component found"); }
    }

    private void ToggleStatsDisplay()
    {
        //Instantiate stats prefab 
        if (spawnedStats == null)
        {
            spawnedStats = Instantiate(statsPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            isDisplayed = !isDisplayed;
            spawnedStats.SetActive(isDisplayed);
        }

        // Tell display script to refresh stats when UI  shown
        if (isDisplayed) { GathererStatDisplay.Instance.RefreshStats(); }
    }
}
