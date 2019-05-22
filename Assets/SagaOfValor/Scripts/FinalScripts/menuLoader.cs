using UnityEngine;
using System.Collections;

//This is in the Loader scene and will bring us to the menu right away. 
//This is so we can carry the music manager through the entire game.
//this also sets the target framerate to 60. Mobile versions usually are set to 30 by default so we want to make sure that doesn't happen.


public class menuLoader : MonoBehaviour {
	void Start () {
		Application.targetFrameRate = 60;
		Application.LoadLevel("menu-cs");
	}
}
