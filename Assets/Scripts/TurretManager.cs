using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    [Header("Turret Level")]
    [SerializeField] private int turretLevel;

    [Header("Cooldown Values")]
    [SerializeField] private int cooldownTime;
    [SerializeField] private float cooldownCount;

    [Header("Bullet Prefabs")]
    [SerializeField] private GameObject bulletPrefab;

    [Header("Turret Prefabs")]
    [SerializeField] private GameObject[] turretPrefab;
}
