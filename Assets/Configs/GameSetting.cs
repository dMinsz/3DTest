using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �������ڸ��� �ؾ��ϴ� �۾����� ���� Ŭ����
public class GameSetting 
{
    //���ӽ������ڸ��� �Ʒ��� �Լ��� ȣ�����ش�.
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    { 
        if (GameManager.Instance == null)
        {
            GameObject gameManager = new GameObject() { name = "GameManager" };
            gameManager.AddComponent<GameManager>();
        }
    }
}
