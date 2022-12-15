using UnityEngine;

namespace Runtime.InfoPanel
{
    public class PanelManager : Singleton<PanelManager>
    {
        [SerializeField]
        protected Animator infoPanelAnimator;
        private readonly int infoParam = Animator.StringToHash("SetActive");
        private InformationPanel activePanel = null;

        public void SwitchPanelActivity(InformationPanel panel)
        {
            Debug.Log(panel.name);
            if (GetAnimationBool())
            {
                if (activePanel == panel)
                {
                    SetAnimationBool(false);
                    activePanel.isActive = false;
                    activePanel = null;
                }
                else
                {
                    Debug.Log("Bilgiler yeni onjeye göre güncellendi.");
                    activePanel.isActive = false;
                    activePanel = panel;
                    activePanel.isActive = true;
                }
            }
            else
            {
                SetAnimationBool(true);
                activePanel = panel;
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
    }
}