using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalReferences : MonoBehaviour
{
    // global reference for the other scripts
    public static GlobalReferences Instance { get; set; }
    public GameObject bulletImpactEffectPrefab;
    public GameObject bloodSprayEffect;
    public int waveNumber;
    public int totalZombies;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}

