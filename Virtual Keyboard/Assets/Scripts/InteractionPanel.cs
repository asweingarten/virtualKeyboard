using UnityEngine;
using System.Collections;

public class InteractionPanel : MonoBehaviour
{
	public Collider[] collisionExclusionsList;

	public GameObject user;

	//private OVRPlayerController playerController;
	private BoxCollider[] childColliders;

	public delegate void TriggerEvent();
	public event TriggerEvent OnAction;

	// Use this for initialization
	void Start ()
	{
		//playerController = user.GetComponent<OVRPlayerController>() as OVRPlayerController;
		//Debug.Log (playerController);
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
}

