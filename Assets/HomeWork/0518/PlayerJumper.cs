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
        if (state != PlayerController5.STATE.Jump) //&& state != STATE.Move) // ���� , �����̴� �߿��� ���� �Ұ�
        {
            ani.SetTrigger("JumpTrigger");
            rgd.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
            state = PlayerController5.STATE.Jump;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {   // ���� ��ƾ��� ���� �����ϰ� ��
            //Debug.Log(name+"�� ������Ҵ�.");
            state = PlayerController5.STATE.None;
        }
    }
}
