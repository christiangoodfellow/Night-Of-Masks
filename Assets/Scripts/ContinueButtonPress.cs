using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButtonPress : MonoBehaviour
{
    public GameObject PauseMenu;
    public void ButtonPress()
    {
        PauseMenu.SetActive(false);
    }
}
