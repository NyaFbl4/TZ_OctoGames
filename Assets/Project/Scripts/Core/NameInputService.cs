using System.Collections;
using System.Collections.Generic;
using Naninovel;
using Project.Scripts.Core;
using UnityEngine;

[InitializeAtRuntime]
public class NameInputService : IEngineService
{
    private NameInputUI _inputUI;

    public UniTask<string> RequestPlayerNameAsync() => _inputUI.RequestNameAsync();
    public void ResetService() {}
    public void DestroyService() {}
    
    public UniTask InitializeServiceAsync()
    {
        _inputUI = Engine.GetService<IUIManager>().GetUI<NameInputUI>();
        return UniTask.CompletedTask;
    }
}
