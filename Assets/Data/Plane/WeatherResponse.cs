using System.Collections.Generic;
using UnityEngine;

namespace Data.Plane
{
    public class WeatherResponse
    {
        [SerializeField] private List<string> data;
        public List<string> Data
        {
            get => this.data;
            set => this.data = value;
        }
    }
}