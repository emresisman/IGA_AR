using Runtime.InfoPanel;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime
{
    public class Interactable : MonoBehaviour, IPointerDownHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            if (TryGetComponent<InformationPanel>(out var component))
            {
                PanelManager.Instance.SwitchPanelActivity(component);
            }
            else if(PanelManager.Instance.AnyActivePanel())
            {
                PanelManager.Instance.DeactivatePanel();
            }
        }
    }
}