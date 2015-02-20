using UnityEngine;
using System.Collections;

public class ItemSelection : MonoBehaviour {

	public delegate void ItemOnSelect(GameObject selectedItem);
	public static event ItemOnSelect OnItemSelected;

	private ParticleSystem particleSystem;

	// Use this for initialization
	void Start () {
		createParticleSystem();
	}

	void createParticleSystem() {
		if (gameObject.particleSystem == null) {
			particleSystem = gameObject.AddComponent<ParticleSystem>();
		} else {
			particleSystem = gameObject.GetComponent<ParticleSystem>();
		}
		particleSystem.Stop();
		particleSystem.loop = true;
		particleSystem.maxParticles = 1000;
		particleSystem.startLifetime = 0.5f;
		particleSystem.startColor = Color.cyan;
		particleSystem.startSize = 0.03f;
		particleSystem.startSpeed = 0.2f;
	}

	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision) {
		if (OnItemSelected != null) {
			OnItemSelected(gameObject);
		}
	}
}
