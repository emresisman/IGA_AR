using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Runtime.InfoPanel
{
    public class PanelManager : Singleton<PanelManager>
    {
        [SerializeField] protected Animator infoPanelAnimator;
        [SerializeField] private GameObject turkishTechnicPanel;
        [SerializeField] private GameObject turkishCargoPanel;
        [SerializeField] private GameObject terminalPanel;
        [SerializeField] private GameObject planePanel;

        private readonly int infoParam = Animator.StringToHash("SetActive");
        private InformationPanel activePanel = null;
        private List<GameObject> allPanels;

        private void Start()
        {
            allPanels = new List<GameObject>();
            AddAllPanels();
            DeactivateAll();
        }

        public void DeactivatePanel()
        {
            SetAnimationBool(false);
        }

        public bool AnyActivePanel()
        {
            return activePanel != null;
        }

        public void SwitchPanelActivity(InformationPanel panel)
        {
            Debug.Log(panel.name);
            if (GetAnimationBool())
            {
                if (activePanel == panel) //Close panel
                {
                    SetAnimationBool(false);
                    ClearOutline(activePanel.gameObject);
                    activePanel.isActive = false;
                    activePanel = null;
                }
                else //Switch panel
                {
                    SetActive(panel.MyPanel);
                    ClearOutline(activePanel.gameObject);
                    activePanel.isActive = false;
                    activePanel = panel;
                    AddOutline(activePanel.gameObject);
                    activePanel.isActive = true;
                }
            }
            else //New panel
            {
                SetActive(panel.MyPanel);
                SetAnimationBool(true);
                activePanel = panel;
                AddOutline(activePanel.gameObject);
                activePanel.isActive = true;
            }
        }

        private void SetAnimationBool(bool value)
        {
            infoPanelAnimator.SetBool(infoParam, value);
        }

        private bool GetAnimationBool()
        {
            return infoPanelAnimator.GetBool(infoParam);
        }

        private void SetActive(GameObject panel)
        {
            DeactivateAll();
            panel.SetActive(true);
        }

        private void DeactivateAll()
        {
            foreach (var pnl in allPanels)
            {
                pnl.SetActive(false);
            }
        }

        private void AddAllPanels()
        {
            allPanels.Add(turkishTechnicPanel);
            allPanels.Add(turkishCargoPanel);
            allPanels.Add(terminalPanel);
            allPanels.Add(planePanel);
        }

        private void AddOutline(GameObject obj)
        {
            var outline = obj.AddComponent<Outline>();
            outline.OutlineMode = Outline.Mode.OutlineAll;
            outline.OutlineWidth = 7f;
            outline.OutlineColor= Color.yellow;
        }

        private void ClearOutline(GameObject obj)
        {
            Destroy(obj.GetComponent<Outline>());
        }
    }
}