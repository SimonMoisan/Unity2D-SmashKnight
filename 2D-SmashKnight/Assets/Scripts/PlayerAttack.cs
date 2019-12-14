using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float damage;
    private float timeBtwAttacks;
    public float startTimeBtwAttacks;
    public Transform attackPos;
    public float attackRangeX;
    public float attackRangeY;
    public LayerMask whatIsEnnemy;

    public Animator animator;
    public PlayerMovement playerMovement;
    public CameraShake cameraShake;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBtwAttacks <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                animator.SetTrigger("Attack");
                //StartCoroutine(cameraShake.Shake(0.15f, 0.1f)); called during the animation
                //DealDamages(); called during the animation
                timeBtwAttacks = startTimeBtwAttacks;
            }
        }
        else
        {
            timeBtwAttacks -= Time.deltaTime;
        }
    }

    private void DealDamages()
    {
        Vector2 attackRange = new Vector2(attackRangeX, attackRangeY);
        Collider2D[] ennemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, attackRange, whatIsEnnemy);
        for (int i = 0; i < ennemiesToDamage.Length; i++)
        {
            if(ennemiesToDamage[i].GetComponent<Ennemy>() != null && ennemiesToDamage[i].GetComponent<Ennemy>().verticalPosition == playerMovement.verticalPosition)
            {
                ennemiesToDamage[i].GetComponent<Ennemy>().TakeDamage(damage);
            }
        }
    }

    private void Shake()
    {
        StartCoroutine(cameraShake.Shake(0.15f, 0.1f));
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 attackRange = new Vector3(attackRangeX, attackRangeY, 0);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, attackRange);
    }
}
