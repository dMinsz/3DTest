using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DataManager : MonoBehaviour
{
    [SerializeField] private int ShootCount = 0;

    public UnityEvent<int> OnShootCountChanged = new UnityEvent<int>();

    public void AddShootCount(int count) 
    {
        ShootCount += count;
        OnShootCountChanged?.Invoke(ShootCount);
    }
}
