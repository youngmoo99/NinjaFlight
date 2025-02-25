using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{   
    [SerializeField]
    private GameObject[] enemies;

    [SerializeField]
    private GameObject boss;
    private float[] arrPosX = {-2.2f, -1.1f, 0, 1.1f, 2.2f};

    [SerializeField]
    private float spawnInterval = 1.5f;

    // Start is called before the first frame update
    void Start()
    {   
        StartEnemyRoutine();
    }
    void StartEnemyRoutine() {
        StartCoroutine("EnemyRoutine");
    }
    public void StopEnemyRoutine() {
        StopCoroutine("EnemyRoutine");
    }

    IEnumerator EnemyRoutine() {
        //3초 지난후에 밑에 반복 실행 
        yield return new WaitForSeconds(3f);

        float moveSpeed = 5f;
        int enemyIndex = 0;
        int spawnCount = 0;

        while (true) {
            foreach (float posX in arrPosX) {
                //적 개수 랜덤 0~14 
                //int index = Random.Range(0,enemies.Length);
                SpawnEnemy(posX, enemyIndex, moveSpeed);
            }
            spawnCount++;
            if(spawnCount % 1 == 0) {
                enemyIndex++;
                moveSpeed++; 
            }

            if (enemyIndex >= enemies.Length) {
                SpawnBoss();
                enemyIndex = 0;
                moveSpeed = 5f;
            }
            

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    //적 생성 
    void SpawnEnemy(float posX, int index, float moveSpeed) {
        Vector3 spawnPos = new Vector3(posX,transform.position.y, transform.position.z);

        if(Random.Range(0,5) == 0) {
            index++;
        }
        if(index >= enemies.Length) {
            index = enemies.Length -1;
        }

        //Enemy 클래스에 setmovespeed 적용 (시간이 지날수록 쓰레기 속도 증가)
        GameObject enemyObject = Instantiate(enemies[index], spawnPos, Quaternion.identity);
        Enemy enemy = enemyObject.GetComponent<Enemy>();
        enemy.SetMoveSpeed(moveSpeed);
    }

    void SpawnBoss() {
        Instantiate(boss,transform.position, Quaternion.identity);
    }
}
