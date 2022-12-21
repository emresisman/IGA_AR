using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Plane
{
    [Serializable]
    public class RealTimeFlightObject
    {
        [SerializeField] private List<RealTimeFlightResponse> response;
        public List<RealTimeFlightResponse> Response
        {
            get => this.response;
            set => this.response = value;
        }
    }
}