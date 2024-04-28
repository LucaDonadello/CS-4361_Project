using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ZombieSpawnController : MonoBehaviour
{
    // variables
    public int initialZombiesPerWave = 5;
    public int currentZombiesPerWave;
    public float spawnDelay = 0.5f; // delay between each zombie spawn
    public int currentWave = 0;
    public float waveCooldown = 10.0f; // time between waves
    public bool inCooldown;
    public float cooldownCounter = 0; // testing purposes
    public List<Enemy> currentZombiesAlive;
    public GameObject zombiePrefab;
    public TextMeshProUGUI waveOverUI;
    public TextMeshProUGUI cooldownCounterUI;
    public TextMeshProUGUI currentWaveUI;

    private void Start()
    {
        currentZombiesPerWave = initialZombiesPerWave;
        GlobalReferences.Instance.waveNumber = currentWave;
        StartNextWave();
    }

    // Start the next wave
    private void StartNextWave()
    {
        currentZombiesAlive.Clear();
        currentWave++;
        GlobalReferences.Instance.waveNumber = currentWave;
        // add zombies to the total zombies
        GlobalReferences.Instance.totalZombies += currentZombiesPerWave;
        currentWaveUI.text = "Wave: " + currentWave.ToString();
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        // Spawn the zombies
        for (int i = 0; i < currentZombiesPerWave; i++)
        {
            // generate a random spawn position
            Vector3 spawnOffset = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
            Vector3 spawnPosition = transform.position + spawnOffset;
            // instantiate the zombie
            var zombie = Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
            // get the enemy script
            Enemy enemyScript = zombie.GetComponent<Enemy>();
            // track zombies
            currentZombiesAlive.Add(enemyScript);
            yield return new WaitForSeconds(spawnDelay);
        }

    }

    private void Update()
    {
        // get all dead zombies
        List<Enemy> zombiesToRemove = new List<Enemy>();
        foreach (Enemy zombie in currentZombiesAlive)
        {
            if (zombie.isDead)
            {
                zombiesToRemove.Add(zombie);
            }
        }

        // remove the dead zombies
        foreach (Enemy zombie in zombiesToRemove)
        {
            currentZombiesAlive.Remove(zombie);
        }

        zombiesToRemove.Clear();

        // cooldown for next wave
        if (GlobalReferences.Instance.totalZombies == 0 && inCooldown == false && currentZombiesAlive.Count == 0)
        {
            // start the cooldown
            StartCoroutine(WaveCooldown());
        }

        // run the cooldown
        if (inCooldown)
        {
            cooldownCounter -= Time.deltaTime;
        }
        else
        {
            // reset the counter
            cooldownCounter = waveCooldown;
        }

        cooldownCounterUI.text = cooldownCounter.ToString("F0");
    }

    // cooldown for the next wave
    private IEnumerator WaveCooldown()
    {
        inCooldown = true;
        waveOverUI.gameObject.SetActive(true);

        yield return new WaitForSeconds(waveCooldown);

        inCooldown = false;
        waveOverUI.gameObject.SetActive(false);

        currentZombiesPerWave *= 2;
        StartNextWave();
    }
}
