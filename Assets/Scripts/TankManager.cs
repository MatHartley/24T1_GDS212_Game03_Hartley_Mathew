using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankManager : MonoBehaviour
{
    [Header("Tank Stats")]
    public int tankHealth;
    [SerializeField] private bool isStatic;
    [SerializeField] private float tankSpeed;
    [SerializeField] private int tankCredit;
    [SerializeField] private int tankScore;
    private bool tankDying = false;

    [Header("Tank Components")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePointA;
    [SerializeField] private Transform firePointB;
    [SerializeField] private Animator animator;
    private Rigidbody2D rigidBody;

    [Header("Shooting")]
    [SerializeField] private float cooldownTime;
    [SerializeField] private float cooldownCount;
    [SerializeField] private float shotSpeed;
    [SerializeField] private LayerMask tankMask;
    private bool swapPoint;

    [Header("SFX")]
    [SerializeField] private AudioSource tankMoveSFX;
    [SerializeField] private AudioSource tankShootSFX;
    [SerializeField] private AudioSource tankDeathSFX;

    [Header("Script Reference")]
    private ScoreManager scoreManager;

    // Start is called before the first frame update
    void Start()
    {
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        rigidBody.velocity = new Vector2(0, -tankSpeed);
        cooldownCount = 0;

        tankShootSFX = GameObject.Find("TankShootSFX").GetComponent<AudioSource>();
        tankDeathSFX = GameObject.Find("TankDeathSFX").GetComponent<AudioSource>();

        tankMoveSFX.Play();
    }

    // Update is called once per frame
    void Update()
    {
        cooldownCount += Time.deltaTime;

        //Raycast to check if there is another tank in front of them
        RaycastHit2D hit = Physics2D.Raycast(firePointA.position, Vector2.down, Mathf.Infinity, tankMask);
        //Debug.Log("Raycast Hit");

        //Dont fire if the Raycast hits something
        if ((cooldownCount >= cooldownTime) && (hit.collider == null))
        {
            FireTank();
        }

        if (tankHealth <= 0 && !tankDying)
        {
            TankDeath();
            tankDying = true;
        }

        if (!isStatic)
        {
            rigidBody.velocity = new Vector2(0, -tankSpeed);
        }

        if (Time.timeScale == 0f)
        {
            tankMoveSFX.Stop();
        }

    }

    public void FireTank()
    {
        if (!animator.GetBool("isDead"))
        {
            //Debug.Log("Firing Tank");
            GameObject shot = Instantiate(bulletPrefab) as GameObject;
            tankShootSFX.Play();

            if (swapPoint)
            {
                shot.transform.position = firePointA.transform.position;
            }
            else
            {
                shot.transform.position = firePointB.transform.position;
            }

            if (!isStatic)
            {
                shot.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -shotSpeed);
            }
            else 
            {
                shot.GetComponent<Rigidbody2D>().velocity = new Vector2(0, shotSpeed);
            }

            swapPoint = !swapPoint;
            cooldownCount = 0;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Tank")
        {
            if (collision.GetComponent<TankManager>().tankSpeed <= tankSpeed)
            {
                tankSpeed = collision.GetComponent<TankManager>().tankSpeed;
            }
            if (collision.attachedRigidbody.constraints == RigidbodyConstraints2D.FreezePositionY)
            {
                tankSpeed = 0;
            }
        }
        else if (collision.tag == "TankStopper")
        {
            rigidBody.constraints = RigidbodyConstraints2D.FreezePositionY;
            tankMoveSFX.Stop();
        }
    }
    private void TankDeath()
    {
        scoreManager.UpdateScore(tankScore);
        scoreManager.UpdateCredit(tankCredit);
        tankDeathSFX.Play();
        tankMoveSFX.Stop();
        animator.SetBool("isDead", true);
        this.gameObject.GetComponent<Collider2D>().enabled = false;
        Destroy(this.gameObject, 1);
    }
}
