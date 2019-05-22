using UnityEngine;
using System.Collections;

public class parallax : MonoBehaviour {

	//this is the parallax script that makes the background move slower than the objects in front. it just divides the matched transform position of the camera by 3.
	
	private GameObject target;
	
	void Start () {
		target = GameObject.Find("Main Camera");
	}
	
	void Update () {
		transform.position = new Vector3(target.transform.position.x/3,target.transform.position.y/3,transform.position.z);
	}
}
