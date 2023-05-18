using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJumper : MonoBehaviour
{
    [Header("Jump Status")]
    public float JumpPower = 5.0f;

    private Rigidbody rgd;
    private PlayerController5.STATE state;
    private Animator ani;

    private void Awake()
    {
        rgd = GetComponent<Rigidbody>();
        state = GetComponent<PlayerController5>().state;
        ani = GetComponent<Animator>();
    }

    //private void OnJump(InputValue input)
    //{
    //    jump();
    //}

    public void jump()
    {
        if (state != PlayerController5.STATE.Jump) //&& state != STATE.Move) // 점프 , 움직이는 중에는 점프 불가
        {
            ani.SetTrigger("JumpTrigger");
            rgd.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
            state = PlayerController5.STATE.Jump;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {   // 땅에 닿아야지 점프 가능하게 함
            //Debug.Log(name+"이 땅에닿았다.");
            state = PlayerController5.STATE.None;
        }
    }
}
