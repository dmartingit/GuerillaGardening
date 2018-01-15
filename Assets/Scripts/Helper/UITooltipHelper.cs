using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITooltipHelper : MonoBehaviour {

	private string labelText;
	private Color labelColor = Color.white;
	private int labelFontSize = 24;
	private float labelFadingSpeed = 0.1f;
	private float labelAlpha;

	void Start () {
	}

	void Update () {
	}

	void OnGUI() {
		this.labelColor.a = this.labelAlpha;
		GUIStyle style = new GUIStyle ();
		style.normal.textColor = this.labelColor;
		style.alignment = TextAnchor.MiddleCenter;
		style.fontSize = this.labelFontSize;
		GUI.Label (new Rect (0, 0, Screen.width, Screen.height), this.labelText, style);
	}

	public void Draw() {
		StartCoroutine ("Fade");
	}

	public void SetText (string text) {
		this.labelText = text;
	}

	public void SetColor (Color color) {
		this.labelColor = color;
	}

	public void SetFontSize (int fontSize) {
		this.labelFontSize = fontSize;
	}

	public void SetFadingSpeed (float fadingSpeed) {
		this.labelFadingSpeed = fadingSpeed;
	}

	IEnumerator Fade() {
		for (var f = 1f; f >= 0f; f -= 0.05f) {
			this.labelAlpha = f;
			yield return new WaitForSeconds (this.labelFadingSpeed);
		}
		this.labelAlpha = 0f;
	}
}
