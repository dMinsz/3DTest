using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurretRotater : MonoBehaviour
{
    public GameObject tankTurret;
    [SerializeField] private float MouseSensitive = 0.5f;
    [SerializeField] private bool InvertVertical = true;

    public void RotateTurret(Vector2 pos)
    {

        var deltaMouse = new Vector2(pos.x, pos.y);
        deltaMouse.x %= Screen.width; // ��ũ�� width ������ ���콺 ��Ÿ ���� �� �ٿ��ش�.

        float middle = Screen.width / 2f; // ��ũ�� ��� �� ���ؼ�

        float offset = deltaMouse.x - middle; // offset�� �༭  ���콺�� �̵��� �� ���� �ȵ��ư��� �ٲ۴�.
        offset *= MouseSensitive;

        Vector2 deltaRotation = deltaMouse;
        deltaRotation.x *= InvertVertical ? 1.0f : -1.0f; // �¿� ���� �ʿ�� ��밡��

        offset = Mathf.Clamp(offset, -140.0f, 140.0f); // -140�� 140�� �� ����


        tankTurret.transform.localRotation = Quaternion.Euler(0f, offset, 0f);
    }

}
