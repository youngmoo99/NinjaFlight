using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{   
    [SerializeField]
    private GameObject coin; 


    [SerializeField]
    private float moveSpeed = 10f;
    private float minY = -7f;

    [SerializeField]
    private float hp = 1f;

    public void SetMoveSpeed(float moveSpeed) {
        this.moveSpeed = moveSpeed; // this를 붙일경우 클래스내에 있는 변수 
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        if(transform.position.y < minY) {
            Destroy(gameObject);
        }
    }

    //충돌 메서드 OnTriggerEnter2D 충돌감지  isTirgger 체크 o
    private void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.tag == "Weapon") {
                Weapon weapon = other.gameObject.GetComponent<Weapon>();
                hp -= weapon.damage;
                if (hp <=0) {
                    if (gameObject.tag == "Boss") {
                        GameManager.instance.SetGameOver();
                    }
                    Destroy(gameObject);
                    //코인생성
                    Instantiate(coin, transform.position, Quaternion.identity);

                }
                Destroy(other.gameObject);
            }
    }

    //충돌 메서드 OnCollisionEnter2D 물리적 충돌발생 isTirgger 체크 x
    // private void OnCollisionEnter2D(Collision2D other) {
        
    // }
}
