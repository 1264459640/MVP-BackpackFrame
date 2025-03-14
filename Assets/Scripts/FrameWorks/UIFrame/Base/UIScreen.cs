using System;
using Extension;
using R3;
using UnityEngine;

namespace FrameWorks.UIFrame.Base
{
    public abstract class UIScreen : IDisposable
    {
        protected GameObject root;
        protected CanvasGroup canvasGroup;

        protected readonly CompositeDisposable disposable = new();
        
        public UIScreen(GameObject root)
        {
            this.root = root;
            InitializeComponent();
        }
        public virtual void Show()
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            root.transform.SetAsFirstSibling();
            OnEnable();
        }

        public virtual void Hide()
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            OnDisable();
        }

        protected virtual void InitializeComponent()
        {
            canvasGroup = root.GetOrAddComponent<CanvasGroup>();
        }

        protected abstract void RegisterEvent();
        
        protected abstract void OnDisable();
        protected abstract void OnEnable();
        
        public void Dispose()
        {
            disposable.Dispose();
        }
    }
}
