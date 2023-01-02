using UnityEngine;

namespace Runtime.Planes.PlaneMovements
{
    public abstract class MovementStates
    {
        protected Plane plane;
        protected StateMachine stateMachine;

        protected MovementStates(Plane plane, StateMachine stateMachine)
        {
            this.plane = plane;
            this.stateMachine = stateMachine;
        }

        public virtual void Enter()
        {
            
        }

        public virtual void HandleInput()
        {

        }

        public virtual void LogicUpdate()
        {
            
        }

        public virtual void PhysicsUpdate()
        {

        }

        public virtual void Exit()
        {

        }
    }
}