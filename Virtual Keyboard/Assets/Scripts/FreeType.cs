using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class FreeType : TextReceiver {
	string typedText = "";
	private UnityEngine.UI.Text typedTextbox;
	private UnityEngine.UI.Text underline;
	private string startingUnderline = "";

	public bool allowKeyboardInput = false;
	public float underlineBlinkSpeed = 0.5f;

	static Dictionary<string,bool> isKeyDownArray = new Dictionary<string,bool>();

	void Awake() {
		typedTextbox = GameObject.Find("TypedText").GetComponent("Text") as Text;
		underline = GameObject.Find("Underline").GetComponent("Text") as Text;
		startingUnderline = underline.text;
		StartCoroutine(blinkUnderline());

		isKeyDownArray[KeyCode.A.ToString()] = false;
		
		isKeyDownArray[KeyCode.B.ToString()] = false;
		
		isKeyDownArray[KeyCode.C.ToString()] = false;
		
		isKeyDownArray[KeyCode.D.ToString()] = false;
		
		isKeyDownArray[KeyCode.E.ToString()] = false;
		
		isKeyDownArray[KeyCode.F.ToString()] = false;
		
		isKeyDownArray[KeyCode.G.ToString()] = false;
		
		isKeyDownArray[KeyCode.H.ToString()] = false;
		
		isKeyDownArray[KeyCode.I.ToString()] = false;
		
		isKeyDownArray[KeyCode.J.ToString()] = false;
		
		isKeyDownArray[KeyCode.K.ToString()] = false;
		
		isKeyDownArray[KeyCode.L.ToString()] = false;
		
		isKeyDownArray[KeyCode.M.ToString()] = false;
		
		isKeyDownArray[KeyCode.N.ToString()] = false;
		
		isKeyDownArray[KeyCode.O.ToString()] = false;
		
		isKeyDownArray[KeyCode.P.ToString()] = false;
		
		isKeyDownArray[KeyCode.Q.ToString()] = false;
		
		isKeyDownArray[KeyCode.R.ToString()] = false;
		
		isKeyDownArray[KeyCode.S.ToString()] = false;
		
		isKeyDownArray[KeyCode.T.ToString()] = false;
		
		isKeyDownArray[KeyCode.U.ToString()] = false;
		
		isKeyDownArray[KeyCode.V.ToString()] = false;
		
		isKeyDownArray[KeyCode.W.ToString()] = false;
		
		isKeyDownArray[KeyCode.X.ToString()] = false;
		
		isKeyDownArray[KeyCode.Y.ToString()] = false;
		
		isKeyDownArray[KeyCode.Z.ToString()] = false;

		isKeyDownArray[getStringRepresentationOfKey(KeyCode.Space)] = false;

		isKeyDownArray[getStringRepresentationOfKey(KeyCode.Comma)] = false;

		isKeyDownArray[getStringRepresentationOfKey(KeyCode.Period)] = false;

		isKeyDownArray[getStringRepresentationOfKey(KeyCode.Semicolon)] = false;

		isKeyDownArray[getStringRepresentationOfKey(KeyCode.Minus)] = false;

		isKeyDownArray[getStringRepresentationOfKey(KeyCode.Quote)] = false;
	}

	private void updateUnderline() {
		if( underline == null ) return;
		if( typedText.EndsWith(" ") ) {
			underline.text = " " + startingUnderline;
		} else {
			underline.text = startingUnderline;
		}
	}

	private IEnumerator blinkUnderline() {
		while( underline != null ) {
			underline.enabled = !underline.enabled;
			yield return new WaitForSeconds(underlineBlinkSpeed);
		}
	}

	void OnDestroy() {
		StopAllCoroutines();
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (!allowKeyboardInput) {
			return;
		}

		onKeyDown(KeyCode.A);
		onKeyDown(KeyCode.B);
		onKeyDown(KeyCode.C);
		onKeyDown(KeyCode.D);
		onKeyDown(KeyCode.E);
		onKeyDown(KeyCode.F);
		onKeyDown(KeyCode.G);
		onKeyDown(KeyCode.H);
		onKeyDown(KeyCode.I);
		onKeyDown(KeyCode.J);
		onKeyDown(KeyCode.K);
		onKeyDown(KeyCode.L);
		onKeyDown(KeyCode.M);
		onKeyDown(KeyCode.N);
		onKeyDown(KeyCode.O);
		onKeyDown(KeyCode.P);
		onKeyDown(KeyCode.Q);
		onKeyDown(KeyCode.R);
		onKeyDown(KeyCode.S);
		onKeyDown(KeyCode.T);
		onKeyDown(KeyCode.U);
		onKeyDown(KeyCode.V);
		onKeyDown(KeyCode.W);
		onKeyDown(KeyCode.X);
		onKeyDown(KeyCode.Y);
		onKeyDown(KeyCode.Z);
		onKeyDown(KeyCode.Space);
		onKeyDown(KeyCode.Comma);
		onKeyDown(KeyCode.Period);
		onKeyDown(KeyCode.Semicolon);
		onKeyDown(KeyCode.Minus);
		onKeyDown(KeyCode.Quote);
		onKeyDown(KeyCode.Backspace);

		onKeyUp(KeyCode.A);
		onKeyUp(KeyCode.B);
		onKeyUp(KeyCode.C);
		onKeyUp(KeyCode.D);
		onKeyUp(KeyCode.E);
		onKeyUp(KeyCode.F);
		onKeyUp(KeyCode.G);
		onKeyUp(KeyCode.H);
		onKeyUp(KeyCode.I);
		onKeyUp(KeyCode.J);
		onKeyUp(KeyCode.K);
		onKeyUp(KeyCode.L);
		onKeyUp(KeyCode.M);
		onKeyUp(KeyCode.N);
		onKeyUp(KeyCode.O);
		onKeyUp(KeyCode.P);
		onKeyUp(KeyCode.Q);
		onKeyUp(KeyCode.R);
		onKeyUp(KeyCode.S);
		onKeyUp(KeyCode.T);
		onKeyUp(KeyCode.U);
		onKeyUp(KeyCode.V);
		onKeyUp(KeyCode.W);
		onKeyUp(KeyCode.X);
		onKeyUp(KeyCode.Y);
		onKeyUp(KeyCode.Z);
		onKeyUp(KeyCode.Space);
		onKeyUp(KeyCode.Comma);
		onKeyUp(KeyCode.Period);
		onKeyUp(KeyCode.Semicolon);
		onKeyUp(KeyCode.Minus);
		onKeyUp(KeyCode.Quote);
		onKeyDown(KeyCode.Backspace);

		updateUnderline();
	}

	public void updateText(char keyPressed) {
		//Debug.Log("key pressed: " + keyPressed);

		typedText += keyPressed;
		typedTextbox.text = typedText;
	}

	private void onKeyDown(KeyCode key) {
		string keyPressed = getStringRepresentationOfKey(key);

		if (isKeyDownArray[keyPressed]) {
			return;
		} else if (Input.GetKeyDown(key)){
			isKeyDownArray[keyPressed] = true;
			if( key == KeyCode.Backspace ) {
				backspace();
			} else {
				updateText(keyPressed.ToLower()[0]);

			}
		}
	}

	private void onKeyUp(KeyCode key) {
		string keyPressed = getStringRepresentationOfKey(key);
		if (!isKeyDownArray[keyPressed]) {
			return;
		} else if (Input.GetKeyUp(key)){
			isKeyDownArray[keyPressed] = false;
		}
	}

	private string getStringRepresentationOfKey(KeyCode key) {
		switch(key) {
			case KeyCode.Comma:
				return ",";

			case KeyCode.Period:
				return ".";

			case KeyCode.Space:
				return " ";

			case KeyCode.Semicolon:
				return ";";

			case KeyCode.Minus:
				return "-";

			case KeyCode.Quote:
				return "'";

			default:
				return key.ToString();
		}
	}

	private void backspace() {
		if( typedText.Length == 0 ) return;
		typedText = typedText.Remove(typedText.Length - 1, 1);
		typedTextbox.text = typedText;
	}

	private void space() {
		typedText = typedText + " ";
		typedTextbox.text = typedText;
	}

	public override void receiveText(KeyCode keyCode) {
		switch(keyCode) {
			case KeyCode.Backspace: 
				backspace(); 
				break;
			case KeyCode.Space:
				space();
				break;
			default:
				receiveText(getStringRepresentationOfKey(keyCode));
				break;
		}
		updateUnderline();
	}

	public override void receiveText(string text) {
		foreach (char c in text) {
			updateText(c);
		}
		updateUnderline();
	}
}
