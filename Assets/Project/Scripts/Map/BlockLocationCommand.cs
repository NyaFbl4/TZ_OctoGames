using Naninovel;

namespace Project.Scripts.Map
{
    [CommandAlias("blockLocation")]
    public class BlockLocationCommand : Command
    {
        [RequiredParameter]
        public StringParameter LocationId;

        public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            Engine.GetService<MapService>().BlockLocation(LocationId);
            return UniTask.CompletedTask;
        }
    }
}