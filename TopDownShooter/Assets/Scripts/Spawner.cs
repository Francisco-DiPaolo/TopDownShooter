using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject swarmerPrefab;

    [SerializeField]
    private float swarmerInterval = 3.5f;

    public bool boolTree = false;

    Tree tree;
    GameController gameController;

    [Header("PowerUps")]
    [SerializeField] GameObject powerUps;
    [SerializeField] float powerUpInterval;

    private void Start()
    {
        tree = FindObjectOfType<Tree>();
        gameController = FindObjectOfType<GameController>();
    }
    private void FixedUpdate()
    {
        if (boolTree)
        {
            StartCoroutine(spawnEnemy(swarmerInterval, swarmerPrefab));

            tree.boolYaSpawnearon = true;
            boolTree = false;

            StartCoroutine(spawnPowerUps(powerUpInterval, powerUps));
        }
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);

        int obstacleSpawnIndex = Random.Range(0, 6);
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;

        GameObject newEnemy = Instantiate(enemy, spawnPoint.position, Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }

    private IEnumerator spawnPowerUps(float interval, GameObject powerUps)
    {
        yield return new WaitForSeconds(interval);

        int obstacleSpawnIndex = Random.Range(7, 10);
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;

        GameObject newEnemy = Instantiate(powerUps, spawnPoint.position, Quaternion.identity);
        StartCoroutine(spawnPowerUps(interval, powerUps));
    }
}
