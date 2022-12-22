using System.Collections;
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
        private int minAlt = 999;
        
        private void Start()
        {
            flightsUri =
                "https://airlabs.co/api/v9/flights?_fields=flight_iata,dep_iata,arr_iata,alt&arr_iata=IST&airline_iata=TK&api_key=5be83830-7a41-4b7f-b746-d1480d7dc7ac";
            StartCoroutine(RequestLoop());
        }

        IEnumerator RequestLoop()
        {
            while (true)
            {
                yield return new WaitForSeconds(30);
                StartCoroutine(GetFlightInfo((myFlight) =>
                {
                    Debug.Log(minAlt);
                    if(minAlt <= 400) PlaneManager.Instance.CreatePlane(myFlight);
                }));
            }
        }

        IEnumerator GetNearestFlight()
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
                    var flight = ConvertResponseToObject(webRequest.downloadHandler.text);
                    SetFlightInfoUri(flight);
                    break;
            }
        }
        
        IEnumerator GetFlightInfo(System.Action<FlightResponse> callback)
        {
            yield return GetNearestFlight();
            using var webRequest = UnityWebRequest.Get(flightInfoUri);
            
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
                    callback(flight);
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
            minAlt = min;
            var flight = data.Response.Find(entry => entry.Alt == min).Flight_Iata;
            return flight;
        }

        private FlightResponse ConvertFlightToObject(string text)
        {
            var data = JsonSerializer.DeserializeFlightObject(text);
            var flight = data.Response;
            return flight;
        }
    }
}