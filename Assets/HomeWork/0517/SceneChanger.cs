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

    // <�� �߰�>
    public void AddSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    public void AddSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Additive);
    }

    // <�񵿱� �� �ε�>
    public void ChangeSceneASync(string sceneName)
    {
        // �񵿱� �� �ε� : ��׶���� ���� �ε��ϵ��� �Ͽ� ���� �� ������ ������ ��
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        asyncOperation.allowSceneActivation = true;     // �� �ε� �Ϸ�� �ٷ� �� �ε带 �����ϴ��� ����
        bool isLoad = asyncOperation.isDone;            // �� �ε��� �Ϸ�Ǿ����� �Ǵ�
        float progress = asyncOperation.progress;       // �� �ε��� Ȯ��
        asyncOperation.completed += (oper) => { };      // �� �ε� �Ϸ�� ������ �̺�Ʈ �߰�
    }

    // <Don't destroy on load>
    // ���� ��ȯ���� ���ŵ��� �ʱ� ���ϴ� ���ӿ�����Ʈ�� ��� �������� �ʴ� ���� ������Ʈ�� �߰��ϴ� ����� ���
    // (���� ����� ���� ���� ���� �ε�ÿ� ���ŵ��� �ʴ� ���� �����ϴ� ���)
    public void SetDontDestroyOnLoad(GameObject go)
    {
        DontDestroyOnLoad(go);
    }

}
