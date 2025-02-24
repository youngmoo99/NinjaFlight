using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{   
    //캐릭터 움직임 속도 
    [SerializeField]
    private float moveSpeed;

    //무기 게임 오브젝트 생성
    [SerializeField]
    private GameObject weapon;
    //private int weaponIndex = 0;

    [SerializeField]
    private Transform shootTransform;

    [SerializeField]
    private float shootInterval = 0.05f;
    private float lastShotTime = 0f;


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

        Shoot();

    }
    void Shoot() {
        if (Time.time - lastShotTime > shootInterval) { // Time.time : 게임이 시작된 이후로 현재까지 흐른시간
            //Instantiate(어떤객체를, 어떤위치에, 회전을 어떻게해서);
            //Instantiate(weapons[weaponIndex], shootTransform.position, quaternion.identity);
            Instantiate(weapon, shootTransform.position, quaternion.identity);
            lastShotTime = Time.time;
        }
    }
}
