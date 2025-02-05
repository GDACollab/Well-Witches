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
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable() // ✅ Updates text whenever the UI is displayed
    {
        RefreshStats();
    }

    public void UpdateStatDisplay(float health, float stamina, float speed)
    {
        if (healthText != null)
            healthText.text = $"Health: {health}";

        if (staminaText != null)
            staminaText.text = $"Stamina: {stamina}";

        if (speedText != null)
            speedText.text = $"Speed: {speed}";

        Debug.Log($"Updated Stats: Health={health}, Stamina={stamina}, Speed={speed}");
    }

    public void RefreshStats() // ✅ Fetches latest stats and updates UI
    {
        if (GathererStatManagement.Instance != null)
        {
            UpdateStatDisplay(
                GathererStatManagement.Instance.GetHealth(),
                GathererStatManagement.Instance.GetStamina(),
                GathererStatManagement.Instance.GetSpeed()
            );
        }
        else
        {
            Debug.LogError("❌ GathererStatManagement instance not found!");
        }
    }
}
