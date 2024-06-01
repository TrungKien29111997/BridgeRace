public class FindBrickBot : IStateBot
{
    public void OnEnter(Bot bot)
    {
        bot.EnterFindBrick();
    }

    public void OnExecute(Bot bot)
    {
        bot.FindBrick();
    }

    public void OnExit(Bot bot)
    {

    }
}
