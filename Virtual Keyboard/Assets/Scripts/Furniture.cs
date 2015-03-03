using System;
using UnityEngine;

public class Furniture
{
	public GameObject model;
	public enum Mounting {FLOOR, CEILING, WALL};
	public Mounting mounting;
	public Vector3 scale;
	public Vector3 rotation;

	public Furniture (GameObject model, Mounting mounting,
	                  Vector3 scale, Vector3 rotation)
	{
		this.mounting = mounting;
		this.scale = scale;
		this.rotation = rotation;
		this.model = model;
	}
}