using Extension;
using UIFrame.Utilities;
using UnityEngine;

namespace UIFrame.Base
{
    public abstract class UIScreen 
    {
        private GameObject root;
        protected readonly CanvasGroup canvasGroup;
        
        public UIScreen(GameObject root)
        {
            this.root = root;
            canvasGroup = root.GetOrAddComponent<CanvasGroup>();
        }
        public virtual void Show()
        {
            canvasGroup.interactable = true;
            root.transform.SetAsFirstSibling();
            OnEnable();
        }

        public virtual void Hide()
        {
            canvasGroup.interactable = false;
            OnDisable();
        }

        protected abstract void OnDisable();
        protected abstract void OnEnable();
    }
}
