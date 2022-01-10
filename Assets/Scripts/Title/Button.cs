using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public void TitleScene()
    {
        UnLockCursor();
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void MainPlayScene()
    {
        LockCursor();
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void TutorialScene()
    {
        LockCursor();
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }

    public void ExitScene()
    {
        UnLockCursor();
        Time.timeScale = 1;
        Application.Quit();
    }

    private void UnLockCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    private void LockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
