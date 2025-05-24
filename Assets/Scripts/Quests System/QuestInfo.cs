using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestInfo",menuName = "ScriptableObjects/QuestInfo", order = 1)]
public class QuestInfo : ScriptableObject
{
    [field: SerializeField] public string id { get; private set; }

    [Header("General")]

    public string displayName;

    [Header("Pre-Requirements")]

    [Tooltip("This will be the #of runs/deaths required before this quest shows up")]
    public int runRequirement;
    public QuestInfo[] questPrerequisites;

    [Header("Steps")]

    public GameObject[] questStepPrefabs;

    [Header("Rewards")]

    public GameObject reward;

    // sets the ID of the quest to be the name of the scriptable object
    private void OnValidate()
    {
#if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }
}
