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
        [SerializeField] private string dep_iata;
        public string Dep_Iata {  
            get => this.dep_iata;
            set => this.dep_iata = value;
        }
        [SerializeField] private string arr_iata;
        public string Arr_Iata {  
            get => this.arr_iata;
            set => this.arr_iata = value;
        }
        [SerializeField] private int alt;
        public int Alt {  
            get => this.alt;
            set => this.alt = value;
        }
    }
}