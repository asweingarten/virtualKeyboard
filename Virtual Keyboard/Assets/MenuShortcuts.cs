using UnityEngine;
using System.Collections;

[RequireComponent (typeof (ItemSelection))]
public class MenuShortcuts : MonoBehaviour {

	public char menuKey = 'm';

	private ItemSelection itemSelection;
	public GameObject menuObject;

	// Use this for initialization
	void Start () {
		itemSelection = GetComponent<ItemSelection> ();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (radialTransformManager);
		if (isSelected() && Input.GetKeyDown (""+menuKey)) {
			menuObject.SetActive(!menuObject.activeSelf);
		}
	}

	private bool isSelected() {
		return itemSelection.selected;
	}
}
