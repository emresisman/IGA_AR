using UnityEngine;
using UnityEngine.Serialization;

namespace Runtime.Routes
{
    public enum PlaneState
    {
        OnAir,
        Landed,
        Driving
    }

    public class Route : MonoBehaviour
    {
        public bool isUsing = false;
        public int wheelContactIndex;
        public int driveInOutIndex;

        public PlaneState GetPlaneState(int planeIndex)
        {
            if (planeIndex <= wheelContactIndex)
            {
                return PlaneState.OnAir;
            }
            else if (planeIndex <= driveInOutIndex)
            {
                return PlaneState.Landed;
            }
            else
            {
                return PlaneState.Driving;
            }
        }
    }
}