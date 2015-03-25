using UnityEngine;
using System.Collections;

public abstract class TextReceiver : MonoBehaviour
{
	public abstract void receiveText(KeyCode keyCode);
	public abstract void receiveText(string text);
}

