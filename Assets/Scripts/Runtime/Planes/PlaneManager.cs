using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Data.Plane;
using UnityEngine;

namespace Runtime.Planes
{
    public class PlaneManager : Singleton<PlaneManager>
    {
        [SerializeField]
        private GameObject planePrefab;
        
        private readonly List<string> landedPlanes = new List<string>();
        private readonly List<FlightResponse> onRoutePlanes = new List<FlightResponse>();

        private void Start()
        {
            StartCoroutine(CreatePlane());
        }

        private IEnumerator CreatePlane()
        {
            while (true)
            {
                if (onRoutePlanes.Count > 0)
                {
                    var plane = Instantiate(planePrefab, transform.position, Quaternion.identity);
                    if (plane.TryGetComponent<Plane>(out var component))
                    {
                        var planeResponse = onRoutePlanes[0];
                        component.SetFlight(planeResponse);
                        landedPlanes.Add(planeResponse.Flight_Iata);
                        onRoutePlanes.Remove(planeResponse);
                    }
                }

                yield return new WaitForSeconds(25f);
            }
        }

        public void UpdateArrivalFlights(List<FlightResponse> flights)
        {
            var listedFlights = flights.OrderBy(x=> x.Alt).ToList();
            foreach (var plane in listedFlights)
            {
                if (onRoutePlanes.Any(x=> x.Flight_Iata == plane.Flight_Iata) || 
                    landedPlanes.Contains(plane.Flight_Iata)) continue;
                
                onRoutePlanes.Add(plane);
            }
        }
        
        public void UpdateDepartureFlights(List<FlightResponse> flights)
        {
            /*var listedFlights = flights.OrderBy(x=> x.Alt).ToList();
            foreach (var plane in listedFlights)
            {
                if (onRoutePlanes.Any(x=> x.Flight_Iata == plane.Flight_Iata) || 
                    landedPlanes.Contains(plane.Flight_Iata)) continue;
                
                onRoutePlanes.Add(plane);
            }*/
        }
    }
}