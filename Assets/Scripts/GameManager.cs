using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   
    //싱글톤 패턴 
    public static GameManager instance = null;

    private int coin = 0;

    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private GameObject gameOverPanel;

    [HideInInspector]
    public bool isGameOver = false;

    void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    public void IncreaseCoin() {
        coin++;
        text.SetText(coin.ToString());

        if (coin % 5 == 0) {
            Player player = FindObjectOfType<Player>();
            if (player != null) {
                player.Upgrade();
            }
        }
    }
    public void IncreaseCoin2() {
        coin = coin+2;
        text.SetText(coin.ToString());

        if (coin % 3 == 0) {
            Player player = FindObjectOfType<Player>();
            if (player != null) {
                player.Upgrade();
            }
        }
    }
    public void IncreaseCoin3() {
        coin = coin+3;
        text.SetText(coin.ToString());

        if (coin % 3 == 0) {
            Player player = FindObjectOfType<Player>();
            if (player != null) {
                player.Upgrade();
            }
        }
    }

    public void SetGameOver() {
        isGameOver = true;

        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
        if (enemySpawner != null) {
            enemySpawner.StopEnemyRoutine();
        }

        //시간을 기다린뒤에 메소드실행  1초뒤에   
        Invoke("ShowGameOverPanel", 1f);
    }

    void ShowGameOverPanel() {
        gameOverPanel.SetActive(true);
    }

    public void PlayAgain() {
        SceneManager.LoadScene("SampleScene");
    }
}
