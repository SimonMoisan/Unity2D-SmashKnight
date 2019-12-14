using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyDestroyer : MonoBehaviour
{
    public EnnemyManager ennemyManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        addToSpawner(collision.GetComponent<Ennemy>());
        if(collision.gameObject.tag.Equals("Ennemy"))
        {
            Destroy(collision.gameObject);
        }
    }

    public void addToSpawner(Ennemy ennemy)
    {
        if(ennemy.orientation == "Right")
        {
            ennemyManager.ennemiesReachedRightDestroyer++;
        }
        else
        {
            ennemyManager.ennemiesReachedLeftDestroyer++;
        }
    }
}
