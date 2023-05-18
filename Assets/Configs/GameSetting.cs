using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임 시작하자마자 해야하는 작업들을 위한 클래스
public class GameSetting 
{
    //게임시작하자마자 아래의 함수를 호출해준다.
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
