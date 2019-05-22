using UnityEngine;
using System.Collections;

public class floater : MonoBehaviour {

	//public variables we use to make the floater characters
	//sound for when its hurt
	public AudioClip hurtSound;
	//the textures of the floater
	public Sprite[] floatAnimation;
	//animation frame rate
	public float frameRate = 6.0f;
	public float moveSpeed = 16.0f;
	public float moveAccuracy = 1.0f;

	public float moveRange;
	
	//private variables that we use to help animate the floater
	private CharacterController controller;
	private SpriteRenderer rend;
	private float counter = 0.0f;
	private int i = 0;
	private GameObject target;
	private float distance = 0.0f;
	private float ydistance = 0.0f;
	private float origX;

	Animator anim;
	
	//we use the player to help animate the floater enemy by using the player's position.
	void Start () 
	{
		
		anim = GetComponent<Animator>();
		controller = GetComponent<CharacterController>();
		rend = GetComponent<SpriteRenderer>();
		target = GameObject.Find("Player");
		Physics.IgnoreCollision(target.GetComponent<Collider>(), GetComponent<Collider>());
		origX = transform.localScale.x;
		//here it makes it ignore other enemy colliders so they can't get caught on each other.
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
		foreach(GameObject en in enemies)  
		{
			if (en.GetComponent<Collider>() != GetComponent<Collider>()) {
				Physics.IgnoreCollision(GetComponent<Collider>(), en.GetComponent<Collider>());
			}
		}


	}
	
	void Update () {
		//here we check the distance the player is from the floater.
		distance = target.transform.position.x - transform.position.x;
		ydistance = target.transform.position.y - transform.position.y;
		
		
		if(distance < 0){
			distance *= -1;
		}
		if(ydistance < 0)
		{
			ydistance*= -1;
		}
		//here we check if the floater is to the left or to the right of the player so we can decide which textures to use while he's floating to make him look like he's looking at the player
		if(controller.velocity.x > 0){
			transform.localScale = new Vector3(origX,transform.localScale.y,transform.localScale.z);
		}
		if(controller.velocity.x < 0){
			transform.localScale = new Vector3(-origX,transform.localScale.y,transform.localScale.z);
		}
		
		//if the player is close enough, start animating and moving.
		if(distance < 16 && ydistance < 8)
		{
			anim.SetBool("floatAnim",true);
		}
		
		//this is how we get the floating to always move towards the player if he's close enough.
		if(target != null && distance < moveRange){
			var dir = target.transform.position - transform.position;
			dir = dir.normalized;
			controller.Move(Vector3.Lerp(controller.velocity, dir*moveSpeed, moveAccuracy*Time.deltaTime)*Time.deltaTime);
			//controller.Move(dir*moveSpeed*Time.deltaTime);

		}
	}
}
