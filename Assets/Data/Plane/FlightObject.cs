using System;
using UnityEngine;

namespace Data.Plane
{
    [Serializable]
    public class FlightObject
    {
        [SerializeField] private FlightResponse response;
        public FlightResponse Response
        {
            get => this.response;
            set => this.response = value;
        }
    }
}