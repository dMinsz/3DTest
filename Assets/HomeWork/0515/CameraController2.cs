using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController2 : MonoBehaviour
{
    public GameObject Target;// ī�޶� ����ٴ� Ÿ��

    public float CameraSpeed = 10.0f; // ī�޶��� �ӵ�

    private Vector3 TargetPos;

    public float offsetX = 0.0f;            // ī�޶��� x��ǥ
    public float offsetY = 10.0f;           // ī�޶��� y��ǥ
    public float offsetZ = 0.0f;          // ī�޶��� z��ǥ

    void FixedUpdate()
    {
        // Ÿ���� x, y, z ��ǥ�� ī�޶��� ��ǥ�� ���Ͽ� ī�޶��� ��ġ�� ����
        TargetPos = new Vector3(
            Target.transform.position.x + offsetX,
            Target.transform.position.y + offsetY,
            Target.transform.position.z + offsetZ
            );

        // ī�޶��� �������� �ε巴�� �ϴ� �Լ�(Lerp)
        transform.position = Vector3.Lerp(transform.position, TargetPos, Time.deltaTime * CameraSpeed);
    }
}