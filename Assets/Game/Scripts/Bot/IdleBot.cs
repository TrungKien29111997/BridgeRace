public class IdleBot : IStateBot
{
    public void OnEnter(Bot bot)
    {
        bot.EnterIdleState();
    }

    public void OnExecute(Bot bot)
    {

    }

    public void OnExit(Bot bot)
    {

    }
}
