using UnityEngine;
using System.Collections;

public class TextureListItem : ListItem
{
	public delegate void ApplyTexture(Texture texture);
	public static event ApplyTexture TextureApplied;

	[SerializeField]
	Texture texture;

	public override void onItemChosen() {
		if (texture == null) return;
		Debug.Log("firing texture applied event");
		TextureApplied(texture);
	}
}

