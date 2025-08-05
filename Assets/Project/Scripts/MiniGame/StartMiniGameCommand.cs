using Naninovel;

namespace Project.Scripts.MiniGame
{
    [CommandAlias("miniGame")]
    public class StartMiniGameCommand : Command
    {
        public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            var uiManager = Engine.GetService<IUIManager>();
            var gameUI = uiManager.GetUI<MiniGameUI>();
            gameUI?.Show();
        
            return UniTask.CompletedTask;
        }
    }
}