﻿using UnityEngine;
using System.Collections;

public class enemyBullet : MonoBehaviour {

	public int damage = 1;
	//this script only controls the life of the bullet. we make it disappear after 1 second. You don't want stray bullets floating around forever for no reason.
	public float bulletLife = 1.0f;
	//heres the counter variable
	private float lifeCounter = 0.0f;
	private float origX = 0.0f;
	
	void Start () {
		origX = transform.localScale.x;
	}
	
	void Update () {
		//flip the bullet depending on the velocity
		if(GetComponent<Rigidbody>().velocity.x < 0){
			transform.localScale = new Vector3(-origX,transform.localScale.y,transform.localScale.z);
		}
		if(GetComponent<Rigidbody>().velocity.x > 0){
			transform.localScale = new Vector3(origX,transform.localScale.y,transform.localScale.z);
		}
		//here we add time to the counter variable
		lifeCounter += Time.deltaTime;
		
		//when the counter is higher than 1 (1 second) it will destroy the bullet it is attached to.
		if(lifeCounter > bulletLife )
		{
			Destroy(gameObject);
		}
	}
	
	void OnTriggerEnter (Collider other){
		if(other.tag == "Player"){
			other.SendMessage("takeDamage", damage, SendMessageOptions.DontRequireReceiver);
			Destroy(gameObject);
		}
	}

	public void takeDamage(float amount)
	{
		//bulletLife -= amount;
		lifeCounter += amount;
	}
}
