using System.Collections;
using System.Collections.Generic;
using Naninovel;
using UnityEngine;

public class NameInputService : IEngineService
{
    public UniTask InitializeServiceAsync() => UniTask.CompletedTask;
    public void ResetService() { }
    public void DestroyService() { }
}
