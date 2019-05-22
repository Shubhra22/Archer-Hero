using UnityEngine;
using System.Collections;

public class deathAnimation : MonoBehaviour {

	//here are the star textures that make a "stylized" explosion. this is used on both enemies and the player on death.
	public Sprite[] deathSprites;
	//we also want to multiply the counter by a number to get a specific framerate
	public float frameRate = 12.0f;
	//death sound
	public AudioClip deathSound;
	//we want to set a counter so the animation can be based on time
	private float counter = 0.0f;
	private int i = 0;
	private SpriteRenderer rend;
	
	void Start () {
		rend = GetComponent<SpriteRenderer>();
		//play the death sound once as soon as this object is spawned
		GetComponent<AudioSource>().PlayOneShot(deathSound);
	}
	
	void Update () {
		//keeping track of time with counter
		counter += Time.deltaTime*frameRate;
		if(counter > i && i < deathSprites.Length){
			rend.sprite = deathSprites[i];
			i += 1;
		}
		//If animation finishes, we destroy the object
		if(counter > deathSprites.Length){
			Destroy(gameObject);
		}
	}
}
