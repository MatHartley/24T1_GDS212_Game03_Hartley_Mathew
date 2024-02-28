using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallManager : MonoBehaviour
{
    [Header("UI Reference")]
    public Slider healthSlider;
    [SerializeField] private GameObject cooldownSliderHolder;

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
            cooldownSliderHolder.SetActive(false);
            gameController.BadEnd();
        }
    }
}
