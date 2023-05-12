using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class PlayerController: MonoBehaviour
{

    //public Camera camera;
    private Vector3 moveDir;
    private Rigidbody rgd;

    public float MovePower = 1.0f;
    public float RotatePower = 60.0f;
    public float JumpPower = 5.0f;

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
        //.AddForce(moveDir * MovePower);

        // Transform으로 이동
        //transform.position = moveDir * Time.deltaTime * MovePower; // 프레임당 이동해줘야한다.
        transform.Translate(moveDir * Time.deltaTime * MovePower); // 위와 같다.

        //Vector3 cameraPos = new Vector3();
        //cameraPos.x = camera.transform.position.x + moveDir.x;
        //cameraPos.y = camera.transform.position.y;
        //cameraPos.z = camera.transform.position.z + moveDir.z;

        //camera.gameObject.transform.Translate(cameraPos * Time.deltaTime * MovePower); // 카메라도 같이이동

        // 바라보는 방향 기준으로 이동한다.
        // AddForce 함수와 다르게 누르는 순간 이동하고 가속, 감속하지않는다.

    }

    private void jump() 
    {
        rgd.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
    }

    private void Roatate()
    {
        //transform.rotation = Quaternion.identity;
        //transform.rotation = Quaternion.Euler(0, 90, 0);
        //transform.rotation.ToEulerAngles(); 쿼터니언을 오일러 앵글로 바꾸는법
        
        transform.Rotate(Vector3.up, moveDir.x * Time.deltaTime * RotatePower , Space.World);
    }


    //input system 이용하여 움직이기
    private void OnMove(InputValue input)
    {
        //Vector3 inputDir = new Vector3();
        moveDir.x = input.Get<Vector2>().x;
        moveDir.z = input.Get<Vector2>().y;

        //OnMoved?.Invoke(inputDir);
        //animator.SetFloat("Accel", inputDir.sqrMagnitude);
    }

    private void OnJump(InputValue input)
    {
        //bool jump = input.Get<bool>();

        jump();
    }

  

}
