using System.Collections;
using System.Linq;
using Data.Plane;
using UnityEngine;
using UnityEngine.Networking;

namespace Runtime.Utilities
{
    public class WebRequestHandler: MonoBehaviour
    {
        private string flightsUri, flightInfoUri;
        
        private void Start()
        {
            flightsUri =
                "https://airlabs.co/api/v9/flights?_fields=flight_iata,dep_iata,arr_iata,alt&arr_iata=IST&airline_iata=TK&bbox=41.216629,28.680496,41.343825,28.787956&api_key=5be83830-7a41-4b7f-b746-d1480d7dc7ac";
            StartCoroutine(GetFlightInfo());
        }

        IEnumerator GetNearestFlight()
        {
            using var webRequest = UnityWebRequest.Get(flightsUri);
            
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    break;
                case UnityWebRequest.Result.Success:
                    var flight = ConvertResponseToObject(webRequest.downloadHandler.text);
                    SetFlightInfoUri(flight);
                    break;
            }
        }
        
        IEnumerator GetFlightInfo()
        {
            yield return GetNearestFlight();
            using var webRequest = UnityWebRequest.Get(flightInfoUri);
            
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    break;
                case UnityWebRequest.Result.Success:
                    var flight = ConverFlightToObject(webRequest.downloadHandler.text);
                    Debug.Log(flight.Flight_Iata + " " + flight.Dep_Iata + " " + flight.Arr_Iata);
                    break;
            }
        }

        private void SetFlightInfoUri(string flight)
        {
            flightInfoUri =
                "https://airlabs.co/api/v9/flight?_fields=flight_iata,dep_iata,arr_iata,dep_time,dep_actual,arr_time,arr_estimated&flight_iata=" +
                flight + "&api_key=5be83830-7a41-4b7f-b746-d1480d7dc7ac";
        }

        private string ConvertResponseToObject(string text)
        {
            var data = JsonSerializer.DeserializeRealTimeFlightObject(text);
            var min = data.Response.Min(entry=> entry.Alt);
            var flight = data.Response.Find(entry => entry.Alt == min).Flight_Iata;
            return flight;
        }

        private FlightResponse ConverFlightToObject(string text)
        {
            var data = JsonSerializer.DeserializeFlightObject(text);
            var flight = data.Response;
            return flight;
        }
    }
}