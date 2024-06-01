public class BuildBrickBot : IStateBot
{
    public void OnEnter(Bot bot)
    {
        bot.EnterBuildBridge();
    }

    public void OnExecute(Bot bot)
    {
        bot.BuildBridge();
    }

    public void OnExit(Bot bot)
    {

    }
}
