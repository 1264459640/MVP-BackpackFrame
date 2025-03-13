using R3;
using UnityEngine;

namespace UniRxCustom
{
	public abstract class ObservableTriggerBase: MonoBehaviour
    {
        private bool calledAwake = false;
        private Subject<Unit> awake;
        private void Awake()
        {
            calledAwake = true;
            if (awake == null) return;
            awake.OnNext(Unit.Default); 
            awake.OnCompleted();
        }
        
        public Observable<Unit> AwakeAsObservable()
        {
            if (calledAwake) return Observable.Return(Unit.Default);
            return awake ??= new Subject<Unit>();
        }

        private bool calledStart = false;
        private Subject<Unit> start;

        private void Start()
        {
            calledStart = true;
            if (start == null) return;
            start.OnNext(Unit.Default); 
            start.OnCompleted();
        }
        
        public Observable<Unit> StartAsObservable()
        {
            if (calledStart) return Observable.Return(Unit.Default);
            return start ??= new Subject<Unit>();
        }


        private bool calledDestroy = false;
        private Subject<Unit> onDestroy;
        
        private void OnDestroy()
        {
            calledDestroy = true;
            if (onDestroy != null) { onDestroy.OnNext(Unit.Default); onDestroy.OnCompleted(); }

            RaiseOnCompletedOnDestroy();
        }
        
        public Observable<Unit> OnDestroyAsObservable()
        {
            if (this == null) return Observable.Return(Unit.Default);
            if (calledDestroy) return Observable.Return(Unit.Default);
            return onDestroy ??= new Subject<Unit>();
        }

        protected abstract void RaiseOnCompletedOnDestroy();
    }
}