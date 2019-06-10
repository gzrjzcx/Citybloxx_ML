using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
}
