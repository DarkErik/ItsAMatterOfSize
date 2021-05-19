using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    private Resolution[] resolutions;

    [SerializeField] public TMP_Dropdown resolutionDropdown;

    private void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        Resolution[] finalResolutions = new Resolution[resolutions.Length];
        int filled = 0;

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++) 
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            //rausfiltern von Resolutions mit anderen Framerates
            if (options.Contains(option) || resolutions[i].refreshRate != Screen.currentResolution.refreshRate) {
                continue;
            }
            //Hinzufügen der aktuellen Auflösung zum Dropdownmenue
            options.Add(option);
            //Aufnehmen der Auflösung in die Finale Liste und Fuellungsgrad vergroeßern
            finalResolutions[filled] = resolutions[i];
            filled++;

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = filled;
            }
        }
        resolutions = finalResolutions;

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        //Test auf Duplikate (unterschiedliche)
        //Fix richtiger Index
        //Höchste Refreshrate 
    }
 

    public void SetFullscreen(bool isFullscreen)
    {
        Debug.Log("Set Fullscreenmode to: "+isFullscreen);
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        Debug.Log("Set Resolution to: " + resolution.width + " x " + resolution.height);
    }
}
