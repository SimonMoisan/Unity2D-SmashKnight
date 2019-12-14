using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float maxHealthPoint;
    [Range(0, 1000)] public float healthPoint;
    public float movespeed = 5f;

    [Range(1,3)] public int verticalPosition = 2; //3 positions top to bottom : 1,2,3

    public Rigidbody2D rb;
    public Animator animator;
    public Transform[] bodyParts;

    private Vector2 movement;
    private string orientation = "Left";

    public float jumpSpeed;
    private float jumpTime;
    public float startJumpTime;
    public int direction; //0 : no direction, 1 : Up, 2 : Down

    public float startJumpCooldown;
    private float jumpCooldown;

    public int invicibilityTime;
    private bool isInvicible;

    private IEnumerator invincibilityCoroutine;

    void Start()
    {
        invincibilityCoroutine = Invicibility();
        healthPoint = maxHealthPoint;
        direction = 0;
        jumpTime = startJumpTime;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Movementx", movement.x);
        ManageSpriteOrientation();
        Jump();

        
    }

    //Better with physic
    void FixedUpdate()
    {
        //Movement
        if (direction == 0)
        {
            rb.MovePosition(rb.position + movement * movespeed * Time.fixedDeltaTime);
        }
    }

    public void TakeDamage(float amount)
    {
        if(!isInvicible)
        {
            invincibilityCoroutine = Invicibility();
            StartCoroutine(invincibilityCoroutine);
            healthPoint -= amount;
            if (healthPoint == 0)
            {
                Debug.Log("You are dead");
            }
        }
    }

    IEnumerator Invicibility()
    {
        isInvicible = true;
        int numberFlash = invicibilityTime * 5;

        for (int i = 0; i < numberFlash; i++)
        {
            for (int j = 0; j < bodyParts.Length; j++)
            {
                bodyParts[j].GetComponent<Renderer>().enabled = true;
            }
            yield return new WaitForSeconds(0.1f);
            for (int j = 0; j < bodyParts.Length; j++)
            {
                bodyParts[j].GetComponent<Renderer>().enabled = false;
            }
            yield return new WaitForSeconds(0.1f);
        }
        for (int j = 0; j < bodyParts.Length; j++)
        {
            bodyParts[j].GetComponent<Renderer>().enabled = true;
        }

        isInvicible = false;
    }

    private void Jump()
    {
        if (direction == 0)
        {
            //Give direction of the jump
            if (Input.GetKeyDown("z") && verticalPosition > 1 && jumpCooldown <= 0)
            {
                verticalPosition--;
                animator.SetTrigger("Jump");
                direction = 1;

                jumpCooldown = startJumpCooldown;
            }
            else if (Input.GetKeyDown("s") && verticalPosition < 3 && jumpCooldown <= 0)
            {
                verticalPosition++;
                animator.SetTrigger("Jump");
                direction = 2;

                jumpCooldown = startJumpCooldown;
            }
            else
            {
                jumpCooldown -= Time.deltaTime;
            }
        }
        else
        {
            if (jumpTime <= 0)
            {
                direction = 0;
                jumpTime = startJumpTime;

                //Change player layer
                if (verticalPosition == 1)
                {
                    changeLayers("Player Back");
                }
                else if (verticalPosition == 2)
                {
                    changeLayers("Player Middle");
                }
                else
                {
                    changeLayers("Player Front");
                }
            }
            else
            {
                jumpTime -= Time.deltaTime;

                //Top jump
                if (direction == 1)
                {
                    rb.velocity = Vector2.up * jumpSpeed;
                }
                //Bottom Jump
                else if (direction == 2)
                {
                    rb.velocity = Vector2.down * jumpSpeed;
                }
            }
        }
    }

    private void ManageSpriteOrientation()
    {
        if (orientation == "Left" && movement.x > 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            orientation = "Right";
        }
        else if (orientation == "Right" && movement.x < 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            orientation = "Left";
        }
    }

    void changeLayers(string newLayer)
    {
        for (int i = 0; i < bodyParts.Length; i++)
        {
            bodyParts[i].GetComponent<SpriteRenderer>().sortingLayerName = newLayer;
        }
    }
}
