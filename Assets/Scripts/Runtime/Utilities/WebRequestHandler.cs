using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Data.Plane;
using Runtime.InfoPanel;
using Runtime.Planes;
using UnityEngine;
using UnityEngine.Networking;

namespace Runtime.Utilities
{
    public enum FlightDirection
    {
        Arrival,
        Departure
    }
    
    public class WebRequestHandler: MonoBehaviour
    {
        private string arrivalFlightsUri, departureFlightsUri, weatherUri;
        
        private readonly List<string> nearestArrivals = new List<string>();
        private readonly List<string> nearestDepartures = new List<string>();
        private readonly List<FlightResponse> nearestArrivalPlanes = new List<FlightResponse>();
        private readonly List<FlightResponse> nearestDeparturePlanes = new List<FlightResponse>();

        private void Start()
        {
            arrivalFlightsUri =
                "https://airlabs.co/api/v9/flights?_fields=flight_iata,alt&arr_iata=IST&airline_iata=TK&api_key=be7b3fae-2e7e-416d-b2e7-e2ec16f5e069";
            
            departureFlightsUri =
                "https://airlabs.co/api/v9/flights?_fields=flight_iata,alt&dep_iata=IST&airline_iata=TK&api_key=be7b3fae-2e7e-416d-b2e7-e2ec16f5e069";

            weatherUri = 
                "https://api.checkwx.com/metar/LTFM?x-api-key=f51fb137414b429d821d160d0b";
            
            StartCoroutine(RequestLoop());
            StartCoroutine(WeatherRequest());
        }

        private IEnumerator WeatherRequest()
        {
            using var webRequest = UnityWebRequest.Get(weatherUri);
            
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success) yield break;
            var weather = JsonSerializer.ConvertWeatherDataToObject(webRequest.downloadHandler.text);
            PanelManager.Instance.SetTerminalPanelText(weather);
        }

        private IEnumerator RequestLoop()
        {
            while (true)
            {
                ClearAllList();

                StartCoroutine(GetNearestArrivals());
                StartCoroutine(GetNearestDeparture());
                Debug.Log("En yakın uçuşlar çekildi...");
                yield return new WaitForSeconds(5);
                
                
                PullArrivalDetails();
                PullDepartureDetails();
                Debug.Log("En yakın uçuşların detayları çekildi...");
                yield return new WaitForSeconds(5);
                
                
                SendArrivalToPlaneManager();
                SendDepartureToPlaneManager();
                Debug.Log("Uçuşlar PlaneManager'a gönderildi...");
                yield return new WaitForSeconds(20);
            }
        }

        private IEnumerator GetNearestArrivals()
        {
            using var webRequest = UnityWebRequest.Get(arrivalFlightsUri);
            
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success) yield break;
            var flights = JsonSerializer.ConvertResponseToObject(webRequest.downloadHandler.text);
            foreach (var alt in flights)
            {
                nearestArrivals.Add(alt.Flight_Iata);
            }
        }
        
        private IEnumerator GetNearestDeparture()
        {
            using var webRequest = UnityWebRequest.Get(departureFlightsUri);
            
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success) yield break;
            var flights = JsonSerializer.ConvertResponseToObject(webRequest.downloadHandler.text);
            foreach (var alt in flights)
            {
                nearestDepartures.Add(alt.Flight_Iata);
            }
        }
        
        private IEnumerator GetFlightDetails(string uri, FlightDirection direction)
        {
            using var webRequest = UnityWebRequest.Get(uri);
            
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success) yield break;
            var flight = JsonSerializer.ConvertFlightToObject(webRequest.downloadHandler.text);
            Debug.Log("1000 matre altındaki uçuş : " + flight.Flight_Iata);
            switch (direction)
            {
                case FlightDirection.Arrival:
                    if(!IsPlaneLanding(flight)) break;
                    nearestArrivalPlanes.Add(flight);
                    Debug.Log("Geçerli iniş Yapan : " + flight.Flight_Iata);
                    break;
                case FlightDirection.Departure:
                    if(!IsPlaneTakingOff(flight)) break;
                    nearestDeparturePlanes.Add(flight);
                    Debug.Log("Geçerli kalkış Yapan : " + flight.Flight_Iata);
                    break;
            }
        }

        private bool IsPlaneLanding(FlightResponse flight)
        {
            if (flight.Dep_Time_Utc == null) return false;
            var duration = flight.Duration;
            var depTime = flight.Dep_Time_Utc;
            var givenTime = DateTime.ParseExact(depTime, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
            var currentTime = DateTime.UtcNow;
            var difference = currentTime.Subtract(givenTime);
            return !(difference.TotalMinutes < (duration / 2));
        }

        private bool IsPlaneTakingOff(FlightResponse flight)
        {
            if (flight.Dep_Time_Utc == null) return false;
            var duration = flight.Duration;
            var depTime = flight.Dep_Time_Utc;
            var givenTime = DateTime.ParseExact(depTime, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
            var currentTime = DateTime.UtcNow;
            var difference = currentTime.Subtract(givenTime);
            return !(difference.TotalMinutes > (duration / 2));
        }

        private void ClearAllList()
        {
            nearestArrivals.Clear();
            nearestDepartures.Clear();
            nearestArrivalPlanes.Clear();
            nearestDeparturePlanes.Clear();
        }

        private string GetFlightDetailsUri(string flight)
        {
            return "https://airlabs.co/api/v9/flight?flight_iata=" +
                flight + "&api_key=be7b3fae-2e7e-416d-b2e7-e2ec16f5e069";
        }

        private void PullArrivalDetails()
        {
            if(nearestArrivals.Count == 0) return;
            foreach (var flight in nearestArrivals)
            {
                StartCoroutine(GetFlightDetails(GetFlightDetailsUri(flight), FlightDirection.Arrival));
            }
        }
        
        private void PullDepartureDetails()
        {
            if(nearestDepartures.Count == 0) return;
            foreach (var flight in nearestDepartures)
            {
                StartCoroutine(GetFlightDetails(GetFlightDetailsUri(flight), FlightDirection.Departure));
            }
        }

        private void SendArrivalToPlaneManager()
        {
            if(nearestArrivalPlanes.Count == 0) return;
            PlaneManager.Instance.UpdateArrivalFlights(nearestArrivalPlanes);
        }
        
        private void SendDepartureToPlaneManager()
        {
            if(nearestDeparturePlanes.Count == 0) return;
            PlaneManager.Instance.UpdateDepartureFlights(nearestDeparturePlanes);
        }
    }
}