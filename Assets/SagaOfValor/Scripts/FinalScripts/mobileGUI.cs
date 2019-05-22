using UnityEngine;
using System.Collections;

public class mobileGUI : MonoBehaviour {
	//all this script does is check to see if the platform the game is running on is mobile or not. If not, it will not show the mobile controls. If it is mobile, the control images will be on.
	//these are attached to the arrows and buttons GUITextures.
	void Start () {
		#if UNITY_WEBPLAYER
			guiTexture.enabled = false;
		#endif

		#if UNITY_STANDALONE
			guiTexture.enabled = false;
		#endif

		#if UNITY_IOS
			guiTexture.enabled = true;
		#endif

		#if UNITY_ANDROID
			GetComponent<GUITexture>().enabled = true;
		#endif
	}
}
