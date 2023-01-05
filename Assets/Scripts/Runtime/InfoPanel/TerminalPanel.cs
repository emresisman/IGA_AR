using Data.Plane;
using TMPro;
using UnityEngine;

namespace Runtime.InfoPanel
{
    public class TerminalPanel : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text weatherText;

        public void SetText(string weather)
        {
            weatherText.text = weather;
        }
    }
}