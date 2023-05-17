using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
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

public class PlayerController4: MonoBehaviour
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

    [SerializeField] private float MouseSensitive = 0.5f;
    [SerializeField] private bool InvertVertical = true;


    private Animator ani;

    private Vector3 orgBackCam;
    private bool ChangeCam = false;
    IEnumerator FireRoutine()
    {
        while (true)
        {
            Transform shoot = transform.Find("Tank").Find("TankRenderers").Find("TankTurret").Find("ShootPosition");
            Instantiate(BulletFrepab, shoot.position, shoot.rotation);
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
        ani = GetComponent<Animator>();

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
            ani.SetTrigger("JumpTrigger");
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

    public void Fire() 
    {
        ani.SetTrigger("FireTrigger");

        Transform shoot = transform.Find("Tank").Find("TankRenderers").Find("TankTurret").Find("ShootPosition");
        GameObject obj = Instantiate(BulletFrepab, shoot.position, shoot.rotation);
    }

    private void OnFire(InputValue input) 
    {
        Fire();
    }

    private void OnRepeatFire(InputValue input) 
    {

        if (input.isPressed)
        {
            ani.SetTrigger("FireTrigger");
            CoroutineStart();
        }
        else 
        {
            CoroutineStop();
        }
    }

    private void OnFocus(InputValue input)
    {        
        if (!ChangeCam)
        {
            var BackCam = transform.Find("BackCam").GetComponent<CinemachineVirtualCamera>();
            BackCam.Priority = 0;
            ChangeCam = true;
        }
        else 
        {
            var BackCam = transform.Find("BackCam").GetComponent<CinemachineVirtualCamera>();
            BackCam.Priority = 2;
            ChangeCam = false;
        }

    }


    private void OnRotateTurret(InputValue input)
    {

        var deltaMouse = new Vector2(input.Get<Vector2>().x, input.Get<Vector2>().y);
        deltaMouse.x %= Screen.width; // ��ũ�� width ������ ���콺 ��Ÿ ���� �� �ٿ��ش�.

        float middle = Screen.width / 2f; // ��ũ�� ��� �� ���ؼ�

        float offset =  deltaMouse.x - middle; // offset�� �༭  ���콺�� �̵��� �� ���� �ȵ��ư��� �ٲ۴�.
        offset *= MouseSensitive;

        Vector2 deltaRotation = deltaMouse;
        deltaRotation.x *= InvertVertical ? 1.0f : -1.0f; // �¿� ���� �ʿ�� ��밡��

        offset = Mathf.Clamp(offset, -140.0f, 140.0f); // -140�� 140�� �� ����


        tankTurret.transform.localRotation = Quaternion.Euler(0f, offset, 0f);
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
