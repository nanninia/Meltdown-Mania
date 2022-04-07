using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject darkenImage;
    [SerializeField] GameObject pasueMenu;

    private void Update()
    {
        if (Input.GetKeyDown("escape") && pasueMenu != null)
        {
            if (pasueMenu.activeInHierarchy == false)
            {
                pasueMenu.SetActive(true);
                darkenImage.SetActive(true);
            }
            else
            {
                pasueMenu.SetActive(false);
                darkenImage.SetActive(false);
            }
        }
    }

    // Opens the passed in menu
    public void OpenMenu(GameObject menu)
    {
        menu.SetActive(true);
        darkenImage.SetActive(true);
    }

    // Closes the passed in menu
    public void CloseMenu(GameObject menu)
    {
        menu.SetActive(false);
        darkenImage.SetActive(false);
    }

    // Exits the game
    public void Exit()
    {
        Application.Quit();
    }
}
