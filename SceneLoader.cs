using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    UImanager uimanager;
    //SceneLoader
    bool DisableClick;
    int Character_Selected;

    Scene scene;

    private void Start()
    {
        uimanager = GameObject.FindObjectOfType<UImanager>();

        scene = SceneManager.GetActiveScene();

        if (scene.name == "Tutorial")
        {
            Character_Selected = ES3.Load<int>("Character Selected");
        }
    }
    public void LoadNextScene()
    {
        if (DisableClick == false)
        {
            StartCoroutine(MoveToCharacterSelection());

            DisableClick = true;
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadMainGame()
    {
        SceneManager.LoadScene("CharacterSelection");
    }

    public void LoadTheTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void LoadTheGame()
    {
        if (Character_Selected == 0)
        {
            SceneManager.LoadScene("characterAnimation_Chemist");
        }

        else if (Character_Selected == 1)
        {
            SceneManager.LoadScene("characterAnimation_Paramedic");
        }

        else if (Character_Selected == 2)
        {
            SceneManager.LoadScene("characterAnimation_Nurse");
        }
    }

    IEnumerator MoveToCharacterSelection()
    {
        uimanager.FadeIn();
        DisableClick = false;

        yield return new WaitForSeconds(3.0f);

        if (ES3.FileExists("SaveFile.es3"))
        {
            ES3.DeleteFile("SaveFile.es3");
        }

        SceneManager.LoadScene("Story");
    }

}
