public interface IStateBot
{
    void OnEnter(Bot bot);
    void OnExecute(Bot bot);
    void OnExit(Bot bot);
}
