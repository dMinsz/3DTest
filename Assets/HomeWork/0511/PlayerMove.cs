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
        rgd = GetComponent<Rigidbody>();
        name = "Player";
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = new Vector3(
            Input.GetAxis("Horizontal"),
            0,
            Input.GetAxis("Vertical")
        );
        // 이동방향 * 속도 * 프레임단위 시간을 곱해서 카메라의 트랜스폼을 이동
        transform.Translate(dir * moveSpeed * Time.deltaTime);


        float yRotateSize = Input.GetAxis("Mouse X") * rotateSpeed;
        float yRotate = transform.eulerAngles.y + yRotateSize;

        transform.eulerAngles = new Vector3(-Input.GetAxis("Mouse Y"), yRotate, 0);

        if (Input.GetKeyDown(KeyCode.Space) && IsJump)
        {
            Debug.Log(name + "이 점프상태");
            rgd.AddForce(Vector3.up * jumpSpeed ,ForceMode.Impulse);
            IsJump = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log(name + "이 땅에닿았다.");
            IsJump = true;
        }
    }
}
