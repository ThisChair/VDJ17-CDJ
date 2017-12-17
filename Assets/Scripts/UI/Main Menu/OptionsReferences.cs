using UnityEngine;
using UnityEngine.UI;

public class OptionsReferences : MonoBehaviour
{
    [HideInInspector] public Image image;
    [HideInInspector] public Button button;
    [HideInInspector] public Text text;
    [HideInInspector] public Dropdown dropdown;
    public bool isDropdownField;

    public OptionsReferences up = null;
	public OptionsReferences down = null;
	public OptionsReferences left = null;
	public OptionsReferences right = null;

	void Start ()
	{
        image = GetComponent<Image>();
        text = GetComponentInChildren<Text>();
        isDropdownField = gameObject.HasComponent<Dropdown>();

        if (isDropdownField)
        {
            dropdown = GetComponent<Dropdown>();
        }

        else
        {
            button = GetComponent<Button>();
        }

    }
}