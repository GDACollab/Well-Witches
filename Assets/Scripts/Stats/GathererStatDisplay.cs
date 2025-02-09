using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GathererStatDisplay : MonoBehaviour
{
    public static GathererStatDisplay Instance { get; private set; }

    [Header("UI Display")]
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text staminaText;
    [SerializeField] private TMP_Text speedText;

    private void Awake()
    {
        // Singleton Pattern
        if (Instance == null){ Instance = this; }
        else { Destroy(gameObject); }
    }
 
    // Updates text when UI Displayed
    private void OnEnable()
    {
        RefreshStats();
    }

    // Update Stat Display with new values
    public void UpdateStatDisplay(float health, float stamina, float speed)
    {
        if (healthText != null) healthText.text = $"Health: {health}";

        if (staminaText != null) staminaText.text = $"Stamina: {stamina}";

        if (speedText != null) speedText.text = $"Speed: {speed}";

    }

    //Retrieve stat from GathererStatManagement
    public void RefreshStats()
    {
        if (GathererStatManagement.Instance != null)
        {
            UpdateStatDisplay(
                GathererStatManagement.Instance.GetHealth(),
                GathererStatManagement.Instance.GetStamina(),
                GathererStatManagement.Instance.GetSpeed()
            );
        }
        else { Debug.LogError("GathererStatManagement instance not found!"); }
    }
}
