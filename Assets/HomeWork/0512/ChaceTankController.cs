using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaceTankController : MonoBehaviour
{
    public GameObject Target;// ī�޶� ����ٴ� Ÿ��

    public float Speed = 10.0f; // �����Ӽӵ�

    private Vector3 TargetPos;

    public float offsetX = 0.0f;          
    public float offsetY = 0.0f;         
    public float offsetZ = -3.0f;          

    void FixedUpdate()
    {
        // Ÿ���� x, y, z ��ǥ�� ī�޶��� ��ǥ�� ���Ͽ� ī�޶��� ��ġ�� ����
        TargetPos = new Vector3(
            Target.transform.position.x + offsetX,
            Target.transform.position.y + offsetY,
            Target.transform.position.z + offsetZ
            );

        // ī�޶��� �������� �ε巴�� �ϴ� �Լ�(Lerp)
        transform.position = Vector3.Lerp(transform.position, TargetPos, Time.deltaTime);

        transform.Rotate(Vector3.up, TargetPos.x * Time.deltaTime * Speed, Space.World);
    }
}
