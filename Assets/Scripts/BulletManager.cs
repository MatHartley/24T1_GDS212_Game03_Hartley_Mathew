using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [Header("Bullet Stats")]
    [SerializeField] private int bulletHealth;
    [SerializeField] private int bulletDamage;

    [Header("Animation")]
    [SerializeField] private Animator animator;
    private float timeToDeath = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DealDamage(collision.gameObject);
    }
    private void DealDamage(GameObject target)
    {
        if (target.tag == "Bullet")
        {
            target.GetComponent<BulletManager>().bulletHealth -= bulletDamage;
        }

        if (target.tag == "Tank")
        {
            target.GetComponent<TankManager>().tankHealth -= bulletDamage;
            bulletHealth = 0;
        }

        if (target.tag == "Wall")
        {
            GameObject.Find("GameManager").GetComponent<WallManager>().wallHealth -= bulletDamage;
            bulletHealth = 0;
        }
    }

    private void Start()
    {
        animator.SetBool("isDead", false);
        animator.SetBool("inFlight", true);
    }

    private void Update()
    {
        if (bulletHealth <= 0)
        {
            animator.SetBool("isDead", true);
            timeToDeath -= Time.deltaTime;
            GetComponent<Collider2D>().enabled = false;
        }

        if (timeToDeath <= 0)
        {
            BulletDeath();
        }
    }

    private void BulletDeath()
    {
            Destroy(this.gameObject);
    }
}
