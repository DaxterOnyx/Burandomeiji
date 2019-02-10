using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// script mère de tous les objets qu'utilise le joueur VR
/// @author Brice
/// </summary>
public class Tool : MonoBehaviour
{
	protected bool inUse = false;
	public virtual void Use()
	{
		inUse = true;
	}

	public virtual void EndUse()
	{
		inUse = false;
	}
}

