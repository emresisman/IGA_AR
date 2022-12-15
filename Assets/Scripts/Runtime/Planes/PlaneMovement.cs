using UnityEngine;

namespace Runtime.Planes
{
    public class PlaneMovement : MonoBehaviour
    {
        [SerializeField] private LineRenderer planeLandingRoute;
        [SerializeField] private float currentPlaneSpeed = 1f;
        private Routes route;
        private PlaneState speedState;
        private float targetSpeed;
        private int currentIndex;
        private int lastIndex;

        private void Start()
        {
            currentIndex = 0;
            lastIndex = planeLandingRoute.positionCount - 1;
            route = planeLandingRoute.GetComponent<Routes>();
            SetStartPosition();
            SetStartSpeed();
        }

        private void Update()
        {
            if (currentIndex == lastIndex) return;
            Move();
            CalculateSpeed();
        }

        private void SetStartPosition()
        {
            transform.position = planeLandingRoute.GetPosition(currentIndex);
        }

        private void SetStartSpeed()
        {
            UpdateState();
            CalculateTargetSpeed();
            currentPlaneSpeed = targetSpeed;
        }

        private void UpdateState()
        {
            speedState = route.GetPlaneState(currentIndex);
        }

        private void Move()
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                planeLandingRoute.GetPosition(currentIndex + 1),
                currentPlaneSpeed * Time.deltaTime
            );

            if (transform.position != planeLandingRoute.GetPosition(currentIndex + 1)) return;
            
            currentIndex += 1;
            UpdateState();
            CalculateTargetSpeed();
            if (currentIndex != lastIndex) Rotate();
        }

        private void Rotate()
        {
            var relative = transform.InverseTransformPoint(planeLandingRoute.GetPosition(currentIndex + 1));
            var angle = Mathf.Atan2(relative.x, relative.z) * Mathf.Rad2Deg;
            transform.Rotate(0, angle, 0);
        }

        private void CalculateTargetSpeed()
        {
            switch (speedState)
            {
                case PlaneState.OnAir:
                    targetSpeed = 3.5f;
                    break;
                case PlaneState.Landed:
                    targetSpeed = 1.5f;
                    break;
                case PlaneState.Driving:
                    targetSpeed = 1f;
                    break;
            }
        }

        private void CalculateSpeed()
        {
            if (targetSpeed > currentPlaneSpeed) IncreaseSpeed();
            if (targetSpeed < currentPlaneSpeed) DecreaseSpeed();
        }

        private void IncreaseSpeed()
        {
            currentPlaneSpeed += Time.deltaTime / currentPlaneSpeed;
        }

        private void DecreaseSpeed()
        {
            currentPlaneSpeed -= Time.deltaTime / currentPlaneSpeed;
        }
    }
}