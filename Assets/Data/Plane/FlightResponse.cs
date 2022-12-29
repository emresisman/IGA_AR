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
        [SerializeField] private int alt;
        public int Alt {  
            get => this.alt;
            set => this.alt = value;
        }        
        [SerializeField] private int duration;
        public int Duration {  
            get => this.duration;
            set => this.duration = value;
        }        
        [SerializeField] private string dep_time_utc;
        public string Dep_Time_Utc {  
            get => this.dep_time_utc;
            set => this.dep_time_utc = value;
        }        
        [SerializeField] private string arr_time_utc;
        public string Arr_Time_Utc {  
            get => this.arr_time_utc;
            set => this.arr_time_utc = value;
        }        
        [SerializeField] private string dep_actual_utc;
        public string Dep_Actual_Utc {  
            get => this.dep_actual_utc;
            set => this.dep_actual_utc = value;
        }        
        [SerializeField] private string arr_estimated_utc;
        public string Arr_Estimated_Utc {  
            get => this.arr_estimated_utc;
            set => this.arr_estimated_utc = value;
        }        
        [SerializeField] private string model;
        public string Model {  
            get => this.model;
            set => this.model = value;
        }
    }
}