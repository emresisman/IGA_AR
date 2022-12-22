using System;
using UnityEngine;
using Plane = Runtime.Planes.Plane;

namespace Runtime.InfoPanel
{
    public class InformationPanel : MonoBehaviour
    {
        [SerializeField] private GameObject myPanel;
        public GameObject MyPanel => this.myPanel;

        private void Start()
        {
            if (gameObject.TryGetComponent<Plane>(out var component))
            {
                myPanel = PanelManager.Instance.GetPlanePanel().gameObject;
            }
        }
    }
}