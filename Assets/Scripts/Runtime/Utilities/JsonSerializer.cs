using System;
using System.Collections;
using System.Collections.Generic;
using Data.Plane;
using UnityEngine;
using System.IO;
using System.Text;
using Data.Buildings;
using Object = System.Object;

namespace Runtime.Utilities
{
    public static class JsonSerializer
    {
        public static void CreatePlaneJson()
        {
            /*var defaultObject = new PlaneObject
            {
                CallSign = "TK253",
                FromAirport = "TRX",
                ToAirport = "IST"
            };

            var json = JsonUtility.ToJson(defaultObject);
            
            var fileStream = File.Open(Application.dataPath + "/Data/Plane/PlaneData.json", 
                FileMode.OpenOrCreate,
                FileAccess.ReadWrite,
                FileShare.None);
            
            fileStream.Write(Encoding.UTF8.GetBytes(json));
            fileStream.Close();*/
        }
        
        public static void CreateBuildingJson()
        {
            /*var defaultObject = new BuildingObject()
            {
                Header = "Default Header",
                Description = "Default description..."
            };

            var json = JsonUtility.ToJson(defaultObject);
            
            var fileStream = File.Open(Application.dataPath + "/Data/Buildings/BuildingData.json", 
                FileMode.OpenOrCreate,
                FileAccess.ReadWrite,
                FileShare.None);
            
            fileStream.Write(Encoding.UTF8.GetBytes(json));
            fileStream.Close();*/
        }

        public static RealTimeFlightObject DeserializeRealTimeFlightObject(string json)
        {
            return JsonUtility.FromJson<RealTimeFlightObject>(json);
        }        
        public static FlightObject DeserializeFlightObject(string json)
        {
            return JsonUtility.FromJson<FlightObject>(json);
        }
    }
}