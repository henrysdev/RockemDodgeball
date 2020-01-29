using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodgeball : MonoBehaviour
{
    public int id;
    public int damage = 30;
    public Vector3 launchPosition;
    public Quaternion launchRotation;
    public Vector3 launchVelocity;
    public DodgeballPlayer thrower;
    public string team;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DodgeballPlayer hitPlayer = other.GetComponent<DodgeballPlayer>();
            //if (hitPlayer.team != thrower.team)
            //{
            hitPlayer.ApplyDamage(damage);
            //}
        }
    }
}
