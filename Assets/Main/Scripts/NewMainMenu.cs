using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewMainMenu : MonoBehaviour
{
    public GameObject instructions;
    public GameObject tittle;
    public GameObject mainMenu;

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(0);

    }
    public void Instruction()
    {
        mainMenu.SetActive(false);
        tittle.SetActive(false);
        instructions.SetActive(true);

    }
}
