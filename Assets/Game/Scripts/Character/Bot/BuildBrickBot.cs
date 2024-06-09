public class BuildBrickBot : IState<Bot>
{
    public void OnEnter(Bot bot)
    {

    }

    public void OnExecute(Bot bot)
    {
        bot.BuildBridge();
    }

    public void OnExit(Bot bot)
    {

    }
}
