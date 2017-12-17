using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubMenuController : MenuController
{

    public Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        currentResolutionIndex = options.Count - 1 - currentResolutionIndex;
        options.Reverse();
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutions.Length - 1 - resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public override void Update()
    {

        base.Update();

        if (input.horizontalInput && input.horizontalDir == -1 && uiItemSeletected.left != null)
        {

            if (uiItemSeletected.left.isDropdownField)
            {
                NextOption(uiItemSeletected.left, -1);

            }
        }

        if (input.horizontalInput && input.horizontalDir == 1 && uiItemSeletected.right != null)
        {

            if (uiItemSeletected.right.isDropdownField)
            {
                NextOption(uiItemSeletected.right, 1);

            }
        }
    }

    public void NextOption(OptionsReferences nextOption, int sign = 1)
    {
        nextOption.dropdown.value = (nextOption.dropdown.value + sign) % nextOption.dropdown.options.Count;
        nextOption.dropdown.RefreshShownValue();
    }
}
