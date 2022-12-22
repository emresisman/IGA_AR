using Data.Plane;
using UnityEngine;

namespace Runtime.Planes
{
    public class PlaneManager : Singleton<PlaneManager>
    {
        [SerializeField]
        private GameObject planePrefab;

        public void CreatePlane(FlightResponse plane)
        {
            Debug.Log(plane.Flight_Iata 
                      + "\n" + plane.Dep_Iata
                      + "\n" + plane.Arr_Iata
                      + "\n" + plane.Alt
                      + "\n" + plane.Dep_Time
                      + "\n" + plane.Arr_Time);
        }

        public void RemovePlane(Plane plane)
        {

        }
    }
}