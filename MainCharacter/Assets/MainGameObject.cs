using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameObject : MonoBehaviour
{
    public float jumpPower;

    public bool isSliding = false;
    public Rigidbody2D rigidBody;
    public Animator anim;

    public BoxCollider2D regularColl;
    public BoxCollider2D slideColl;

    public float slideSpeed = 5f;
    
    Rigidbody2D rigid;
    MeshRenderer mesh;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        mesh = GetComponent<MeshRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Coin")
        {
            //Coin이랑 부딪혔을 경우에만 적용됨
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        //space를 누르면 점프
        if (Input.GetKeyDown(KeyCode.LeftShift))
            prefromSlide();
        //shift를 누르면 슬라이드
    }

    private void prefromSlide()
    {
        isSliding = true;
        anim.SetBool("IsSlide", true);

        regularColl.enabled = false;
        slideColl.enabled = true;
        
        rigidBody.AddForce(Vector2.right * slideSpeed);
        
        StartCoroutine("stopSlide");
    }
    IEnumerator stopSlide()
    {
        yield return new WaitForSeconds(0.8f);
        anim.Play("Idle");
        anim.SetBool("IsSlide", false);
        regularColl.enabled = true;
        slideColl.enabled = false;
        isSliding = false;
    }
    private void FixedUpdate()
    {
        rigid.velocity = Vector2.right;//오른쪽으로 계속 이동
    }

}
