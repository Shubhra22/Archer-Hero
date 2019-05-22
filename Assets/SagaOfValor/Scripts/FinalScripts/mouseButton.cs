using UnityEngine;
using System.Collections;

public class mouseButton : MonoBehaviour {

	public string sendMessageUp = "";
	public bool standalone = true;
	public bool mobile = true;


	private bool over = false;

	void Start () {
		//#if UNITY_STANDALONE || UNITY_WEBPLAYER
		if(!standalone){
			gameObject.SetActive(false);
		}
	//	#endif

//		if(!mobile){
//			gameObject.SetActive(false);
//		}

	}
	
	void OnMouseEnter () {
		over = true;
	}
	
	void OnMouseExit () {
		over = false;
	}
	
	void Update () 
	{
		if(Input.GetMouseButtonUp(0))
		{
			if(over)
			{
				print ("asdf");
				SendMessageUpwards(sendMessageUp, SendMessageOptions.DontRequireReceiver);
			}
		}
	}
}
