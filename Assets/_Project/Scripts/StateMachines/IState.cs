namespace TheLastLand._Project.Scripts.StateMachines
{
    public interface IState
    {
        void OnEnter();
        void Update();
        void FixedUpdate();
        void OnExit();
    }
}