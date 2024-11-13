namespace TheLastLand._Project.Scripts.StateMachines
{
    public interface ITransition
    {
        IState TargetState { get; }
        IPredicate Condition { get; }
        
        
    }
}