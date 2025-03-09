using System.Collections.Generic;
using UnityEngine;

namespace Extension
{
	public static class DefaultExtensions
	{
		#region For Component

		public static T AddComponent<T>(this Component component) where T : Component
		{
			return component.gameObject.AddComponent<T>();
		}

		public static T GetOrAddComponent<T>(this Component component) where T : Component
		{
			if (!component.TryGetComponent(out T result)) result = component.AddComponent<T>();

			return result;
		}

		public static bool HasComponent<T>(this Component component) where T : Component
		{
			return component.GetComponent<T>() != null;
		}

		#endregion

		#region For GameObjects

		
		public static T GetOrAddComponent<T>(this GameObject go) where T : Component
		{
			return go.TryGetComponent<T>(out var component) ? component : go.AddComponent<T>();
		}

		public static bool HasComponent<T>(this GameObject go) where T : Component
		{
			return go.GetComponent<T>() != null;
		}

		public static void DestroyChildren(this Transform trans)
		{
			for (var i = trans.childCount - 1; i >= 0; i--)
			{
				Object.Destroy(trans.GetChild(i).gameObject);
			}
		}

		#endregion
		
		   
		public static T GetOrAddComponentInChild<T>(this GameObject gameObject, string childName) where T : Component
		{
			var childTransform = gameObject.transform.FindRecursive(childName);
			if (childTransform != null)
			{
				var component = childTransform.GetComponent<T>();
				if (component != null) return component;
				component = childTransform.gameObject.AddComponent<T>();
				Debug.LogWarning($"Component {typeof(T).Name} was added to {childTransform.gameObject.name}.");
				return component;
			}
			Debug.LogError($"{gameObject.name} not have {childName}");
			return null;
		}
        
		private static Transform FindRecursive(this Transform transform, string childName)
		{
			if (string.IsNullOrEmpty(childName))
			{
				return null;
			}
            
			var queue = new Queue<Transform>();
			queue.Enqueue(transform);

			while (queue.Count > 0)
			{
				var current = queue.Dequeue();
				var found = current.Find(childName);
				if (found != null)
				{
					return found;
				}
				foreach (Transform child in current)
				{
					queue.Enqueue(child);
				}
			}

			return null;
		}
	}
}