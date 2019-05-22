using UnityEngine;
using System.Collections;

public class levelManager : MonoBehaviour {

	public string nextLevel;

	private Transform enemy;
	
	void Start () {
		//we want to save progress so we save the name of the level so players can click the continue button
		PlayerPrefs.SetString("savedLevel", Application.loadedLevelName);
		//enemy = GameObject.FindGameObjectWithTag("enemy").transform;

	}

	void Update()
	{

	}
	//check to see if something runs into it and checks only upon enter, not constant. Constant would be OnTriggerStay
	void OnTriggerEnter (Collider other)
	{
		if(other.tag == "Player"){
			Application.LoadLevel(nextLevel);
		}
	}
}
