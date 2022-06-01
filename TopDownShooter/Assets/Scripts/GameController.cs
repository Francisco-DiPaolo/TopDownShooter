using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int score;

    public GameObject boss;
    public Transform spawnerPosition;
    public bool boolSpawnerBoss = false;
    public bool boolUltimateFaseBoss = false;

    private PlayerMovement player;

    [Header("Text")]
    public Text textContador;
    public Text textLife;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        textContador.text = "Score: " + score.ToString();
        textLife.text = "Vida: " + player.vidaPlayer.ToString();
        score = 0;
    }
    void Update()
    {
        if (boolUltimateFaseBoss && !boolSpawnerBoss)
        {
            Instantiate(boss, spawnerPosition.transform.position, Quaternion.identity);
            boolSpawnerBoss = true;
        }
    }

    public void EnemyDie()
    {
        score++;
        textContador.text = "Score: " + score.ToString();
    }

    public void SetTextLife()
    {
        textLife.text = "Vida: " + player.vidaPlayer.ToString();
    }

    public void Win()
    {
        SceneManager.LoadScene("Winner");
    }

    public void Lose()
    {
        SceneManager.LoadScene("GameOver");
    }
}
