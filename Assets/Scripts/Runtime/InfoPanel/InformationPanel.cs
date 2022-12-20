using UnityEngine;

namespace Runtime.InfoPanel
{
    public class InformationPanel : MonoBehaviour
    {
        [SerializeField] private GameObject myPanel;
        public GameObject MyPanel => this.myPanel;

        public bool isActive = false;
        
        public void SetText()
        {
            
        }
    }
}