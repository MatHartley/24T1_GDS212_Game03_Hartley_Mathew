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
    [SerializeField] private float timeToDeath = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DealDamage(collision.gameObject);
    }
    public void DealDamage(GameObject target)
    {
        if (target.tag == "Bullet")
        {
            target.GetComponent<BulletManager>().bulletHealth -= bulletDamage;
        }

        if (target.tag == "Tank")
        {
            //target.GetComponent<TankManager>().tankHealth -= bulletDamage;
        }

        if (target.tag == "Wall")
        {
            //target.GetComponent<WallManager>().wallHealth -= bulletDamage;
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
            if (timeToDeath <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
