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
    


    private void Awake() // 유니티에서는 에디터상에서 추가할수 있기때문에 이런식으로구현
    {
        if (instance != null)
        {
            Debug.LogWarning("GameInstance: valid instance already registered.");
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this); // 유니티는 씬을 전환하면 자동으로 오브젝트들이 삭제된다
                                 // 해당 코드로 삭제 안하고 유지
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
