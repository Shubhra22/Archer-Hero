using UnityEngine;
using System.Collections;

public class playerAnimator : MonoBehaviour {

	//here we have all of the player's textures to animate him running and being idle. jumping just uses the idle texture.
	public Sprite[] idle;
	public float idleFrameRate = 8.0f;
	public Sprite[] run;
	public float runFrameRate = 8.0f;
	public Sprite[] jump;
	public float jumpFrameRate = 8.0f;
	public Sprite[] attack;
	public float attackFrameRate = 8.0f;

	public float frameRate = 8;

	//here are some private variables we use to help animate the character.
	private CharacterController controller;
	private float counter = 0.0f;
	private int i = 0;
	private SpriteRenderer rend;
	private bool isJumping = false;
	public bool isAtacking = false;
	private playerWeapons weapons;

	Animator anim;
	
	void Start () {
		controller = GetComponent<CharacterController>();
		rend = GetComponent<SpriteRenderer>();
		weapons = GetComponent<playerWeapons>();

		anim = GetComponent<Animator>();
	}
	
	void Update () 
	{
		//Check velocity to see if player should do idle or run animation
		float xVelocity = controller.velocity.x;
		float yVelocity = controller.velocity.y;
		if(xVelocity < 0)
		{
			xVelocity *= -1;
		}

		anim.SetFloat("Speed", xVelocity);

		if(!controller.isGrounded && !isAtacking)
		{
			anim.SetBool("Jump",true);

		}
		if(controller.isGrounded)
		{
			anim.SetBool("Jump",false);
		}

	}
	
}
