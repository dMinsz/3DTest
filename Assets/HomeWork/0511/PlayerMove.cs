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
        // �̵����� * �ӵ� * �����Ӵ��� �ð��� ���ؼ� ������Ʈ �̵�
        transform.Translate(dir * moveSpeed * Time.deltaTime);


        float yRotateSize = Input.GetAxis("Mouse X") * rotateSpeed;//���콺�� Ⱦ�̵��� �����ͼ�
        float yRotate = transform.eulerAngles.y + yRotateSize;

        transform.eulerAngles = new Vector3(-Input.GetAxis("Mouse Y"), yRotate, 0); // ȸ�� �����ش�.

        if (Input.GetKeyDown(KeyCode.Space) && IsJump) // �����̽� Ű�� ������ ������ �Ҽ��ִٸ�
        {
            Debug.Log(name + "�� ��������");
            rgd.AddForce(Vector3.up * jumpSpeed ,ForceMode.Impulse); //���� �� �Ѵ�.
            IsJump = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // ���� ������ ������ �� �� �ְ� ����
        {
            Debug.Log(name + "�� ������Ҵ�.");
            IsJump = true;
        }
    }
}
