using System;
using UnityEngine;

namespace Data.Buildings
{
    [Serializable]
    public class BuildingObject
    {
        [SerializeField] private string header;
        public string Header {  
            get => this.header;
            set => this.header = value;
        }

        [SerializeField] private string description;
        public string Description {  
            get => this.description;
            set => this.description = value;
        }
    }
}