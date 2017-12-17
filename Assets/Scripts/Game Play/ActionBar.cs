using UnityEngine;

public class ActionBar : MonoBehaviour {

    protected float value;
    public float speed = 1;

    public virtual void Update()
    {
        float progress = Mathf.Cos(speed * Time.time);
        value = Mathf.Clamp(-progress, -1, 1);
    }

}
