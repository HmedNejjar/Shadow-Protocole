using UnityEngine;
using System.Collections;
using TMPro;

public class EnemyWaveSpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;

    [Header("Wave Settings")]
    public int startEnemies = 3;
    public float spawnDelay = 0.5f;
    private int waveNumber = 0;
    private int enemiesAlive = 0;

    [Header("UI")]
    public TextMeshProUGUI waveText;

    void Start()
    {
        waveText.text = "Get ready to fight!";
        EnemyHealth.OnEnemyKilled += HandleEnemyKilled;
        StartCoroutine(StartNextWave());
    }

    void OnDestroy()
    {
        EnemyHealth.OnEnemyKilled -= HandleEnemyKilled;
    }

    void HandleEnemyKilled()
    {
        enemiesAlive--;

        if (enemiesAlive <= 0)
        {
            StartCoroutine(StartNextWave());
        }
    }

    IEnumerator StartNextWave()
    {
        waveNumber++;
        UpdateWaveUI(waveNumber);
        yield return new WaitForSeconds(2f); // wait before next wave

        int enemyCount = startEnemies + waveNumber;
        enemiesAlive = enemyCount;

        for (int i = 0; i < enemyCount; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Length == 0) return;

        int index = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[index];
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    void UpdateWaveUI(int wave)
    {
        if (waveText != null)
        {
            waveText.text = "Wave " + wave.ToString("00");
            StartCoroutine(FadeWaveText());
        }
    }

    IEnumerator FadeWaveText()
    {
        float fadeInTime = 0.5f;
        float displayTime = 1.5f;
        float fadeOutTime = 1f;

        Color originalColor = waveText.color;

        // Fade In
        for (float t = 0f; t < fadeInTime; t += Time.deltaTime)
        {
            float alpha = t / fadeInTime;
            waveText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }
        waveText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);

        // Hold
        yield return new WaitForSeconds(displayTime);

        // Fade Out
        for (float t = 0f; t < fadeOutTime; t += Time.deltaTime)
        {
            float alpha = 1f - (t / fadeOutTime);
            waveText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }
        waveText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
    }
}
