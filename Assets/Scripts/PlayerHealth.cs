using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    public Animator anim; //useful for triggering animation of death

    void Start() {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount) { //made to register damage, have any projectiles use this the "TakeDamage()" reference to register damage, use (whatever dmg number)
        currentHealth -= amount;

        if (currentHealth <= 0) {
            //you dead
            GameManager.RespawnPlayer(gameObject);
            //anim.SetBool("", true);   //if you have a command for death animation
        }
    }

    public void Heal(float amount) { //made to register healing 
        currentHealth += amount;

        if (currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }
    }
}
