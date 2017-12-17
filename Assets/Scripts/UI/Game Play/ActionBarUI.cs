using UnityEngine.UI;

public class ActionBarUI : ActionBar {

    public float rightmost;
    public float leftmost;

    private Slider bar;
    public InputControl input;

    private void Start()
    {
        bar = GetComponent<Slider>();
    }

    public override void Update()
    {
        base.Update();
        bar.value = value;

        if (bar.value >= rightmost && bar.value <= leftmost) {
            if (input.action2WasPress) {
                print("bien");
            }
        }

        if (bar.value < rightmost || bar.value > leftmost)
        {
            if (input.action2WasPress)
            {
                print("mal");
            }
        }
    }
}
