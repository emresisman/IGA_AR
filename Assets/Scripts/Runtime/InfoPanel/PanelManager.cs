﻿using System.Collections.Generic;
using UnityEngine;

namespace Runtime.InfoPanel
{
    public class PanelManager : Singleton<PanelManager>
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] protected Animator infoPanelAnimator;
        [SerializeField] private GameObject turkishTechnicPanel;
        [SerializeField] private GameObject turkishCargoPanel;
        [SerializeField] private GameObject terminalPanel;
        [SerializeField] private GameObject planePanel;
        [SerializeField] private LayerMask interactableMask;

        private readonly int infoParam = Animator.StringToHash("SetActive");
        private InformationPanel activePanel = null;
        private List<GameObject> allPanels;
        private bool isPanelActive;

        private void Start()
        {
            isPanelActive = false;
            allPanels = new List<GameObject>();
            AddAllPanels();
            DeactivateAll();
        }

        private void Update()
        {
            if (Input.touchCount <= 0) return;
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                var panel = GetRaycastHitFromScreen(touch.position, interactableMask);
                SwitchPanelActivity(panel);
            }
        }

        private InformationPanel GetRaycastHitFromScreen(Vector2 screenPosition, LayerMask layerMask)
        {
            var ray = mainCamera.ScreenPointToRay(screenPosition);
            if (Physics.Raycast(ray: ray, hitInfo: out var hit, maxDistance: 50f, layerMask: layerMask))
            {
                hit.transform.TryGetComponent<InformationPanel>(out var component);
                return component;
            }
            else
            {
                return null;
            }
        }

        private void SwitchPanelActivity(InformationPanel panel)
        {
            if (panel == null)
            {
                if (isPanelActive)
                {
                    ClosePanel(panel);
                }
            }
            else
            {
                if (isPanelActive)
                {
                    if (activePanel == panel)
                    {
                        ClosePanel(panel);
                    }
                    else
                    {
                        ClosePanel(activePanel);
                        OpenPanel(panel);
                    }
                }
                else
                {
                    OpenPanel(panel);
                }
            }
        }

        private void OpenPanel(InformationPanel panel)
        {
            SetActive(panel.MyPanel);
            SetAnimationBool(true);
            activePanel = panel;
            AddOutline(activePanel.gameObject);
        }

        private void ClosePanel(InformationPanel panel)
        {
            SetAnimationBool(false);
            ClearOutline(activePanel.gameObject);
            activePanel = null;
        }

        private void SetAnimationBool(bool value)
        {
            isPanelActive = value;
            infoPanelAnimator.SetBool(infoParam, value);
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
            if (obj.TryGetComponent<Outline>(out var component)) return;
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