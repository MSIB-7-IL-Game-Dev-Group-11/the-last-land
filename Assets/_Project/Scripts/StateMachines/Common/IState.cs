namespace TheLastLand._Project.Scripts.StateMachines.Common
{
    public interface IState
    {
        void OnEnter();
        void Update();
        void FixedUpdate();
        void OnExit();
    }
}