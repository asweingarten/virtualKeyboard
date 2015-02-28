using UnityEngine;
using System.Collections;

public abstract class ListItem : MonoBehaviour
{
	private bool isActive;
	
	void setActive(bool isActive) {
		this.isActive = isActive;
	}
	
	void execute();
}
