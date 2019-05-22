using UnityEngine;
using System.Collections;

public class playerControls : MonoBehaviour
{
    public bool touch;
    public float walkSpeed = 14.0f;
    public float jumpHeight = 8.0f;
    public float fallLimit = -10;
    public AudioClip jumpSound;

    private RaycastHit hit;
    private float jumpCounter = 0.0f;
    private CharacterController controller;
    private Vector3 vel;
    private float origX;
    private bool canControl = true;
    private bool canCeiling = true;

    private int direction = 1;
    private Animator anim;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        origX = transform.localScale.x;
    }

    void Update()
    {
        //Apply Gravity
        if (!controller.isGrounded)
        {
            jumpCounter += Time.deltaTime;
            vel.y -= Time.deltaTime * 80;
        }
        else
        {
            jumpCounter = 0.0f;
            vel.y = -1;
        }

        //Flip Player based on velocity
        if (controller.velocity.x > 0)
        {
            transform.localScale = new Vector3(origX, transform.localScale.y, transform.localScale.z);
        }

        if (controller.velocity.x < 0)
        {
            transform.localScale = new Vector3(-origX, transform.localScale.y, transform.localScale.z);
        }

        if (canControl)
        {
#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
            if (Input.GetKey("a") || Input.GetKey("d") || Input.GetKey("left") || Input.GetKey("right"))
            {
                if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
                {
                    direction = -1;
                    vel.x = -walkSpeed;
                }

                if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
                {
                    direction = 1;
                    vel.x = walkSpeed;
                }
            }
            else
            {
                vel.x = 0;
            }

            if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow))
            {
                if (jumpCounter < 0.125f)
                {
                    vel.y = jumpHeight;
                    jumpCounter = 0.125f;
                    GetComponent<AudioSource>().PlayOneShot(jumpSound);
                }
            }
//#endif
            //if (touch)
           // {
#elif UNITY_IOS || UNITY_ANDROID || UNITY_EDITOR
                if (Input.touchCount > 0)
                {
                    foreach (Touch touch1 in Input.touches)
                    {
                        
                        if (touch1.position.x < Screen.width / 2 && touch1.phase ==TouchPhase.Moved)
                        {
                            if (touch1.deltaPosition.x > 2f )
                            {
                                direction = 1;
                                vel.x = walkSpeed;
                            }
                            
                            else if (touch1.deltaPosition.x < -2f)
                            {
                                direction = -1;
                                vel.x = -walkSpeed;
                                Debug.Log(touch1.phase +" : "+ touch1.deltaPosition);
                            }

                            if (touch1.phase == TouchPhase.Stationary)
                            {
                                vel.x = walkSpeed * direction;
                            }

                            Vector3 playerDir = transform.localScale;
                            playerDir.x *= direction;
                            transform.localScale = playerDir;
                        }
                    }
                }
                else
                {
                    vel.x = 0;
                }

                if (Input.touchCount > 0)
                {
                    foreach (Touch touch2 in Input.touches)
                    {
                        if (touch2.position.x > Screen.width / 4 * 3) //&& touch2.position.y < Screen.height / 3)
                        {
                            if (Input.touchCount == 1)
                            {
                                vel.x = 0;
                            }

                            if (jumpCounter < 0.125f)
                            {
                                vel.y = jumpHeight;
                                jumpCounter = 0.125f;
                                GetComponent<AudioSource>().PlayOneShot(jumpSound);
                            }
                        }
                    }
                }
#endif
            //}
            transform.localScale = new Vector3(direction,1,1);
            anim.SetFloat("Speed",Mathf.Abs(vel.x));
        }

        if ((controller.collisionFlags & CollisionFlags.Above) != 0 && canCeiling)
        {
            canCeiling = false;
            vel.y = 0;
            StartCoroutine(resetCeiling());
        }

        //Apply movement to player based on vel variable which is a Vector3.
        controller.Move(vel * Time.deltaTime);

        //reset level if player falls past Fall Limit
        if (transform.position.y < fallLimit)
        {
            string lvlName = Application.loadedLevelName;
            Application.LoadLevel(lvlName);
        }
    }

    public IEnumerator resetCeiling()
    {
        yield return new WaitForSeconds(0.25f);
        canCeiling = true;
    }

    void died()
    {
        canControl = false;
        vel.x = 0;
    }
}