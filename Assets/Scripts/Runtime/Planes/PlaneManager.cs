using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Data.Plane;
using Runtime.InfoPanel;
using Runtime.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Runtime.Planes
{
    public class PlaneManager : Singleton<PlaneManager>
    {
        [SerializeField]
        private GameObject planePrefab;
        
        private readonly List<string> landedPlanes = new List<string>();
        private readonly List<string> takeOffPlanes = new List<string>();
        private readonly List<FlightResponse> onRouteLandingPlanes = new List<FlightResponse>();
        private readonly List<FlightResponse> onRouteTakeOffPlanes = new List<FlightResponse>();

        private void Start()
        {
            StartCoroutine(CreateLandingPlane());
            StartCoroutine(CreateTakeOffPlane());
        }

        private IEnumerator CreateLandingPlane()
        {
            while (true)
            {
                if (onRouteLandingPlanes.Count > 0)
                {
                    var plane = Instantiate(planePrefab, transform.position, Quaternion.identity);
                    if (plane.TryGetComponent<Plane>(out var component))
                    {
                        var planeResponse = onRouteLandingPlanes[0];
                        component.SetFlight(planeResponse, FlightDirection.Arrival);
                        landedPlanes.Add(planeResponse.Flight_Iata);
                        onRouteLandingPlanes.Remove(planeResponse);

                        var terminalEntry = planeResponse.Flight_Iata + " - " + planeResponse.Dep_Iata + " > " + planeResponse.Arr_Iata + " - " + planeResponse.Arr_Time_Utc + "UTC";
                        PanelManager.Instance.SetTerminalArrivals(terminalEntry);
                    }
                }

                yield return new WaitForSeconds(Random.Range(22,30));
            }
        }
        
        private IEnumerator CreateTakeOffPlane()
        {
            while (true)
            {
                if (onRouteTakeOffPlanes.Count > 0)
                {
                    var plane = Instantiate(planePrefab, transform.position, Quaternion.identity);
                    if (plane.TryGetComponent<Plane>(out var component))
                    {
                        var planeResponse = onRouteTakeOffPlanes[0];
                        component.SetFlight(planeResponse, FlightDirection.Departure);
                        takeOffPlanes.Add(planeResponse.Flight_Iata);
                        onRouteTakeOffPlanes.Remove(planeResponse);

                        var terminalEntry = planeResponse.Flight_Iata + " - " + planeResponse.Dep_Iata + " > " + planeResponse.Arr_Iata + " - " + planeResponse.Dep_Time_Utc + "UTC";
                        PanelManager.Instance.SetTerminalDepartures(terminalEntry);
                    }
                }

                yield return new WaitForSeconds(Random.Range(22,30));
            }
        }

        public void UpdateArrivalFlights(List<FlightResponse> flights)
        {
            var listedFlights = flights.OrderBy(x=> x.Alt).ToList();
            foreach (var plane in listedFlights)
            {
                if (onRouteLandingPlanes.Any(x=> x.Flight_Iata == plane.Flight_Iata) || 
                    landedPlanes.Contains(plane.Flight_Iata)) continue;
                
                onRouteLandingPlanes.Add(plane);
            }
        }
        
        public void UpdateDepartureFlights(List<FlightResponse> flights)
        {
            var listedFlights = flights.OrderBy(x=> x.Alt).ToList();
            foreach (var plane in listedFlights)
            {
                if (onRouteTakeOffPlanes.Any(x=> x.Flight_Iata == plane.Flight_Iata) || 
                    takeOffPlanes.Contains(plane.Flight_Iata)) continue;
                
                onRouteTakeOffPlanes.Add(plane);
            }
        }

        public void RemovePlane(GameObject plane)
        {
            StartCoroutine(DestroyPlane(plane));
        }

        private IEnumerator DestroyPlane(GameObject plane)
        {
            yield return new WaitForSeconds(3f);
            Destroy(plane);
            
        }
    }
}