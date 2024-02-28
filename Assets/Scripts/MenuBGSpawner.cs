using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBGSpawner : MonoBehaviour
{
    [Header("Tank Prefabs")]
    [SerializeField] private GameObject[] tankSpawns;

    [Header("Cooldown")]
    [SerializeField] private float initialCooldown;
    [SerializeField] private float spawnCooldown;
    [SerializeField] private float spawnCooldownCount;

    [Header("Internals")]
    private int lastLane = 0;

    // Start is called before the first frame update
    void Start()
    {
        spawnCooldownCount = initialCooldown; //done to make the first spawn tie in with the base drop on the menu bgm :)
    }

    // Update is called once per frame
    void Update()
    {
        spawnCooldownCount -= Time.deltaTime;

        if (spawnCooldownCount <= 0)
        {
            int spawnLocation;

            //picks a spawn line at random, without spawning in the same lane twice in a row
            do
            {
                spawnLocation = Mathf.RoundToInt(Random.Range(1, 5));
                Debug.Log(spawnLocation + " | " + lastLane);
            } while (spawnLocation == lastLane);

            lastLane = spawnLocation;

            GameObject spawn = Instantiate(tankSpawns[(Mathf.RoundToInt(Random.Range(0, tankSpawns.Length)))]) as GameObject;

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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}
