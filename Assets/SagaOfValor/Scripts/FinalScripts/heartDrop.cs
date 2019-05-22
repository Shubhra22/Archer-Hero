using UnityEngine;
using System.Collections;

public class heartDrop : MonoBehaviour {
	//this only controls a little velocity when a heart is spawned to give it some movement.
	void Start () {
		GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-6,6),Random.Range(4,8),0);
	}
}
