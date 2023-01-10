using TMPro;
using UnityEngine;

namespace Runtime.InfoPanel
{
    public class TerminalPanel : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text departuresText, arrivalsText;

        public void AddDepartures(string departure)
        {
            departuresText.text += departure + "\n";
        }

        public void AddArrivals(string arrival) 
        {
            arrivalsText.text += arrival + "\n";
        }
    }
}