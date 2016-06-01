using System;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public const int CorspeHealth = 1000;
    public const int CorspeDOT = 1;
    public int Health = 100;
    public int MaxHealth = 100;
    public GameObject DeathSpawn;
    public event Action OnDeath;
    public bool Dead;
    public Rigidbody body;
    public Animator anim;

    public virtual void DealDamage(int amount)
    {
        Health -= amount;
        if (Health < 0)
            Die();
    }

    public virtual void Update()
    {
        if (Dead)
            DealDamage(CorspeDOT);
    }

    public virtual void Die()
    {
        if (Dead)
        {
            Destroy(gameObject);
            return;
        }
        StopAllCoroutines();
        if (DeathSpawn != null)
            Instantiate(DeathSpawn, transform.position, transform.rotation);
        if (OnDeath != null)
            OnDeath();
        Health = CorspeHealth;
        body.isKinematic = false;
        body.useGravity = true;
        body.AddExplosionForce(100, transform.position - Vector3.down, 1);
        anim.Stop();
    }
}