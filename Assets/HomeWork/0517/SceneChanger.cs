using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void ChangeSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }

    // <씬 추가>
    public void AddSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    public void AddSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Additive);
    }

    // <비동기 씬 로드>
    public void ChangeSceneASync(string sceneName)
    {
        // 비동기 씬 로드 : 백그라운드로 씬을 로딩하도록 하여 게임 중 멈춤이 없도록 함
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        asyncOperation.allowSceneActivation = true;     // 씬 로딩 완료시 바로 씬 로드를 진행하는지 여부
        bool isLoad = asyncOperation.isDone;            // 씬 로딩이 완료되었는지 판단
        float progress = asyncOperation.progress;       // 씬 로딩률 확인
        asyncOperation.completed += (oper) => { };      // 씬 로딩 완료시 진행할 이벤트 추가
    }

    // <Don't destroy on load>
    // 씬의 전환에도 제거되지 않기 원하는 게임오브젝트의 경우 지워지지 않는 씬의 오브젝트로 추가하는 방법을 사용
    // (동작 방법은 다중 씬을 통한 로드시에 제거되지 않는 씬을 구성하는 방법)
    public void SetDontDestroyOnLoad(GameObject go)
    {
        DontDestroyOnLoad(go);
    }

}
