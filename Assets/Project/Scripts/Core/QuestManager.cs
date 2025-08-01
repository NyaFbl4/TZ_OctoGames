using Naninovel;
using Project.Scripts.Core;
using UnityEngine;

[InitializeAtRuntime]
public class QuestManager : IEngineService
{
    private QuestUI questUI;
    
    public string CurrentQuest { get; private set; }

    public UniTask InitializeServiceAsync()
    {
        questUI = Engine.GetService<IUIManager>().GetUI<QuestUI>();
        return UniTask.CompletedTask;
    }

    public void UpdateQuest(string text)
    {
        CurrentQuest = text;
        questUI.UpdateQuest(text);
    }

    public void CompleteQuest() => questUI.Hide();

    public void ResetService() => CurrentQuest = null;
    public void DestroyService() {}
}