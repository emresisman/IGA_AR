using System;
using UnityEngine;

namespace Data.Plane
{
    [Serializable]
    public class PlaneObject
    {
        [SerializeField] private string callSign;
        public string CallSign {  
            get => this.callSign;
            set => this.callSign = value;
        }

        [SerializeField] private string fromAirport;
        public string FromAirport {  
            get => this.fromAirport;
            set => this.fromAirport = value;
        }
        
        [SerializeField] private string toAirport;
        public string ToAirport {  
            get => this.toAirport;
            set => this.toAirport = value;
        }
    }
}