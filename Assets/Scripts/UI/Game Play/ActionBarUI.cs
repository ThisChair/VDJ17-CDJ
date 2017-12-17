using UnityEngine.UI;

public class ActionBarUI : ActionBar {

    private Slider bar;

    private void Start()
    {
        bar = GetComponent<Slider>();
    }

    public override void Update()
    {
        base.Update();
        bar.value = value;
    }
}
