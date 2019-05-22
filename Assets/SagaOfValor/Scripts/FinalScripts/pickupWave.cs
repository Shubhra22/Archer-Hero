using UnityEngine;
using System.Collections;

public class pickupWave : MonoBehaviour {
	
	//this script gives a little wave to objects based on Sin. These are only attached to the pickup objects
	
	private float yPosition;
	
	void Start () {
		yPosition = transform.position.y;
	}
	
	void Update () {
		if(Time.timeScale == 1){
			transform.position = new Vector3(transform.position.x, yPosition + Mathf.Sin(Time.time * 6)/10,transform.position.z);
		}
	}
}