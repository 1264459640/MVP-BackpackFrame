using System;
using R3;
using UniRxCustom;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Extension
{
	public static class UniRxExtensions
	{
		public static Observable<Unit> OnMouseDownAsObservable(this Component component)
		{
			if (!component || !component.gameObject) return Observable.Empty<Unit>();
            
			return component.GetOrAddComponent<ObservableOnMouseDownTrigger>().OnMouseDownAsObservable();
		}
		
		public static Observable<PointerEventData> OnPointerDownAsObservable(this UIBehaviour component)
		{
			if (component == null || component.gameObject == null) return Observable.Empty<PointerEventData>();
			return component.gameObject.GetOrAddComponent<ObservablePointerDownTrigger>().OnPointerDownAsObservable().AsObservable();
		}
	}
}