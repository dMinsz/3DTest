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
        deltaMouse.x %= Screen.width; // 스크린 width 값으로 마우스 델타 값을 좀 줄여준다.

        float middle = Screen.width / 2f; // 스크린 가운데 를 구해서

        float offset = deltaMouse.x - middle; // offset을 줘서  마우스의 이동을 좀 휙휙 안돌아가게 바꾼다.
        offset *= MouseSensitive;

        Vector2 deltaRotation = deltaMouse;
        deltaRotation.x *= InvertVertical ? 1.0f : -1.0f; // 좌우 반전 필요시 사용가능

        offset = Mathf.Clamp(offset, -140.0f, 140.0f); // -140도 140도 만 돌게


        tankTurret.transform.localRotation = Quaternion.Euler(0f, offset, 0f);
    }

}
