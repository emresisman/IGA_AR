using UnityEngine;
using TMPro;

namespace Assets.Scripts.Runtime.InfoPanel
{
    public class WeatherPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text metarWeatherText;
        [SerializeField] private TMP_Text tafWeatherText;

        public void SetWeatherText(string metar, string taf)
        {
            metarWeatherText.text = metar;
            tafWeatherText.text = taf;
        }
    }
}