﻿using UnityEngine;

public class LeaveGameOnEscapePressed : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
