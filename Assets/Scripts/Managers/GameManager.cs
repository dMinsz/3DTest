using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private static DataManager dataManager;

    public static GameManager Instance { get { return instance; } }
    public static DataManager Data { get { return dataManager; } }


    private GameManager() { }
    


    private void Awake() // ����Ƽ������ �����ͻ󿡼� �߰��Ҽ� �ֱ⶧���� �̷������α���
    {
        if (instance != null)
        {
            Debug.LogWarning("GameInstance: valid instance already registered.");
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this); // ����Ƽ�� ���� ��ȯ�ϸ� �ڵ����� ������Ʈ���� �����ȴ�
                                 // �ش� �ڵ�� ���� ���ϰ� ����
        instance = this;

        InitManagers();
    }

    private void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }

    private void InitManagers() 
    {
        
        GameObject dobj = new GameObject() { name = "DataManager" };
        dobj.transform.SetParent(transform);
        dataManager = dobj.AddComponent<DataManager>();
        
    }
}
