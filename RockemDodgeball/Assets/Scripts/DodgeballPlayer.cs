using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeballPlayer : MonoBehaviour
{
    public bool DEBUG = false;
    public int id;
    public int health = 100;
    public string playerName;
    public string team;

    public void ApplyDamage(int damage)
    {
        if (DEBUG) Debug.Log("Applied damage!");
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("DEAD!");
    }
}
