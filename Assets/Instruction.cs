using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionMenu : MonoBehaviour
{
    public void Instruction()
    {
        SceneManager.LoadScene(2);
    }

    public void Quit()
    {
        SceneManager.LoadScene(0);

    }
}
