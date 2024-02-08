using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitListener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Cursor.visible = false;
        }
        if (Input.GetKeyDown(KeyCode.CapsLock))
        {
            Cursor.visible = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            print("esc received");
            Application.Quit();
        }
    }
}
