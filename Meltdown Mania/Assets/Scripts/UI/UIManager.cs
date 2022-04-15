using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject darkenImage;
    [SerializeField] GameObject pasueMenu;
    [SerializeField] GameObject controlsScreen;
    public GameObject winScreen;

    private void Update()
    {
        if (Input.GetKeyDown("escape") && pasueMenu != null)
        {
            if (pasueMenu.activeInHierarchy == false && winScreen.activeInHierarchy == false)
            {
                pasueMenu.SetActive(true);
                darkenImage.SetActive(true);
                Time.timeScale = 0;
            }
            else if (pasueMenu.activeInHierarchy == true && winScreen.activeInHierarchy == false && controlsScreen.activeInHierarchy == false)
            {
                pasueMenu.SetActive(false);
                darkenImage.SetActive(false);
                Time.timeScale = 1;
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

        if (menu.name != "Controls")
            darkenImage.SetActive(false);

        if (pasueMenu != null && menu.name == "Pause")
            Time.timeScale = 1;
    }

    // Exits the game
    public void Exit()
    {
        Application.Quit();
    }
}
