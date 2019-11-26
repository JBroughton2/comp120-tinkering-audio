using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuChanging : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject settingsPanel;
    public GameObject savePanel;

    //Hides the main panel and activates the settings menu.
   public void ChangeToSettings()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    //Hides the other panels but makes the main panel become visable again.
    public void GoBack()
    {
        mainPanel.SetActive(true);
        settingsPanel.SetActive(false);
        savePanel.SetActive(false);
    }

    //Hides the main manu but sets the save panel to true.
    public void SaveMenu()
    {
        mainPanel.SetActive(false);
        savePanel.SetActive(true);
    }
   
}
