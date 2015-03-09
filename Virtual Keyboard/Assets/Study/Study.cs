using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Study : MonoBehaviour {
	string untypedText;
	string typedText;
	int mistakeCount;


	private enum StudyType {
		ENGLISH_PHYSICAL,
		ENGLISH_VIRTUAL,
		LATIN_PHYSICAL,
		LATIN_VIRTUAL
	}

	private UnityEngine.UI.Text untypedTextbox;
	private UnityEngine.UI.Text typedTextbox;

	[SerializeField]
	private StudyType studyType;

	void Awake() {
		untypedText = getStudyText();
		typedTextbox = GameObject.Find("TypedText").GetComponent("Text") as Text;
		untypedTextbox = GameObject.Find("UntypedText").GetComponent("Text") as Text;
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
	}

	// Use this for initialization
	void Start () {
		Debug.Log(getStudyText());
	}
	
	// Update is called once per frame
	void Update () {
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

	}
	
	private string getStudyText() {
		switch(studyType) {
			case StudyType.ENGLISH_PHYSICAL:
			return "and how nobly it raises our conceit of the mighty, misty monster, to behold him solemnly sailing through a calm tropical sea; his vast, mild head overhung by a canopy of vapour, engendered by his incommunicable contemplations, and that vapour—as you will sometimes see it—glorified by a rainbow, as if Heaven itself had put its seal upon his thoughts. For, d'ye see, rainbows do not visit the clear air; they only irradiate vapour. And so, through all the thick mists of the dim doubts in my mind, divine intuitions now and then shoot, enkindling my fog with a heavenly ray. And for this I thank God; for all have doubts; many deny; but doubts or denials, few along with them, have intuitions. Doubts of all things earthly, and intuitions of some things heavenly; this combination makes neither believer nor infidel, but makes a man who regards them both with equal eye.";
			break;

			case StudyType.ENGLISH_VIRTUAL:
			return "Reckoning the largest sized Sperm Whale's tail to begin at that point of the trunk where it tapers to about the girth of a man, it comprises upon its upper surface alone, an area of at least fifty square feet. The compact round body of its root expands into two broad, firm, flat palms or flukes, gradually shoaling away to less than an inch in thickness. At the crotch or junction, these flukes slightly overlap, then sideways recede from each other like wings, leaving a wide vacancy between. In no living thing are the lines of beauty more exquisitely defined than in the crescentic borders of these flukes. At its utmost expansion in the full grown whale, the tail will considerably exceed twenty feet across. Nor does this—its amazing strength, at all tend to cripple the graceful flexion of its motions; where infantileness of ease undulates through a Titanism of power.";
			break;

			case StudyType.LATIN_PHYSICAL:
			return "Consectetur adipiscing elit. Nunc a quam elementum velit aliquam porta. Nunc eu blandit augue, a tempus risus. Etiam dignissim porttitor neque, ut ullamcorper lectus ullamcorper at. Fusce vulputate mattis magna luctus fringilla. Aliquam erat volutpat. Nulla ac diam arcu. Suspendisse vel efficitur justo. In condimentum eget metus vel lobortis. Cras vestibulum at sapien sed lobortis. Proin accumsan, odio ac placerat tempor, lectus metus consectetur tortor, ut vehicula augue lacus sed urna. Pellentesque nisl est, blandit id malesuada eget, tristique in sem. Fusce eget enim id velit accumsan cursus sit amet non ligula. Integer aliquet libero eu porta faucibus. Quisque sed enim semper eros scelerisque scelerisque. Sed mauris enim, bibendum id libero at, vulputate porta elit.";
			break;

			case StudyType.LATIN_VIRTUAL:
			return "Nam dictum ligula nisl, nec dapibus massa fermentum a. Phasellus et eleifend est, vel rutrum nibh. Cras sit amet sagittis quam, ut accumsan magna. Integer sit amet eros in nisl egestas mattis. Nulla auctor aliquam dui ut mattis. Suspendisse pharetra accumsan dui eget fermentum. Donec id leo vel purus pellentesque aliquam. Sed consectetur leo sed odio fermentum, a faucibus enim efficitur. Nulla eu imperdiet turpis, vel rhoncus magna. Phasellus non ullamcorper augue. Maecenas a nibh massa. Etiam ex mi, ornare lacinia mi ut, commodo interdum nisi. Pellentesque vel lorem sit amet lorem maximus efficitur eu molestie sem. Ut ullamcorper rutrum ligula sit amet maximus. Maecenas tellus sem, mollis sit amet urna ac, eleifend eleifend ex. Ut a enim ut dui posuere fermentum ac at odio.";
			break;
		}
		return "Dewey Cox";
	}

	private void updateStudyText(char keyPressed) {
		// @TODO: make characters appear in typed text bod
		////      - listen for space key
		if (keyPressed == untypedText[0]) {
			typedText += keyPressed;
			untypedText = untypedText.Substring(1);
			typedTextbox.text = typedText;
			untypedTextbox.text = untypedText;
		} else {
			mistakeCount++;
		}
	}

	static Dictionary<string,bool> isKeyDownArray = new Dictionary<string,bool>();
	private void onKeyDown(KeyCode key) {
		if (isKeyDownArray[key.ToString()]) {
			return;
		} else if (Input.GetKeyDown(key)){
			updateStudyText(key.ToString().ToLower()[0]);
			Debug.Log("Key down: " + key.ToString());
			isKeyDownArray[key.ToString()] = true;;
		}
	}

	private void onKeyUp(KeyCode key) {
		if (!isKeyDownArray[key.ToString()]) {
			return;
		} else if (Input.GetKeyUp(key)){
			isKeyDownArray[key.ToString()] = false;
		}
	}
}
