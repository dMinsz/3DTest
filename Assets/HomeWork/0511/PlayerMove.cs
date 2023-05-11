using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMove: MonoBehaviour
{
    [SerializeField]
    public float jumpSpeed = 5.0f;
    [SerializeField]
    public float moveSpeed = 5.0f;

    [SerializeField]
    public float rotateSpeed = 3.0f;

    [SerializeField]
    public float rotatePower = 10.0f;

    private Rigidbody rgd;

    private bool IsJump = true;

    // Start is called before the first frame update
    void Start()
    {
        rgd = GetComponent<Rigidbody>(); // set rigidbody
        name = "Player"; // set name
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = new Vector3(
            Input.GetAxis("Horizontal"),
            0,
            Input.GetAxis("Vertical")
        );
        // 이동방향 * 속도 * 프레임단위 시간을 곱해서 오브젝트 이동
        transform.Translate(dir * moveSpeed * Time.deltaTime);


        float yRotateSize = Input.GetAxis("Mouse X") * rotateSpeed;//마우스의 횡이동을 가져와서
        float yRotate = transform.eulerAngles.y + yRotateSize;

        transform.eulerAngles = new Vector3(-Input.GetAxis("Mouse Y"), yRotate, 0); // 회전 시켜준다.

        if (Input.GetKeyDown(KeyCode.Space) && IsJump) // 스페이스 키가 눌리고 점프를 할수있다면
        {
            Debug.Log(name + "이 점프상태");
            rgd.AddForce(Vector3.up * jumpSpeed ,ForceMode.Impulse); //점프 를 한다.
            IsJump = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // 땅에 닿으면 점프를 할 수 있게 구현
        {
            Debug.Log(name + "이 땅에닿았다.");
            IsJump = true;
        }
    }
}
