using UnityEngine;
using System.Collections;

public class pauseManager : MonoBehaviour {

	public GUITexture pauseButton;
	public GUIText resumeButton;
	public GUIText menuButton;
	public GUIText quitButton;
	public GUITexture darken;
	
	private bool isPaused = false;
	
	void Start () {
		resumeButton.GetComponent<GUIText>().enabled = false;
		menuButton.GetComponent<GUIText>().enabled = false;
		quitButton.GetComponent<GUIText>().enabled = false;
		darken.GetComponent<GUITexture>().enabled = false;
	}
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			doPause();
		}
	}
	
	void doPause () {
		if(isPaused){
			Time.timeScale = 1.0f;
			resumeButton.GetComponent<GUIText>().enabled = false;
			menuButton.GetComponent<GUIText>().enabled = false;
			quitButton.GetComponent<GUIText>().enabled = false;
			darken.GetComponent<GUITexture>().enabled = false;
			isPaused = false;
		}else{
			Time.timeScale = 0.0f;
			resumeButton.GetComponent<GUIText>().enabled = true;
			menuButton.GetComponent<GUIText>().enabled = true;
			quitButton.GetComponent<GUIText>().enabled = true;
			darken.GetComponent<GUITexture>().enabled = true;
			isPaused = true;
		}
	}
	
	void doResume () {
		doPause();
	}
	
	void doMenu () {
		Application.LoadLevel("menu-cs");
	}
	
	void doQuit () {
		Application.Quit();
	}
}
