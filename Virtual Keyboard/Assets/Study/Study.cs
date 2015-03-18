using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Study : MonoBehaviour {
	string untypedText;
	string typedText;
	private UnityEngine.UI.Text untypedTextbox;
	private UnityEngine.UI.Text typedTextbox;
	private UnityEngine.UI.Text results;

	private enum StudyType {
		ENGLISH_PHYSICAL,
		ENGLISH_VIRTUAL,
		LATIN_PHYSICAL,
		LATIN_VIRTUAL,
		DEBUG
	}
	[SerializeField]
	private StudyType studyType;

	int mistakeCount;
	bool studyFinished = false;
	float startTime = -1f;
	float endTime = 0;

	static Dictionary<string,bool> isKeyDownArray = new Dictionary<string,bool>();

	void Awake() {
		untypedText = getStudyText();
		typedTextbox = GameObject.Find("TypedText").GetComponent("Text") as Text;
		untypedTextbox = GameObject.Find("UntypedText").GetComponent("Text") as Text;
		results = GameObject.Find ("Results").GetComponent ("Text") as Text;
		untypedTextbox.text = untypedText;

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

	// Use this for initialization
	void Start () {
		Debug.Log(getStudyText());
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (studyFinished || studyType == StudyType.ENGLISH_VIRTUAL || studyType == StudyType.LATIN_VIRTUAL) {
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

	}

	private string getStudyText() {
		switch(studyType) {
			case StudyType.ENGLISH_PHYSICAL:
			return "and how nobly it raises our conceit of the mighty, misty monster, to behold him solemnly s";
			//return "and how nobly it raises our conceit of the mighty, misty monster, to behold him solemnly sailing through a calm tropical sea; his vast, mild head overhung by a canopy of vapour, en";
			
			case StudyType.ENGLISH_VIRTUAL:
			return "reckoning the largest sized sperm whale's tail to begin at that point of the trunk where i";
			//return "reckoning the largest sized sperm whale's tail to begin at that point of the trunk where it tapers to about the girth of a man, it comprises upon its upper surface alone, an area o";
			
			case StudyType.LATIN_PHYSICAL:
			return "consectetur adipiscing elit. nunc a quam elementum velit aliquam porta. nunc eu blandit au";
			//return "consectetur adipiscing elit. nunc a quam elementum velit aliquam porta. nunc eu blandit augue, a tempus risus. etiam dignissim porttitor neque, ut ullamcorper lectus ullamcorper at";
			
			case StudyType.LATIN_VIRTUAL:
			return "nam dictum ligula nisl, nec dapibus massa fermentum a. phasellus et eleifend est, vel rutr";
			//return "nam dictum ligula nisl, nec dapibus massa fermentum a. phasellus et eleifend est, vel rutrum nibh. cras sit amet sagittis quam, ut accumsan magna. integer sit amet eros in nisl ege";
		}
		return "the quick, brown fox jumped over the lazy, black dog's tail.";
	}

	public void updateStudyText(char keyPressed) {
		//Debug.Log("key pressed: " + keyPressed + "|");
		if (startTime < 0) startTime = Time.time;
		if (untypedText.Length == 0) return;

		if (keyPressed == untypedText[0]) {
			typedText += keyPressed;
			untypedText = untypedText.Substring(1);
			typedTextbox.text = typedText;
			untypedTextbox.text = untypedText;
		} else {
			mistakeCount++;
		}

		if (untypedText.Length == 0) {
			endTime = Time.time;
			studyFinished = true;
			displayResults();
			Debug.Log ("Study over!");
			Debug.Log ("Mistake count: " + mistakeCount);
			Debug.Log("Time taken (in seconds): " + (endTime - startTime));
		}
	}

	private void onKeyDown(KeyCode key) {
		string keyPressed = getStringRepresentationOfKey(key);

		if (isKeyDownArray[keyPressed]) {
			return;
		} else if (Input.GetKeyDown(key)){
			updateStudyText(keyPressed.ToLower()[0]);
			Debug.Log("Key down: " + key.ToString());
			isKeyDownArray[keyPressed] = true;;
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

	private void displayResults() {
		results.text = ("RESULTS:: m: " + mistakeCount + "   t: " + (endTime - startTime));
	}
}
