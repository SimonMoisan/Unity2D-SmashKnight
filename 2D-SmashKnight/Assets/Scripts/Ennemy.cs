using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    public float health;
    public float attackDamage;
    public float maxMovespeed;
    private float actualMovespeed;
    public Vector2 movement;
    public string orientation;
    public int verticalPosition;

    public float startDazedTime;
    private float dazedTime = 0;

    private Rigidbody2D rb;
    public ParticleSystem bloodSplash;
    public Transform[] bodyparts;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(dazedTime <= 0)
        {
            actualMovespeed = maxMovespeed;
        }
        else
        {
            actualMovespeed = 1;
            dazedTime -= Time.deltaTime;
        }
    }

    //Better with physic
    void FixedUpdate()
    {
        //Movement
        rb.MovePosition(rb.position + movement * actualMovespeed  * Time.fixedDeltaTime);
    }

    public void TakeDamage(float damage)
    {
        bloodSplash.Emit(30);
        dazedTime = startDazedTime;
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        ParticleSystem go = Instantiate(bloodSplash, transform.position, Quaternion.identity);
        go.Play();
        Destroy(go,1f);
        Destroy(gameObject);
    }


    public void ChangeOrientation(string newOrientation)
    {
        if (newOrientation == "Right")
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            movement.x = 1;
            orientation = "Right";
        }
        else if(newOrientation == "Left")
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            movement.x = -1;
            orientation = "Left";
        }
    }

    public void setLayer(string newLayer, int newVerticalPosition)
    {
        verticalPosition = newVerticalPosition;
        for (int i = 0; i < bodyparts.Length; i++)
        {
            bodyparts[i].GetComponent<SpriteRenderer>().sortingLayerName = newLayer;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && verticalPosition == collision.GetComponent<PlayerMovement>().verticalPosition)
        {
            collision.GetComponent<PlayerMovement>().TakeDamage(attackDamage);
        }
    }
}
