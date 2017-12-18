using UnityEngine;

public class InputControl : MonoBehaviour
{

    public KeyCode Action1;
    public KeyCode Action2;

    public KeyCode Action3;
    public KeyCode Action4;



    [HideInInspector] public bool action1WasPress;
    [HideInInspector] public bool action2WasPress;

    [HideInInspector] public bool action3WasPress;
    [HideInInspector] public bool action4WasPress;

    [HideInInspector] public bool action5WasPress;

    [HideInInspector] public float verticalDir;
    [HideInInspector] public float horizontalDir;

    [HideInInspector] public bool horizontalInput;
    [HideInInspector] public bool verticalInput;

    private bool horizontalInputTMP;
    private bool verticalInputTMP;

    private bool Action1WasPress()
    {
        return Input.GetKeyDown(Action1);
    }

    private bool Action2WasPress()
    {
        return Input.GetKeyDown(Action2);
    }

    private bool Action3WasPress()
    {
        return Input.GetKeyDown(Action3);
    }

    private bool Action4WasPress()
    {
        return Input.GetKeyDown(Action4);
    }

    private bool Action5WasPress()
    {
        return Input.GetKeyDown(KeyCode.Escape);
    }

    private void HorizontalAxis()
    {
        if (Input.GetAxisRaw("Horizontal") != 0f)
        {

            horizontalInput = !horizontalInputTMP ? true : false;

            horizontalInputTMP = true;

            horizontalDir = Input.GetAxisRaw("Horizontal");
        }

        else
        {
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
        action1WasPress = Action1WasPress();
        action2WasPress = Action2WasPress();
        action3WasPress = Action3WasPress();
        action4WasPress = Action4WasPress();
        action5WasPress = Action5WasPress();

        HorizontalAxis();
        VerticalAxis();
    }

}
