using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MathewHartley;

public class WaveManager : MonoBehaviour
{
    [Header("Wave")]
    [SerializeField] private int waveNumber;
    [SerializeField] private int maxWave;
    [SerializeField] private float waveCooldown;
    [SerializeField] private float waveCooldownCount;
    [SerializeField] private float cooldownToCooldown;
    [SerializeField] private float cooldownToCooldownCount;

    [Header("Object References")]
    [SerializeField] private GameObject[] tankSpawns;
    [SerializeField] private TextMeshProUGUI waveNumberText;
    [SerializeField] private GameObject wavePanel;

    [Header("Spawns")]
    [SerializeField] private int spawnCap;
    [SerializeField] private int spawnNumber;
    [SerializeField] private float spawnCooldown;
    [SerializeField] private float spawnCooldownCount;
    private int lastLane = 0;

    [Header("Timer")]
    public Timer countdownTimer;

    [Header("Game Controller")]
    public GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        waveNumber = 1;
        waveNumberText.text = waveNumber.ToString();
        waveCooldownCount = waveCooldown;
        spawnCooldownCount = spawnCooldown;
        cooldownToCooldownCount = cooldownToCooldown;
        spawnNumber = 0;
        spawnCap = Mathf.RoundToInt((waveNumber * 5) / 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnNumber == spawnCap)
        {
            cooldownToCooldownCount -= Time.deltaTime;

            if (cooldownToCooldownCount <=0)
            {
                waveCooldownCount -= Time.deltaTime;
                countdownTimer.isPaused = false;
                wavePanel.SetActive(true);
            }
        }
        else
        {
            spawnCooldownCount -= Time.deltaTime;
            wavePanel.SetActive(false);
        }

        if ((spawnCooldownCount <= 0) && (spawnNumber <= spawnCap))
        {
            int spawnLocation;

            //picks a spawn line at random, without spawning in the same lane twice in a row
            do
            {
                spawnLocation = Mathf.RoundToInt(Random.Range(1, 5));
            } while (spawnLocation == lastLane);

            lastLane = spawnLocation;

            GameObject spawn = Instantiate(tankSpawns[(Mathf.RoundToInt(Random.Range(0, waveNumber)))]) as GameObject;
            spawnNumber++;

            //Zone 1 is Far Left. Zone 2 is Left Middle. Zone 3 is Centre. Zone 4 is Right Middle. Zone 5 is Far Right.
            switch (spawnLocation)
            {
                case 1:
                    spawn.transform.position = new Vector2(-1.76f, 6f);
                    break;
                case 2:
                    spawn.transform.position = new Vector2(-0.88f, 6f);
                    break;
                case 3:
                    spawn.transform.position = new Vector2(0, 6f);
                    break;
                case 4:
                    spawn.transform.position = new Vector2(0.88f, 6f);
                    break;
                case 5:
                    spawn.transform.position = new Vector2(1.76f, 6f);
                    break;
                default:
                    break;
            }
            spawnCooldownCount = spawnCooldown;
        }

        if (waveCooldownCount <= 0)
        {
            waveNumber++;

            if (waveNumber <=10)
            {
                waveNumberText.text = waveNumber.ToString();
                waveCooldownCount = waveCooldown;
                cooldownToCooldownCount = cooldownToCooldown;
                countdownTimer.currentTime = waveCooldown;
                countdownTimer.isPaused = true;
                spawnNumber = 0;
                spawnCooldown = spawnCooldown * 0.9f;
                spawnCap = Mathf.RoundToInt((waveNumber * 5) / 2);
            }
            else
            {
                gameController.GoodEnd();
            }
        }
    }
}

