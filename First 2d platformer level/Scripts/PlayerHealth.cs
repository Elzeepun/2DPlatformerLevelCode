using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;    
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //Hurt animation

        if(currentHealth <= 0)
        {
            Die();
        }
    }
   

    void Die()
    {
        Debug.Log("Player is dead!");

        //Death animation

        Destroy(this.gameObject);
        LevelManager.instance.Respawn();
    }
}
