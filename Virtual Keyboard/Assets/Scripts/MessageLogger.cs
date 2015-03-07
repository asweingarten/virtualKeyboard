using UnityEngine;
using System.Collections;

public class MessageLogger : MonoBehaviour {

	private static MessageLogger loggerInstance;

	public static MessageLogger Instance {
		get {
			if ( null == loggerInstance ) {
				loggerInstance = GameObject.FindObjectOfType<MessageLogger>();
				DontDestroyOnLoad( loggerInstance.gameObject );
			}
			return loggerInstance;
		}
		private set {
			loggerInstance = value;
			if ( null != value ) {
				DontDestroyOnLoad( loggerInstance.gameObject );
			}
		}
	}

	void Awake() {
		if ( null == Instance ) {
			Instance = this;
		} else {
			if ( this != Instance ) {
				Destroy( this.gameObject );
			}
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		updateText( System.DateTime.Now.ToLongTimeString() );
	}

	/// <summary>
	/// Updates the UI debug text.
	/// </summary>
	/// <param name="newText">New debug text.</param>
	public void updateText(string newText) {
		var textObject = gameObject.transform.GetChild(0);
		var textComponent = textObject.GetComponent<UnityEngine.UI.Text>();
		textComponent.text = newText;
	}
}
