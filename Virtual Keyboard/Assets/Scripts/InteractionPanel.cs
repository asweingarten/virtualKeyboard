using UnityEngine;
using System.Collections;

public abstract class InteractionPanel : MonoBehaviour
{
	public Collider[] collisionExclusionsList;

	public GameObject user;

	private BoxCollider[] childColliders;

	public delegate void TriggerEvent();
	public event TriggerEvent OnAction;

	void Awake() {
		calculateBounds();
	}

	// Use this for initialization
	void Start ()
	{
		childColliders = gameObject.GetComponentsInChildren<BoxCollider>() as BoxCollider[];
		foreach( Collider childCollider in childColliders ) {
			if( childCollider.enabled == true ) {
				foreach( Collider collisonExcluded in collisionExclusionsList ) {
					Physics.IgnoreCollision( childCollider, collisonExcluded );
				}
			}
		}
	}

	// Update is called once per frame
	void Update ()
	{

	}

	public void TriggerAction() {
		Debug.Log("Action triggered");
		if (OnAction != null) {
			OnAction();
		}
	}

	public abstract void calculateBounds();

	public abstract bool withinBounds(Vector3 coordinate);
}

