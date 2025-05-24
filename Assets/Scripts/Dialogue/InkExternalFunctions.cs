using Ink.Runtime;
using UnityEngine;

public class InkExternalFunctions
{
    public void Bind(Story story)
    {
        story.BindExternalFunction("StartQuest", (string questID) => StartQuest(questID));
        story.BindExternalFunction("AdvanceQuest", (string questID) => AdvanceQuest(questID));
        story.BindExternalFunction("FinishQuest", (string questID) => FinishQuest(questID));
    }

    public void Unbind(Story story)
    {
        story.UnbindExternalFunction("StartQuest");
        story.UnbindExternalFunction("AdvanceQuest");
        story.UnbindExternalFunction("FinishQuest");
    }
    private void StartQuest(string questID)
    {
        EventManager.instance.questEvents.StartQuest(questID);
    }

    private void AdvanceQuest(string questID)
    {
        EventManager.instance.questEvents.AdvanceQuest(questID);
    }
    private void FinishQuest(string questID)
    {
        EventManager.instance.questEvents.FinishQuest(questID);
    }
}
