using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    [Header("Wave")]
    [SerializeField] private int waveNumber;
    [SerializeField] private int maxWave;
    [SerializeField] private float waveCooldown;
    [SerializeField] private float waveCooldownCount;

    [Header("Object References")]
    [SerializeField] private GameObject[] tankSpawns;
    [SerializeField] private TextMeshProUGUI waveNumberText;

    [Header("Spawns")]
    [SerializeField] private int spawnCap;
    [SerializeField] private int spawnNumber;
    [SerializeField] private float spawnCooldown;
    [SerializeField] private float spawnCooldownCount;

    // Start is called before the first frame update
    void Start()
    {
        waveNumber = 1;
        waveNumberText.text = waveNumber.ToString();
        waveCooldownCount = waveCooldown;
        spawnCooldownCount = spawnCooldown;
        spawnNumber = 0;
        spawnCap = Mathf.RoundToInt((waveNumber * 5) / 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnNumber == spawnCap)
        {
            waveCooldownCount -= Time.deltaTime;
        }
        else
        {
            spawnCooldownCount -= Time.deltaTime;
        }

        if ((spawnCooldownCount <= 0) && (spawnNumber <= spawnCap))
        {
            //picks a spawn line at random
            int spawnLocation = Mathf.RoundToInt(Random.Range(1, 5));
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
                spawnNumber = 0;
                spawnCooldown = spawnCooldown * 0.9f;
                spawnCap = Mathf.RoundToInt((waveNumber * 5) / 2);
            }
            else
            {
                //end game
            }
        }
    }
}

