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
            var plane = Instantiate(planePrefab, transform.position, Quaternion.identity);
            if (plane.TryGetComponent<Plane>(out var component))
            {
                component.SetFlight(planeResponse);
            }
        }
    }
}