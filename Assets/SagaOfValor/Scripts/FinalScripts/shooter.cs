using UnityEngine;
using System.Collections;

public class shooter : MonoBehaviour {

	//here are the public variables for the shooter enemy. these can be changed in the inspector.
	public float runSpeed = 4.0f;
	//the sound when he shoots a bullet
	public AudioClip shootSound;
	//the textures that make him look like an enemy
	public Sprite[] run;
	public Sprite[] shoot;
	//the bullet he shoots
	public GameObject enemyBullet;
	public float bulletSpeed = 16.0f;
	public Transform bulletPosition;
	public float maxDistance;
	public float shootingDistance;
	
	//private variables that help with animating the enemy
	private CharacterController controller;
	private float counter = 0.0f;
	private int i = 0;
	private GameObject target;
	private float frameRate = 8.0f;
	private bool shooting = false;
	private SpriteRenderer rend;
	private float origX;
	private Vector3 vel;
	
	//we want to use the player as a reference for animating and giving a simple AI.
	void Start () {
		controller = GetComponent<CharacterController>();
		rend = GetComponent<SpriteRenderer>();
		target = GameObject.Find("Player");
		Physics.IgnoreCollision(target.GetComponent<Collider>(), GetComponent<Collider>());
		origX = transform.localScale.x;
		//here it makes it ignore other enemy colliders so they can't get caught on each other.
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
		foreach(GameObject en in enemies)  {
			if (en.GetComponent<Collider>() != GetComponent<Collider>()) {
				Physics.IgnoreCollision(GetComponent<Collider>(), en.GetComponent<Collider>());
			}
		}
	}
	
	void Update () {
		//Apply Gravity
		if(!controller.isGrounded){
			vel.y -= Time.deltaTime*80;
		}else{
			vel.y = -1;
		}
		//here we check the distance away the player is from the enemy
		float distance = target.transform.position.x - transform.position.x;
		float ydistance = target.transform.position.y - transform.position.y;
		if(distance < 0){
			distance *= -1;
		}
		if(ydistance < 0){
			ydistance *= -1;
		}
		if(target.transform.position.x > transform.position.x){
			transform.localScale = new Vector3(-origX,transform.localScale.y,transform.localScale.z);
		}
		if(target.transform.position.x < transform.position.x){
			transform.localScale = new Vector3(origX,transform.localScale.y,transform.localScale.z);
		}
		//print(distance);
		//if the player is close enough. the enemy can start animating and doing its thing.
		if(distance < maxDistance && ydistance < 8)
		{

			counter += Time.deltaTime*frameRate;
			if(distance > shootingDistance && distance < maxDistance && !shooting)
			{
				if(target.transform.position.x < transform.position.x){
					vel.x = -runSpeed;
				}
				if(target.transform.position.x > transform.position.x){
					vel.x = runSpeed;
				}
				if(counter > i && i < run.Length){
					rend.sprite = run[i];
					i += 1;
				}
				if(counter > run.Length){
					counter = 0.0f;
					i = 0;
				}
			}
			if(shooting){
				vel.x = 0;
				if(counter > i && i < shoot.Length){
					rend.sprite = shoot[i];
					i += 1;
				}
			}
			//the enemy will shoot a bullet only if the player is close enough and shooting is indeed false.
			if(distance < shootingDistance && shooting == false){
				StartCoroutine(shootBullet());
			}
		}
		
		//if the enemy falls down a hole, we want to destroy it so it doesn't exist for no reason.
		if(transform.position.y < -10){
			Destroy(gameObject);
		}
		
		//Apply movement to player based on vel variable which is a Vector3.
		controller.Move(vel*Time.deltaTime);
	}
	
	//if the shooter wants to shoot a bullet, then this function is called.
	public IEnumerator shootBullet () {
		vel.x = 0;
		shooting = true;
		counter = 0.0f;
		i = 0;
		//we wait for a bit before the shot fires so that the player can see he's about to do it
		yield return new WaitForSeconds(0.5f);
		//play the shot sound
		GetComponent<AudioSource>().PlayOneShot(shootSound);
		//spawn the bullet
		GameObject bullet = Instantiate(enemyBullet, bulletPosition.position, Quaternion.Euler(0,0,0)) as GameObject;
		//set velocity to the bullet
		if(bulletPosition.position.x < transform.position.x){
			bullet.GetComponent<Rigidbody>().velocity = new Vector3(bulletSpeed,0,0);
		}else{
			bullet.GetComponent<Rigidbody>().velocity = new Vector3(-bulletSpeed,0,0);
		}
		//then pause again before the enemy is ready to fire again.
		yield return new WaitForSeconds(0.5f);
		shooting = false;
	}
}
