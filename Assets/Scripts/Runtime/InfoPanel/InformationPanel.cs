using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Runtime.InfoPanel
{
    public class InformationPanel : MonoBehaviour
    {
        [SerializeField] private GameObject myPanel;
        public GameObject MyPanel => this.myPanel;
    }
}