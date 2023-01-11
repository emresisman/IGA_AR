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
        private string arrivalFlightsUri, departureFlightsUri, weatherMetarUri, weatherTafUri;
        private string airLabsApiKey, weatherApiKey;
        
        private readonly List<string> nearestArrivals = new List<string>();
        private readonly List<string> nearestDepartures = new List<string>();
        private readonly List<FlightResponse> nearestArrivalPlanes = new List<FlightResponse>();
        private readonly List<FlightResponse> nearestDeparturePlanes = new List<FlightResponse>();

        private void Start()
        {
            airLabsApiKey = "be7b3fae-2e7e-416d-b2e7-e2ec16f5e069";

            weatherApiKey = "f51fb137414b429d821d160d0b";

            arrivalFlightsUri =
                "https://airlabs.co/api/v9/flights?_fields=flight_iata,alt&arr_iata=IST&airline_iata=TK&api_key=" + airLabsApiKey;
            
            departureFlightsUri =
                "https://airlabs.co/api/v9/flights?_fields=flight_iata,alt&dep_iata=IST&airline_iata=TK&api_key=" + airLabsApiKey;

            weatherMetarUri = 
                "https://api.checkwx.com/metar/LTFM?x-api-key=" + weatherApiKey;

            weatherTafUri = 
                "https://api.checkwx.com/taf/LTFM?x-api-key=" + weatherApiKey;
            
            StartCoroutine(RequestLoop());
            StartCoroutine(WeatherRequest());
        }

        private IEnumerator WeatherRequest()
        {
            using var webRequestMetar = UnityWebRequest.Get(weatherMetarUri);
            
            yield return webRequestMetar.SendWebRequest();            
            
            using var webRequestTaf = UnityWebRequest.Get(weatherTafUri);
            
            yield return webRequestTaf.SendWebRequest();

            if (webRequestMetar.result != UnityWebRequest.Result.Success &&
                webRequestTaf.result != UnityWebRequest.Result.Success) yield break;

            var weatherMetar = JsonSerializer.ConvertWeatherDataToObject(webRequestMetar.downloadHandler.text);
            var weatherTaf = JsonSerializer.ConvertWeatherDataToObject(webRequestTaf.downloadHandler.text);

            PanelManager.Instance.SetWeatherPanelText(weatherMetar, weatherTaf);
        }

        private IEnumerator RequestLoop()
        {
            while (true)
            {
                ClearAllList();

                StartCoroutine(GetNearestArrivals());
                StartCoroutine(GetNearestDeparture());
                yield return new WaitForSeconds(5);
                
                
                PullArrivalDetails();
                PullDepartureDetails();
                yield return new WaitForSeconds(5);
                
                
                SendArrivalToPlaneManager();
                SendDepartureToPlaneManager();
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
            switch (direction)
            {
                case FlightDirection.Arrival:
                    if(!IsPlaneLanding(flight)) break;
                    nearestArrivalPlanes.Add(flight);
                    break;
                case FlightDirection.Departure:
                    if(!IsPlaneTakingOff(flight)) break;
                    nearestDeparturePlanes.Add(flight);
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
            return "https://airlabs.co/api/v9/flight?flight_iata=" + flight + "&api_key=" + airLabsApiKey;
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