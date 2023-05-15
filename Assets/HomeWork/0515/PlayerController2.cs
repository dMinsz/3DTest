using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.Windows;
using static UnityEngine.GraphicsBuffer;

public class PlayerController2: MonoBehaviour
{
    private enum STATE { None, Jump, Move }

    [SerializeField]
    private STATE state = STATE.None;
    private Rigidbody rgd;

    //public Camera camera;
    [HideInInspector]
    public Vector3 moveDir;

    [Header("Tank Parts")]
    public GameObject tankTurret;
    public GameObject BulletFrepab;

    [Header("Tank Status")]
    public float MovePower = 10.0f;
    public float RotatePower = 60.0f;
    public float JumpPower = 5.0f;
    public float RepeatTime = 1.0f;


    IEnumerator FireRoutine()
    {
        while (true)
        {
            Transform shoot = transform.Find("Tank").Find("TankRenderers").Find("TankTurret").Find("ShootPosition");
            GameObject obj = Instantiate(BulletFrepab, shoot.position, shoot.rotation);
            yield return new WaitForSeconds(RepeatTime);     // n�ʰ� �ð�����

        }
    }

    private void CoroutineStart()
    {
        StartCoroutine(FireRoutine());
    }

    private void CoroutineStop()
    {
        StopAllCoroutines();        // ��� �ڷ�ƾ ����
    }


    private void Awake()
    {
        //camera = GetComponent<Camera>();
        rgd = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        Move();
        Roatate();
    }

    private void Move()
    {
        transform.Translate(Vector3.forward* moveDir.z * Time.deltaTime * MovePower); // ���� ����.
    }

    private void jump() 
    {
        if (state != STATE.Jump) //&& state != STATE.Move) // ���� , �����̴� �߿��� ���� �Ұ�
        {
            rgd.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
            state = STATE.Jump;
        }
    }

    private void Roatate()
    {
        transform.Rotate(Vector3.up, moveDir.x * Time.deltaTime * RotatePower , Space.World);
    }


    //input system �̿��Ͽ� �����̱�
    private void OnMove(InputValue input)
    {
        //Vector3 inputDir = new Vector3();
        moveDir.x = input.Get<Vector2>().x;
        moveDir.z = input.Get<Vector2>().y;
    }


    private void OnJump(InputValue input)
    {
        jump();
    }

    private void OnFire(InputValue input) 
    {
        //if (isFire)
        //{
        //    isFire = false;

            Transform shoot = transform.Find("Tank").Find("TankRenderers").Find("TankTurret").Find("ShootPosition");
            GameObject obj = Instantiate(BulletFrepab, shoot.position , shoot.rotation);
        //}
        //StartCoroutine(FireRoutine());
    }
    private void OnRepeatFire(InputValue input) 
    {
        if (input.isPressed)
        {
            CoroutineStart();
        }
        else 
        {
            CoroutineStop();
        }
    }

    private void OnRotateTurret(InputValue input)
    {
        Vector3  targetPosition = new Vector3(input.Get<Vector2>().x, 0, input.Get<Vector2>().y);

        Vector3 aimVector = targetPosition - tankTurret.transform.position;
        aimVector.y = 0.0f;
        Quaternion newRotation = Quaternion.LookRotation(aimVector, transform.up);
        tankTurret.transform.rotation = Quaternion.Slerp(tankTurret.transform.rotation, newRotation, Time.deltaTime * RotatePower);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {   // ���� ��ƾ��� ���� �����ϰ� ��
            //Debug.Log(name+"�� ������Ҵ�.");
            state = STATE.None;
        }
    }



}
