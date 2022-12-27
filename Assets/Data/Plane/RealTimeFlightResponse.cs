using System;
using UnityEngine;

namespace Data.Plane
{
    [Serializable]
    public class RealTimeFlightResponse
    {
        [SerializeField] private string flight_iata;
        public string Flight_Iata {  
            get => this.flight_iata;
            set => this.flight_iata = value;
        }
        [SerializeField] private int alt;
        public int Alt {  
            get => this.alt;
            set => this.alt = value;
        }
    }
}