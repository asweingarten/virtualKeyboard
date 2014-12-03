using UnityEngine;
using System.Collections;

public class InitializeScene : MonoBehaviour {
	void Start() {
		Debug.Log ("up and running");
		
		string[] keys = {"`", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "-", "=",
			"q", "w", "e", "r", "t", "y", "u", "i", "o", "p", "[", "]", "\\",
			"a", "s", "d", "f", "g", "h", "j", "k", "l", ";", "'",
			"z", "x", "c", "v", "b", "n", "m", ",", ".", "/",
			"space"};
		
		string[] row1Keys = {"`", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "-", "="};
		string[] row2Keys = {"q", "w", "e", "r", "t", "y", "u", "i", "o", "p", "[", "]", "\\"};
		
		GameObject user = GameObject.Find ("User");
		GameObject keyboardObj = new GameObject ();
		keyboardObj.name = "ScriptKeyboard";
		keyboardObj.AddComponent ("KeyboardComponent");
		keyboardObj.transform.parent = user.transform;
		keyboardObj.transform.localPosition = new Vector3 (0, -1.8f, 0);
		
		GameObject keyboardRow1 = new GameObject ();
		keyboardRow1.transform.parent = keyboardObj.transform;
		keyboardRow1.transform.localPosition = new Vector3 (0, 0, 0);
		keyboardRow1.AddComponent ("KeyboardRowComponent");
		KeyboardRowComponent rowComponent = keyboardRow1.GetComponent<KeyboardRowComponent> ();
		rowComponent.setKeys (row1Keys);
		
		KeyboardComponent keyboard = keyboardObj.GetComponent<KeyboardComponent> ();
		
		int offset = 0;
		foreach (string s in rowComponent.getKeys()){
			new Key(keyboardRow1, s, offset);
			offset += 1;
		}
		user.AddComponent<MovementScript> ();

		Debug.Log ("Making leapmotion");
		GameObject leapMotion = new GameObject ();
		leapMotion.name = "LeapMotionController";
		leapMotion.transform.parent = user.transform;
		leapMotion.AddComponent<HandController> ();
		HandController handController = leapMotion.GetComponent<HandController> ();
		

	}
}