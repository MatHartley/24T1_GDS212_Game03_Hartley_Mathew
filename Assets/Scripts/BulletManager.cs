using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [Header("Bullet Stats")]
    [SerializeField] private int bulletHealth;
    [SerializeField] private int bulletDamage;
    public bool isMovingUp;
    private bool bulletDying = false;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    [Header("SFX")]
    [SerializeField] private AudioSource bulletPopSFX;

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

        if (target.tag == "Turret" && !isMovingUp)
        {
            target.GetComponent<TankManager>().tankHealth -= bulletDamage;
            bulletHealth = 0;
        }
    }

    private void Start()
    {
        animator.SetBool("isDead", false);
        animator.SetBool("inFlight", true);

        bulletPopSFX = GameObject.Find("BulletPopSFX").GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (bulletHealth <= 0 && !bulletDying)
        {
            BulletDeath();
            bulletDying = true;
        }
    }

    private void BulletDeath()
    {
        bulletPopSFX.Play();
        animator.SetBool("isDead", true);
        GetComponent<Collider2D>().enabled = false;
        Destroy(this.gameObject, 1);
    }
}
