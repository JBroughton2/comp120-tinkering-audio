using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuChanging : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject settingsPanel;

   public void ChangeToSettings()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void GoBack()
    {
        mainPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }
   
}
