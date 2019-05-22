using UnityEngine;
using System.Collections;

public class jumper : MonoBehaviour {

	//heres some variables that we use to make the jumper character
	//here are the textures that make the jumper look like an enemy
	public Sprite[] idle;
	public float idleFrameRate = 8.0f;
	public Sprite[] jump;
	public float jumpFrameRate = 8.0f;
	public float jumpRate = 2.0f;
	public float jumpHeight = 19.0f;
	
	//here are some private variables that we use to help animate the enemy
	private CharacterController controller;
	private SpriteRenderer rend;
	private GameObject target;
	private float jumpCounter = 0.0f;
	private Vector3 vel;
	private bool jumping = true;
	private float distance = 0.0f;
	private float ydistance = 0.0f;
	private float playerDist = 0.0f;
	private float origX = 0.0f;
	private float counter = 0.0f;
	private int i = 0;

	public float MaxDistance = 10;
	
	
	void Start () {
		controller = GetComponent<CharacterController>();
		rend = GetComponent<SpriteRenderer>();
		origX = transform.localScale.x;
		//here we set the player as the target to help give the jumper behaviors. Pretty much simple AI.
		target = GameObject.Find("Player");
		Physics.IgnoreCollision(target.GetComponent<Collider>(), GetComponent<Collider>());
		//here it makes it ignore other enemy colliders so they can't get caught on each other.
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
		foreach(GameObject en in enemies)  {
			if (en.GetComponent<Collider>() != GetComponent<Collider>()) {
				Physics.IgnoreCollision(GetComponent<Collider>(), en.GetComponent<Collider>());
			}
		}
	}
	
	void Update () {
		//check if the controller is grounded, otherwise apply gravity with time.deltatime
		if(controller.isGrounded){
			vel.y = -1;
			if(jumping){
				jumping = false;
			}
			vel.x = 0;
		}else{
			vel.y -= Time.deltaTime*80;
		}
		//here we check the distance the player is away from the jumper
		distance = target.transform.position.x - transform.position.x;	
		ydistance = target.transform.position.y - transform.position.y;
		if(distance < 0){
			distance *= -1;
		}
		if(ydistance < 0){
			ydistance*= -1;
		}
		if(target.transform.position.x < transform.position.x){
			transform.localScale = new Vector3(-origX,transform.localScale.y,transform.localScale.z);
		}
		if(target.transform.position.x > transform.position.x){
			transform.localScale = new Vector3(origX,transform.localScale.y,transform.localScale.z);
		}
		
		//if the player is close enough, the enemy can now do its thing like animate and jump towards the player
		if(distance < MaxDistance && ydistance < 8){
			jumpCounter += Time.deltaTime;
			if(jumpCounter < jumpRate && !jumping){
				counter += Time.deltaTime*idleFrameRate;
				if(counter > i && i < idle.Length){
					rend.sprite = idle[i];
				}
				if(counter > idle.Length){
					counter = 0.0f;
					i = 0;
				}
			}
			
			//here we jump towards the player at a velocity relative to distance. only if the jumpCounter hits jumpspeed. otherwise the enemy will jump all over the place like crazy and likely be useless... and funny.
			if(jumpCounter > jumpRate){
				playerDist = target.transform.position.x-transform.position.x;
				jumpCounter = 0.0f;
				jumping = true;
				vel.y = jumpHeight;
			}
		}
		
		if(jumping){
			counter += Time.deltaTime*jumpFrameRate;
			if(counter > i && i < jump.Length){
				rend.sprite = jump[i];
			}
		}
		
		//here we make the jump move towards the player
		if(controller.velocity.y > 0.5f){
			vel.x = playerDist*1.5f;
		}
		
		//if the jumper falls down a hole we want to destroy it so that it doesn't continue to exist for no reason. he's not coming back.
		if(transform.position.y < -10){
			Destroy(gameObject);
		}
		//Apply movement to the enemy
		controller.Move(vel*Time.deltaTime);
	}
}
