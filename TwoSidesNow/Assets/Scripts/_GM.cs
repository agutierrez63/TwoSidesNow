using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _GM : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        QuitGame();
    }

    void QuitGame() 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
