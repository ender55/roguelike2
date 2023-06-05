public interface IState
{
    public StateMachine StateMachine { get; set; }
    public void Enter();
    public void Exit();
}