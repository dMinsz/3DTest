using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class ShootCountView : MonoBehaviour
{
  
    private TMP_Text textView;
    private StringBuilder sb;

    private void Awake()
    {
        sb = new StringBuilder();
        textView = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        GameManager.Data.OnShootCountChanged.AddListener(ChangeText);
    }

  
    private void OnDisable()
    {
        GameManager.Data.OnShootCountChanged.RemoveListener(ChangeText);
    }


    private void ChangeText(int count)
    {
        sb.Clear();
        sb.Append("Shoot Count : ");
        sb.Append(count);
        textView.text = sb.ToString();
    }
}
