﻿using Runtime.Routes;
using UnityEngine;

namespace Runtime.Planes.PlaneMovements
{
    public class TakeOffState : MovementStates
    {
        private Route route;
        private LineRenderer takeOffRoute;
        private PlaneState speedState;
        private float planeSpeed;
        private float targetSpeed;
        private int lastIndex;
        private int currentIndex;
        
        public TakeOffState(Plane plane, StateMachine stateMachine) : base(plane, stateMachine) { }

        public override void Enter()
        {
            route = RouteManager.Instance.GetAvailableTakeOffRoute();
            takeOffRoute = route.myRoute;
            lastIndex = takeOffRoute.positionCount - 1;
            SetStartPosition();
            SetStartSpeed();
            currentIndex = 0;
        }

        public override void Exit()
        {
            PlaneManager.Instance.RemovePlane(plane.gameObject);
        }

        public override void HandleInput()
        {
            if (currentIndex == lastIndex)
            {
                stateMachine.ChangeState(plane.Stopped);
            }
        }

        public override void LogicUpdate()
        {
            CalculateSpeed();
        }

        public override void PhysicsUpdate()
        {
            Rotate();
            Move();
        }
        
        private void Move()
        {
            plane.transform.position = Vector3.MoveTowards(
                plane.transform.position,
                takeOffRoute.transform.TransformPoint(takeOffRoute.GetPosition(currentIndex + 1)),
                planeSpeed * Time.deltaTime
            );

            if (plane.transform.position != takeOffRoute.transform.TransformPoint(takeOffRoute.GetPosition(currentIndex + 1))) return;
            
            currentIndex += 1;
            UpdateState();
            CalculateTargetSpeed();
        }
        
        private void Rotate()
        {
            var relative = plane.transform.InverseTransformPoint(takeOffRoute.transform.TransformPoint(takeOffRoute.GetPosition(currentIndex + 1)));
            var angle = Mathf.Atan2(relative.x, relative.z) * Mathf.Rad2Deg;
            plane.transform.Rotate(0, angle, 0);
        }
        
        private void SetStartPosition()
        {
            plane.transform.position = takeOffRoute.transform.TransformPoint(takeOffRoute.GetPosition(currentIndex));
        }
        
        private void SetStartSpeed()
        {
            UpdateState();
            CalculateTargetSpeed();
            planeSpeed = targetSpeed;
        }
        
        private void UpdateState()
        {
            speedState = route.GetTakeOffState(currentIndex);
        }
        
        private void CalculateTargetSpeed()
        {
            switch (speedState)
            {
                case PlaneState.OnAir:
                    targetSpeed = 2f;
                    break;
                case PlaneState.Landed:
                    targetSpeed = 1.25f;
                    break;
                case PlaneState.Driving:
                    targetSpeed = 0.5f;
                    break;
            }
        }

        private void CalculateSpeed()
        {
            if (targetSpeed > planeSpeed) IncreaseSpeed();
            if (targetSpeed < planeSpeed) DecreaseSpeed();
        }

        private void IncreaseSpeed()
        {
            planeSpeed += Time.deltaTime / planeSpeed;
        }

        private void DecreaseSpeed()
        {
            planeSpeed -= Time.deltaTime / planeSpeed;
        }
    }
}