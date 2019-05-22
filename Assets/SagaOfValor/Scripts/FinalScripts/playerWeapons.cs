using UnityEngine;
using System.Collections;

public class playerWeapons : MonoBehaviour
{
    //bullet objects found in the Prefabs folder
    public GameObject[] bullets;

    //damage for each bullet
    public float[] bulletsDamage;

    //speed for each bullet
    public float[] bulletsSpeed;

    //firerate for each bullet
    public float[] bulletsFirerate;

    //spawn position of bullet
    public Transform spawnPosition;

    //sound of the bullet when fired
    public AudioClip bulletSound;

    //sound when you pick up a bullet upgrade. by default we just used the same sound as the heart pickup.
    public AudioClip pickupSound;

    //private variables used to control shooting bullets
    private float bulletCounter = 0.0f;
    private float bulletPos = 0.0f;
    private int weaponSet = 0;
    private GameObject currentBullet;
    private float currentSpeed;
    private float currentDamage;
    private float fireRate = 0.25f;
    private bool dead = false;
    private playerAnimator playerAnim;

    public bool touch;
    Animator anim;

    void Start()
    {
        //when the game starts we want to check to see what bullet the player was using last. This is also called when a pickup is hit.
        updateBulletType();

        playerAnim = GetComponent<playerAnimator>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //keep track of time so we know when a bullet can fire
        bulletCounter += Time.deltaTime;

        if (!dead)
        {
            //controls for shooting bullets for web versions of the game. These are the same as standalone, but are only compiled if its web
#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Space))
            {
                swordAttack();
                if (bulletCounter > fireRate)
                {
                    //playerAnim.isAtacking=true;

                    shootBullet();
                    swordAttack();
                }
            }


            //if(touch)
            //{
            //controls for shooting bullets for ios versions of the game. These are the same as android, but are only compiled if its ios
#elif UNITY_IOS || UNITY_ANDROID
			if(Input.touchCount > 0)
			{

				foreach(Touch touch1 in Input.touches) 
				{ 
					//2nd touch for jump button
					//if(touch1.position.x > Screen.width/2 && touch1.position.x < Screen.width/4*3)// && touch1.position.y < Screen.height/3)
    				if(touch1.position.x < Screen.width/2 && (touch1.phase ==TouchPhase.Began)) //&& touch1.position.x < Screen.width/4*3)// && touch1.position.y < Screen.height/3)
					{
						swordAttack();
						
						if(bulletCounter > fireRate)
						{
							shootBullet();
						}	
					}
				}
			}
#endif
        }

        //end of function update
    }

    void swordAttack()
    {
        anim.SetTrigger("Attack");
    }

    public bool isShooted()
    {
        print(bulletCounter > fireRate);
        return bulletCounter > fireRate;
    }

    void shootBullet()
    {
        anim.SetTrigger("Attack");
        Vector3 pos = new Vector3(bulletPos + transform.position.x, -0.25f + transform.position.y,
            0.01f + transform.position.z);
        GameObject bulletPrefab = Instantiate(currentBullet, pos, Quaternion.Euler(0, 180, 0)) as GameObject;
        bulletPrefab.SendMessage("getBulletDamage", currentDamage, SendMessageOptions.DontRequireReceiver);
        GetComponent<AudioSource>().PlayOneShot(bulletSound);
        
        if (spawnPosition.position.x > transform.position.x)
        {
            bulletPrefab.transform.GetComponent<Rigidbody>().velocity = new Vector3(currentSpeed, 0, 0);
        }
        else
        {
            bulletPrefab.transform.GetComponent<Rigidbody>().velocity = new Vector3(-currentSpeed, 0, 0);
        }

        bulletCounter = 0.0f;
        print("CD:  " + currentDamage);
    }

    void updateBulletType()
    {
        var getSet = PlayerPrefs.GetInt("weaponset");
        if (getSet >= bullets.Length)
        {
            getSet = bullets.Length - 1;
        }

        currentBullet = bullets[getSet];
        fireRate = bulletsFirerate[getSet];
        currentSpeed = bulletsSpeed[getSet];
        currentDamage = bulletsDamage[getSet];
        print(getSet + "    " + currentDamage);
    }


    //this will happen if a trigger collider hits it. we check the tag though so only objects tagged with pickup will make this happen. it upgrades the weapons on pickup.
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "pickup")
        {
            Destroy(other.gameObject);
            GetComponent<AudioSource>().PlayOneShot(pickupSound);
            if (weaponSet < bullets.Length - 1)
            {
                weaponSet = PlayerPrefs.GetInt("weaponset") + 1;
                PlayerPrefs.SetInt("weaponset", weaponSet);
                updateBulletType();
            }
        }
    }

    void died()
    {
        dead = true;
    }
}