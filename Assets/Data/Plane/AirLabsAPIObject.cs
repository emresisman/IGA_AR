using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Plane
{
    [Serializable]
    public class AirLabsAPIObject
    {
        [SerializeField] private List<RealTimeFlightResponse> response;
        public List<RealTimeFlightResponse> Response
        {
            get => this.response;
            set => this.response = value;
        }
    }
}