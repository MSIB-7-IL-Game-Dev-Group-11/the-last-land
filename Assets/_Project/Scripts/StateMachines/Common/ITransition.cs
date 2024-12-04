namespace TheLastLand._Project.Scripts.StateMachines.Common
{
    public interface ITransition
    {
        IState TargetState { get; }
        IPredicate Condition { get; }
        
        
    }
}