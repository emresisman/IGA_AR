using System.Collections;
using Data.Plane;
using UnityEngine;
using UnityEngine.Networking;

namespace Runtime.Utilities
{
    public class WebRequestHandler: MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(GetRequest("https://airlabs.co/api/v9/flights?_fields=flight_iata,dep_iata,arr_iata,alt&arr_iata=IST&airline_iata=TK&bbox=41.216629,28.680496,41.343825,28.787956&api_key=5be83830-7a41-4b7f-b746-d1480d7dc7ac"));
        }

        IEnumerator GetRequest(string uri)
        {
            using var webRequest = UnityWebRequest.Get(uri);
            
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    var data = JsonSerializer.DeserializePlaneResponse(webRequest.downloadHandler.text);
                    Debug.Log(data.Response[0].Flight_Iata);
                    break;
            }
        }
    }
}