using Data.Plane;
using UnityEngine;

namespace Runtime.Planes
{
    public class PlaneManager : Singleton<PlaneManager>
    {
        [SerializeField]
        private GameObject planePrefab;

        public void CreatePlane(FlightResponse planeResponse)
        {
            Debug.Log(planeResponse.Flight_Iata 
                      + "\n" + planeResponse.Dep_Iata
                      + "\n" + planeResponse.Arr_Iata
                      + "\n" + planeResponse.Alt
                      + "\n" + planeResponse.Dep_Time
                      + "\n" + planeResponse.Arr_Time);
            
            var plane = Instantiate(planePrefab, transform.position, Quaternion.identity);
            if (plane.TryGetComponent<Plane>(out var component))
            {
                component.SetFlight(planeResponse);
            }
        }
    }
}