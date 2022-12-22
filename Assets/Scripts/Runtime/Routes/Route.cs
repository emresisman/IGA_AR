using UnityEngine;

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
        public bool isUsing;
        public int WheelContactIndex;
        public int DriveInOutIndex;

        public PlaneState GetPlaneState(int planeIndex)
        {
            if (planeIndex <= WheelContactIndex)
            {
                return PlaneState.OnAir;
            }
            else if (planeIndex <= DriveInOutIndex)
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