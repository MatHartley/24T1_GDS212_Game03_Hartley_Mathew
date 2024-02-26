using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallManager : MonoBehaviour
{
    [Header("UI Reference")]
    public Slider healthSlider;

    [Header("Health")]
    public int wallHealth;

    [Header("Game Controller")]
    public GameController gameController;

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = wallHealth;

        if (wallHealth <= 0)
        {
            gameController.BadEnd();
        }
    }
}
