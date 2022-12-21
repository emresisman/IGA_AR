using System;
using UnityEngine;

namespace Data.Plane
{
    [Serializable]
    public class AirLabsAPIObject
    {
        [SerializeField] private RealTimeFlightResponse[] response;
        public RealTimeFlightResponse[] Response
        {
            get => this.response;
            set => this.response = value;
        }
    }
}