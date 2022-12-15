using UnityEngine;

namespace Runtime.Inputs
{
    public static class Inputs

    {
        public static int GetTouchCount()
        {
            return Input.touchCount;
        }

        public static void GetTouch(int index, out Vector2 position, out TouchPhase phase)
        {
            var touch = Input.GetTouch(index);

            position = touch.position;
            phase = touch.phase;
        }

        public static void GetTouch(int index, out Vector2 position)
        {
            var touch = Input.GetTouch(index);

            position = touch.position;
        }

        public static Touch GetTouch(int index)
        {
            return Input.GetTouch(index);
        }
    }
}