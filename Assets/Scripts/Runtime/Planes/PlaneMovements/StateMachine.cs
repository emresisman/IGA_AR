namespace Runtime.Planes.PlaneMovements
{
    public class StateMachine
    {
        public MovementStates CurrentState { get; private set; }

        public void Initialize(MovementStates startingState)
        {
            CurrentState = startingState;
            startingState.Enter();
        }

        public void ChangeState(MovementStates newState)
        {
            CurrentState.Exit();
            CurrentState = newState;
            newState.Enter();
        }
    }
}