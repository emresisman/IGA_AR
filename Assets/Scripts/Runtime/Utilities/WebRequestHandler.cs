using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Data.Plane;
using Runtime.Planes;
using UnityEngine;
using UnityEngine.Networking;

namespace Runtime.Utilities
{
    public class WebRequestHandler: MonoBehaviour
    {
        private string flightsUri, flightInfoUri;
        private int minAlt = 1000;

        private List<string> nearestPlanesAlt = new List<string>();
        private List<FlightResponse> nearestPlanes = new List<FlightResponse>();

        private void Start()
        {
            flightsUri =
                "https://airlabs.co/api/v9/flights?_fields=flight_iata,alt&arr_iata=IST&airline_iata=TK&api_key=5be83830-7a41-4b7f-b746-d1480d7dc7ac";
            StartCoroutine(RequestLoop());
        }

        IEnumerator RequestLoop()
        {
            while (true)
            {
                nearestPlanesAlt.Clear();
                nearestPlanes.Clear();
                StartCoroutine(GetNearestFlights());
                Debug.Log("En yakın uçuşlar çekildi...");
                yield return new WaitForSeconds(5);
                foreach (var flight in nearestPlanesAlt)
                {
                    StartCoroutine(GetFlightInfo(GetFlightInfoUri(flight)));
                }
                Debug.Log("En yakın uçuşlar detayları çekildi...");
                yield return new WaitForSeconds(5);
                if(nearestPlanes.Count > 0) PlaneManager.Instance.UpdateNearestFlights(nearestPlanes);
                Debug.Log("Uçuşlar PlaneManager'a gönderildi...");
                yield return new WaitForSeconds(20);
            }
        }

        IEnumerator GetNearestFlights()
        {
            using var webRequest = UnityWebRequest.Get(flightsUri);
            
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                    //Error Message
                    break;
                case UnityWebRequest.Result.DataProcessingError:
                    //Error Message
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    //Error Message
                    break;
                case UnityWebRequest.Result.Success:
                    var flights = ConvertResponseToObject(webRequest.downloadHandler.text);
                    foreach (var alt in flights)
                    {
                        nearestPlanesAlt.Add(alt.Flight_Iata);
                        Debug.Log(alt.Flight_Iata);
                    }
                    break;
            }
        }
        
        IEnumerator GetFlightInfo(string uri)
        {
            using var webRequest = UnityWebRequest.Get(uri);
            
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                    //Error Message
                    break;
                case UnityWebRequest.Result.DataProcessingError:
                    //Error Message
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    //Error Message
                    break;
                case UnityWebRequest.Result.Success:
                    var flight = ConvertFlightToObject(webRequest.downloadHandler.text);
                    nearestPlanes.Add(flight);
                    break;
            }
        }

        private string GetFlightInfoUri(string flight)
        {
            return "https://airlabs.co/api/v9/flight?flight_iata=" +
                flight + "&api_key=5be83830-7a41-4b7f-b746-d1480d7dc7ac";
        }

        private List<RealTimeFlightResponse> ConvertResponseToObject(string text)
        {
            var data = JsonSerializer.DeserializeRealTimeFlightObject(text);
            var flights = data.Response.Where(entry=> entry.Alt < minAlt && entry.Alt > 0).ToList();
            return flights;
        }

        private FlightResponse ConvertFlightToObject(string text)
        {
            var data = JsonSerializer.DeserializeFlightObject(text);
            var flight = data.Response;
            return flight;
        }
    }
}