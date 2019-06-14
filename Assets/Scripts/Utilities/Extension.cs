using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public static class GO_Extensions
{

	public static void PositionToLastChild(this Transform aParent)
	{
		var childs = aParent.Cast<Transform>().ToList();
		float pos = childs.Min(c => c.position.y);
		foreach(var c in childs)
		{
			c.parent = null;
		}
		aParent.position = new Vector3(0, pos, 0);
		foreach(var c in childs)
		{
			c.parent = aParent;
		}
	}

	public static void MoveOnlyParent(this Transform aParent, Vector3 _pos)
	{
		var childs = aParent.Cast<Transform>().ToList();
		foreach(var c in childs)
		{
			c.parent = null;
		}
		aParent.position = _pos;
		foreach(var c in childs)
		{
			c.parent = aParent;
		}
	}

	public static float GetCenterPosition(this Transform aParent, int axis)
	{
		var childs = aParent.Cast<Transform>().ToList();
		float posAxis = 0f;
		foreach(var c in childs)
		{
			Debug.Log("piece name = " + c.gameObject.name + "pos = " + c.localPosition[axis], c.gameObject);
			posAxis += c.localPosition[axis];
		}
		Debug.Log("center pos = " +  posAxis / childs.Count + "childs.count = " + childs.Count);
		Debug.Log("column pos = " + aParent.transform.position);
		return posAxis / childs.Count;
	}
}
