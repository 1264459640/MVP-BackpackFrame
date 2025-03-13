using System;
using Extension;
using UnityEngine;

public class Test : MonoBehaviour
{
	private void Awake()
	{
		var context1 = gameObject.GetOrAddComponent<TestLifetimeScope>();
		var context2 = gameObject.GetOrAddComponent<TestLifetimeScope2>();

		if (context1.Container.Resolve(typeof(Test2)) !=
		    context2.Container.Resolve(typeof(Test2)))
		{
			Debug.Log(1);
		}
		else
		{
			Debug.Log(2);
		}
	}
}
