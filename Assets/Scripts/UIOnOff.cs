using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOnOff : MonoBehaviour
{
    public GameObject canvas;
    bool check = true;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (check)
            {
                canvas.gameObject.SetActive(false);
                check = false;
            }
            else
            {
                canvas.gameObject.SetActive(true);
                check = true;
            }
        }
    }
}
