using Naninovel;

namespace Project.Scripts.Game
{
    [CommandAlias("miniGame")]
    public class StartMiniGameCommand : Command
    {
        public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            var uiManager = Engine.GetService<IUIManager>();
            var gameUI = uiManager.GetUI<GameUI>();
            gameUI?.Show();
        
            return UniTask.CompletedTask;
        }
    }
}