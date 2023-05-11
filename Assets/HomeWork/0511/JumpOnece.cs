using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpOnece : MonoBehaviour
{
    private bool IsJump = false;
    private Rigidbody rgd;

    public float JumpPower = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        rgd = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsJump)
        {
            Debug.Log(name + "�� ��������");
            rgd.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
            IsJump = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log(name+"�� ������Ҵ�.");
            IsJump = true;
        }
    }
}
