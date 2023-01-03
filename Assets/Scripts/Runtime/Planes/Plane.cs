using System;
using Data.Plane;
using Runtime.InfoPanel;
using Runtime.Planes.PlaneMovements;
using Runtime.Utilities;
using UnityEngine;

namespace Runtime.Planes
{
    public class Plane : MonoBehaviour
    {
        public StateMachine MovementStateMachine;
        public LandingState Landing;
        public TakeOffState TakeOff;
        public StoppedState Stopped;
        

        private PlanePanel myPanel;
        private FlightResponse myFlightInfo;

        private void Awake()
        {
            MovementStateMachine = new StateMachine();

            Landing = new LandingState(this, MovementStateMachine);
            TakeOff = new TakeOffState(this, MovementStateMachine);
            Stopped = new StoppedState(this, MovementStateMachine);
            
            MovementStateMachine.Initialize(Stopped);
            
            myPanel = PanelManager.Instance.GetPlanePanel();
        }

        private void Update()
        {
            MovementStateMachine.CurrentState.HandleInput();
            MovementStateMachine.CurrentState.LogicUpdate();
            Debug.Log(transform.position + "Plane");
        }

        private void FixedUpdate()
        {
            MovementStateMachine.CurrentState.PhysicsUpdate();
        }

        public void SetFlight(FlightResponse flight, FlightDirection direction)
        {
            myFlightInfo = flight;
            switch (direction)
            {
                case FlightDirection.Arrival:
                    MovementStateMachine.ChangeState(Landing);
                    break;
                case FlightDirection.Departure:
                    MovementStateMachine.ChangeState(TakeOff);
                    break;
            }
        }

        public void SetText()
        {
            myPanel.SetText(myFlightInfo);
        }
    }
}