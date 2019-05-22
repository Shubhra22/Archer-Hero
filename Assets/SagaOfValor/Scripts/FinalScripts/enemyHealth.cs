using UnityEngine;
using System.Collections;

public class enemyHealth : MonoBehaviour {

	//the death animation that spawns if he dies
	public GameObject deathAnim;
	//a heart that might drop when he dies
	public GameObject heartDrop;
	public float health = 6;
	public AudioClip hurtSound;
	
	private SpriteRenderer rend;
	private bool isDead = false;

	private float healthUI;
	static enemyHealth myInstance;
	public static enemyHealth Instance
	{
		get
		{
			if (myInstance == null)
				myInstance = FindObjectOfType(typeof(enemyHealth)) as enemyHealth;
			
			return myInstance;
		}
	}
	void Start () {
		rend = GetComponent<SpriteRenderer>();
		//healthUI = transform.Find("HealthCircleSlider").GetComponent<EnemyHealthUI>().HealthBar;
		
	}
	
	//here we manage health of the enemy when a bullet hits it. if its health is 0 it will spawn the death animation, possibly spawn a heart, then destroy itself.
	public void takeDamage (float amount) 
	{
		if(!isDead)
		{
			GetComponent<AudioSource>().PlayOneShot(hurtSound);
			healthUI = (health - amount) / health;
			transform.Find("HealthCircleSlider").GetComponent<EnemyHealthUI>().HealthBar = healthUI;
			health -= amount;
			if(health <= 0)
			{
				isDead = true;
				Instantiate(deathAnim, transform.position, Quaternion.Euler(0,180,0));
				int randNum = Random.Range(1,4);
				if(randNum == 2){
					Instantiate(heartDrop, transform.position, Quaternion.Euler(0,180,0));
				}
				Destroy(gameObject);
			}else{
				StartCoroutine(resetColor());
			}
		}
	}
	
	//called when hit to show that the enemy was hit
	public IEnumerator resetColor () {
		rend.color = new Vector4(1.0f,0.25f,0.25f,1.0f);
		yield return new WaitForSeconds (0.125f);
		rend.color = new Vector4(1.0f,1.0f,1.0f,1.0f);
	}

	void OnTriggerEnter(Collider coll)
	{
		if(coll.tag=="sword")
		{
			print(this.gameObject.GetComponent<enemyHealth>().health);
			takeDamage(2);
		}
	}
}
