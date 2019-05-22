using UnityEngine;
using System.Collections;

public class GUIRescaler : MonoBehaviour {

	private Component getTxt;
	private Component getTxtr;
	private float resX;
	private float resY;
	private float origResX;
	private float origResY;
	private float txtrX;
	private float txtrY;

	// Use this for initialization
	void Start () {
		getTxt = transform.GetComponent<GUIText>();
		getTxtr = transform.GetComponent<GUITexture>();
		if( getTxtr == null && getTxt == null ){
			Debug.Log ("No GUIText or GUITexture exists on: " + transform.gameObject.name);
		}

	}
	
	// Update is called once per frame
	void Update () {

		if( Screen.width != origResX || Screen.height != origResY ){
			
			origResX = Screen.width;
			origResY = Screen.height;
			
			if( getTxt != null ){
				resX = Screen.width;
				resY = Screen.height;
				transform.localScale = new Vector3 ( transform.localScale.x,transform.localScale.x * ( resX/resY ));
			}
			if( getTxtr != null ){
				resX = Screen.width;
				resY = Screen.height;
				txtrX = transform.GetComponent<GUITexture>().texture.width;
				txtrY = transform.GetComponent<GUITexture>().texture.height;
				transform.localScale = new Vector3 ( transform.localScale.x,(transform.localScale.x * (resX/resY))/(txtrX/txtrY));
			}
		}

	}
}
