using System.Threading.Tasks;
using UnityEngine;
using Naninovel;
using Project.Scripts.Game;

public class NaninovelGameBridge : MonoBehaviour
{
    private GameUI gameUI;
    private IScriptPlayer scriptPlayer;

    private void Awake()
    {
        gameUI = GetComponent<GameUI>();
        scriptPlayer = Engine.GetService<IScriptPlayer>();
        
        //gameUI.OnGameCompleted += HandleGameCompleted;
    }

    private void OnDestroy()
    {
        //gameUI.OnGameCompleted -= HandleGameCompleted;
    }

    private void HandleGameCompleted()
    {
        var variableManager = Engine.GetService<ICustomVariableManager>();
        variableManager.SetVariableValue("GameCompleted", "true");
    
        // Альтернатива без переменной - напрямую продолжаем скрипт
        scriptPlayer.Play();
    }
}