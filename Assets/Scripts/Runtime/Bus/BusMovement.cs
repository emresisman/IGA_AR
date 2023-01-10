using UnityEngine;

namespace Assets.Scripts.Runtime.Bus
{
    public class BusMovement : MonoBehaviour
    {
        [SerializeField] private LineRenderer busRoute;
        private Vector3 forwardDir, backwardDir;
        private Direction direction;
        private float speed = 0.2f;

        private enum Direction
        {
            Forward,
            Backward
        }

        private void Start()
        {
            direction = Direction.Forward;
            Rotate();

            forwardDir = busRoute.transform.TransformPoint(busRoute.GetPosition(1));
            backwardDir = busRoute.transform.TransformPoint(busRoute.GetPosition(0));

            transform.position = backwardDir;
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            switch (direction)
            {
                case Direction.Forward:
                    transform.position = Vector3.MoveTowards(
                        transform.position,
                        forwardDir,
                        speed * Time.deltaTime
                    );
                    if (transform.position == forwardDir)
                    {
                        direction = Direction.Backward;
                        Rotate();
                    }
                    break;
                case Direction.Backward:
                    transform.position = Vector3.MoveTowards(
                        transform.position,
                        backwardDir,
                        speed * Time.deltaTime
                    );
                    if (transform.position == backwardDir)
                    {
                        direction = Direction.Forward;
                        Rotate();
                    }
                    break;
            }
        }

        private void Rotate()
        {
            transform.Rotate(0, 180, 0);
        }
    }
}