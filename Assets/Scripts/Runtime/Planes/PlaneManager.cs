using System.Collections.Generic;
using Data.Plane;
using UnityEngine;

namespace Runtime.Planes
{
    public class PlaneManager : Singleton<PlaneManager>
    {
        [SerializeField]
        private GameObject planePrefab;

        private List<string> actionedPlanes = new List<string>();

        public void CreatePlane(FlightResponse planeResponse)
        {
            if (actionedPlanes.Contains(planeResponse.Flight_Iata)) return;

            actionedPlanes.Add(planeResponse.Flight_Iata);
            var plane = Instantiate(planePrefab, transform.position, Quaternion.identity);
            if (plane.TryGetComponent<Plane>(out var component))
            {
                component.SetFlight(planeResponse);
            }
        }
    }
}