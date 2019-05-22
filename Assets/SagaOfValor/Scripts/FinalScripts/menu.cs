using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour {

	private bool canContinue = true;
	
	void Start () {
		string checkLevelName = PlayerPrefs.GetString("savedLevel");
		if(checkLevelName == null || checkLevelName == ""){
			canContinue = false;
		}
	}
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}
	public void newGame () {
		PlayerPrefs.DeleteAll();
		Application.LoadLevel("Level1");
	}
	
	public void continueGame () 
	{
		if(canContinue){
			string levelName = PlayerPrefs.GetString("savedLevel");
			SceneManager.LoadScene(levelName);
		}
	}
	public void About () 
	{
		SceneManager.LoadScene("About");
	}
	public void Back()
	{
		SceneManager.LoadScene("Menu");
	}
}
