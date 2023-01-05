using System.Collections.Generic;
using System.Linq;
using Data.Plane;
using UnityEngine;

namespace Runtime.Utilities
{
    public static class JsonSerializer
    {
        private static RealTimeFlightObject DeserializeRealTimeFlightObject(string json)
        {
            return JsonUtility.FromJson<RealTimeFlightObject>(json);
        }
        
        private static FlightObject DeserializeFlightObject(string json)
        {
            return JsonUtility.FromJson<FlightObject>(json);
        }

        public static List<RealTimeFlightResponse> ConvertResponseToObject(string text)
        {
            var data = DeserializeRealTimeFlightObject(text);
            var flights = data.Response.Where(entry=> entry.Alt is < 1000 and > 0).ToList();
            return flights;
        }

        public static FlightResponse ConvertFlightToObject(string text)
        {
            var data = DeserializeFlightObject(text);
            var flight = data.Response;
            return flight;
        }

        public static string ConvertWeatherDataToObject(string text)
        {
            var data = JsonUtility.FromJson<WeatherResponse>(text);
            var weather = data.Data[0];
            return weather;
        }
    }
}