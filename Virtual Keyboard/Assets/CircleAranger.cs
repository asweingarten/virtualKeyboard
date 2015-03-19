using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CircleAranger : MonoBehaviour
{

	public float radius = 1.5f;
	[ContextMenu("Arrange children as circle")]
	public void arrageCircle() {
		int childCount = transform.childCount;
		float angleIncrement = (2*Mathf.PI)/childCount;
		for( int i = 0; i < childCount; i++ ) {
			Transform child = transform.GetChild(i);

			float x = radius*Mathf.Cos(i*angleIncrement);
			float y = radius*Mathf.Sin(i*angleIncrement);

			child.transform.localPosition = new Vector3(x,y,0);
		}
	}
	
		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
}

