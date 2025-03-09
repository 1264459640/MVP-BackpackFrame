using R3;
using UnityEngine;

namespace UniRxCustom
{
    [DisallowMultipleComponent]
    public class ObservableOnMouseDownTrigger : ObservableTriggerBase
    {
        #region variable

        private Subject<Unit> onMouseDown;

        #endregion

        #region property

        public Subject<Unit> OnMouseDownAsObservable()
        {
            return onMouseDown ??= new Subject<Unit>();
        }

        #endregion

        #region unity event

        private void OnMouseDown()
        {
            onMouseDown?.OnNext(default);
        }

        #endregion

        #region method

        protected override void RaiseOnCompletedOnDestroy()
        {
            onMouseDown?.OnCompleted();
        }

        #endregion
    }
}
