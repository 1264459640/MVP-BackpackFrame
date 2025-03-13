using System;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UniRxCustom
{
	[DisallowMultipleComponent]
	public class ObservablePointerDownTrigger : ObservableTriggerBase, IPointerDownHandler
	{
		private Subject<PointerEventData> onPointerDown;
		

		void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
		{
			onPointerDown?.OnNext(eventData);
		}

		public Observable<PointerEventData> OnPointerDownAsObservable()
		{
			return onPointerDown ??= new Subject<PointerEventData>();
		}

		protected override void RaiseOnCompletedOnDestroy()
		{
			onPointerDown?.OnCompleted();
		}
	}
}