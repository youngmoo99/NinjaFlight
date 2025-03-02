using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Build.Content;
using UnityEngine;

public class Player : MonoBehaviour
{   
    //캐릭터 움직임 속도 
    [SerializeField]
    private float moveSpeed;

    //무기 게임 오브젝트 생성
    [SerializeField]
    private GameObject[] weapons;
    private int weaponIndex = 0;

    [SerializeField]
    private Transform shootTransform;

    [SerializeField]
    private float shootInterval = 0.05f;
    private float lastShotTime = 0f;

    [SerializeField]
    private float spreadAngle = 30f; //부채꼴 범위 (3발 기준)

    // Update is called once per frame
    void Update()

    {
        //플레이어 키보드 제어 
        float horizontalInput = Input.GetAxisRaw("Horizontal"); //키보드 가로기준 값 받음
        float verticalInput = Input.GetAxisRaw("Vertical"); //세로 기준 값 받음

        Vector3 moveTo = new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        if (Input.GetKey(KeyCode.LeftArrow)) { //왼쪽 방향키 일때 
            transform.position -= moveTo;
        } else if (Input.GetKey(KeyCode.RightArrow)) {
            transform.position += moveTo;
        }

        if(GameManager.instance.isGameOver == false) {
            Shoot();
        }
    }
    void Shoot() {
        if (Time.time - lastShotTime > shootInterval) { // Time.time : 게임이 시작된 이후로 현재까지 흐른시간
            //Instantiate(어떤객체를, 어떤위치에, 회전을 어떻게해서);
            //Instantiate(weapons[weaponIndex], shootTransform.position, quaternion.identity);
            int bulletCount = 3;
            float startAngle = -spreadAngle / 2; // 시작 각도
            float angleStep = spreadAngle / (bulletCount - 1); // 각 탄환 간격
            float fireDistance = 0.2f; // 탄환의 초기 위치 간 거리 (조절 가능)

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = startAngle + (angleStep * i); // -15, 0, 15 순서로 발사
            Quaternion rotation = Quaternion.Euler(0, 0, angle) * shootTransform.rotation;

            // 탄환을 부채꼴 모양으로 배치
            Vector3 bulletPosition = shootTransform.position + rotation * new Vector3((i - 1) * fireDistance, 0, 0);

            // 탄환 생성
            GameObject bullet = Instantiate(weapons[weaponIndex], bulletPosition, rotation);

            // Rigidbody2D 적용해서 발사
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null) {
                Vector2 shootDirection = rotation * Vector2.right; // 부채꼴 방향으로 이동
                rb.velocity = shootDirection * 10f;
            }
        }
            lastShotTime = Time.time;
        } 
        
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss") {
            GameManager.instance.SetGameOver();
            Destroy(gameObject);
        } else if(other.gameObject.tag =="Coin") {
            GameManager.instance.IncreaseCoin();
            Destroy(other.gameObject);
        } else if(other.gameObject.tag =="Coin2") {
            GameManager.instance.IncreaseCoin2();
            Destroy(other.gameObject);
        } else if(other.gameObject.tag =="Coin3") {
            GameManager.instance.IncreaseCoin3();
            Destroy(other.gameObject);
        }
        
    }

    public void Upgrade() {
        weaponIndex++;
        if (weaponIndex >= weapons.Length) {
            weaponIndex = weapons.Length -1;
        }

    }
}
