using System;
using UnityEngine;

namespace Data.Plane
{
    [Serializable]
    public class FlightResponse
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
        [SerializeField] private string dep_time;
        public string Dep_Time {  
            get => this.dep_time;
            set => this.dep_time = value;
        }        
        [SerializeField] private string arr_time;
        public string Arr_Time {  
            get => this.arr_time;
            set => this.arr_time = value;
        }        
        [SerializeField] private string dep_actual;
        public string Dep_Actual {  
            get => this.dep_actual;
            set => this.dep_actual = value;
        }        
        [SerializeField] private string arr_estimated;
        public string Arr_Estimated {  
            get => this.arr_estimated;
            set => this.arr_estimated = value;
        }
    }
}