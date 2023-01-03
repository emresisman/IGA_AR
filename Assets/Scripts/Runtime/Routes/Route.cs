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
        public LineRenderer myRoute;
        public bool isUsing = false;
        public int wheelContactIndex;
        public int driveInOutIndex;

        public PlaneState GetLandingState(int planeIndex)
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
        
        public PlaneState GetTakeOffState(int planeIndex)
        {
            if (planeIndex <= driveInOutIndex)
            {
                return PlaneState.Driving;
            }
            else if (planeIndex <= wheelContactIndex)
            {
                return PlaneState.Landed;
            }
            else
            {
                return PlaneState.OnAir;
            }
        }
    }
}