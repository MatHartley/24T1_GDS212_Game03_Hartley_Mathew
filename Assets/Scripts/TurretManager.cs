using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurretManager : MonoBehaviour
{
    [Header("Turret Level")]
    [SerializeField] private int turretLevel;

    [Header("Cooldown Values")]
    [SerializeField] private float cooldownOne;
    [SerializeField] private float cooldownTwo;
    [SerializeField] private float cooldownThree;
    private float cooldownTime;
    [SerializeField] private float cooldownCount;
    [SerializeField] Slider[] sliderSelector;
    public Slider cooldownSlider;

    [Header("Bullet Prefabs")]
    [SerializeField] private GameObject[] bulletSelector;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float shotSpeed;

    [Header("Turret Sprites")]
    [SerializeField] private Sprite[] turretSprite;

    [Header("Firepoints")]
    [SerializeField] private Transform[] firepointSelector;
    [SerializeField] private Transform firePointA;
    [SerializeField] private Transform firePointB;
    private bool swapPoint = true;

    [Header("Upgrades")]
    [SerializeField] private int upgradeToTwo;
    [SerializeField] private int upgradeToThree;
    [SerializeField] private int upgradeCost;
    [SerializeField] private TextMeshProUGUI costText;

    public void Start()
    {
        //set initial turret at level 1
        cooldownSlider = sliderSelector[0];
        cooldownSlider.gameObject.SetActive(true);
        gameObject.GetComponent<SpriteRenderer>().sprite = turretSprite[0];
        firePointA = firepointSelector[0];
        firePointB = firepointSelector[0];
        bulletPrefab = bulletSelector[0];
        cooldownTime = cooldownOne;
        upgradeCost = upgradeToTwo;
        costText.text = upgradeCost.ToString();

        cooldownCount = 0;
        cooldownSlider.minValue = 0;
        cooldownSlider.maxValue = cooldownTime;
    }

    private void Update()
    {
        cooldownCount += Time.deltaTime;
        cooldownSlider.value = cooldownCount;
    }
    /// <summary>
    /// Fires the tapped/clicked on turret if the cooldown is up
    /// </summary>
    public void Fire()
    {
        if (cooldownCount >= cooldownTime)
        {
            Debug.Log("Firing Cannon");
            GameObject shot = Instantiate(bulletPrefab) as GameObject;

            if (swapPoint)
            {
                shot.transform.position = firePointA.transform.position;
            }
            else
            {
                shot.transform.position = firePointB.transform.position;
            }

            shot.GetComponent<Rigidbody2D>().velocity = new Vector2(0, shotSpeed);

            swapPoint = !swapPoint;
            cooldownCount = 0;
        }
    }

    public void Upgrade()
    {
        //if(scoreManager.currentCredit > upgradeCost)
        //{
            turretLevel++;
        //scoreManager.currentCredit - upgradeCost;
        //}

        if (turretLevel>3)
        { 
            turretLevel = 3;
        }

        cooldownSlider.gameObject.SetActive(false);

        //select which slider, slider max, cooldown, firepoints, bullet prefab, sprite and upgrade cost to use based on turret level
        switch (turretLevel)
        {
            case 1:
                cooldownSlider = sliderSelector[0];
                cooldownSlider.gameObject.SetActive(true);
                gameObject.GetComponent<SpriteRenderer>().sprite = turretSprite[0];
                firePointA = firepointSelector[0];
                firePointB = firepointSelector[0];
                bulletPrefab = bulletSelector[0];
                cooldownTime = cooldownOne;
                cooldownSlider.maxValue = cooldownTime;
                upgradeCost = upgradeToTwo;
                costText.text = upgradeCost.ToString();
                break;
            case 2:
                cooldownSlider = sliderSelector[1];
                cooldownSlider.gameObject.SetActive(true);
                gameObject.GetComponent<SpriteRenderer>().sprite = turretSprite[1];
                firePointA = firepointSelector[1];
                firePointB = firepointSelector[2];
                bulletPrefab = bulletSelector[0];
                cooldownTime = cooldownTwo;
                cooldownSlider.maxValue = cooldownTime;
                upgradeCost = upgradeToThree;
                costText.text = upgradeCost.ToString();
                break;
            case 3:
                cooldownSlider = sliderSelector[2];
                cooldownSlider.gameObject.SetActive(true);
                gameObject.GetComponent<SpriteRenderer>().sprite = turretSprite[2];
                firePointA = firepointSelector[3];
                firePointB = firepointSelector[3];
                bulletPrefab = bulletSelector[1];
                cooldownTime = cooldownThree;
                cooldownSlider.maxValue = cooldownTime;
                upgradeCost = 00;
                costText.text = " ";
                break;
            default:
                cooldownSlider = sliderSelector[0];
                cooldownSlider.gameObject.SetActive(true);
                gameObject.GetComponent<SpriteRenderer>().sprite = turretSprite[0];
                firePointA = firepointSelector[0];
                firePointB = firepointSelector[0];
                bulletPrefab = bulletSelector[0];
                cooldownTime = cooldownOne;
                cooldownSlider.maxValue = cooldownTime;
                upgradeCost = upgradeToTwo;
                costText.text = upgradeCost.ToString();
                break;
        }
    }
}
