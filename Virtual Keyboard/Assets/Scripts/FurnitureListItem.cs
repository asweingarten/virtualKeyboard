using UnityEngine;
using System.Collections;

public class FurnitureListItem : MonoBehaviour, IListItem
{
	public string furnitureName;
	public GameObject furniture;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public virtual void execute() {
		Instantiate (furniture, new Vector3 (0, 0, 0), Quaternion.identity);
	}
}

