using UnityEngine;
using System.Collections;

public class TextureListItem : ListItem
{
	public delegate void ApplyTexture(Texture texture);
	public static event ApplyTexture TextureApplied;

	[SerializeField]
	Texture texture;

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{

	}

	public override void execute() {
		if (texture == null) return;
		Debug.Log("firing texture applied event");
		TextureApplied(texture);
	}
}

