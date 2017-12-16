using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControl : MonoBehaviour
{

    public float verticalDir;
    public float horizontalDir;

    public bool horizontalInput;
    public bool verticalInput;

    private bool horizontalInputTMP;
    private bool verticalInputTMP;

    private void HorizontalAxis()
    {
        if (Input.GetAxisRaw("Horizontal") != 0f)
        {

            horizontalInput = !horizontalInputTMP ? true : false;

            horizontalInputTMP = true;

            horizontalDir = Input.GetAxisRaw("Horizontal");
        }

        else {
            horizontalInputTMP = false;
        }
    }

    private void VerticalAxis()
    {
        if (Input.GetAxisRaw("Vertical") != 0f)
        {

            verticalInput = !verticalInputTMP ? true : false;

            verticalInputTMP = true;

            verticalDir = Input.GetAxisRaw("Vertical");
        }

        else
        {
            verticalInputTMP = false;
        }
    }

    private void Update()
    {
        HorizontalAxis();
        VerticalAxis();
    }

}
