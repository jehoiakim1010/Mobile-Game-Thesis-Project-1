using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UImanager : MonoBehaviour
{
    Image Fade;

    Image Attack_UI;

    int Selected_Character;

    Scene scene;

    int Layout;

    RectTransform JoyStick;
    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();


        if (scene.name == "characterAnimation_Chemist" || scene.name == "characterAnimation_Nurse" || scene.name == "characterAnimation_Paramedic")
        {
            Attack_UI = GameObject.Find("/Canvas/AttackBtn/AttackImgButton").GetComponent<Image>();

            Selected_Character = ES3.Load<int>("Character Selected");

            if (Selected_Character == 0)
            {
                Attack_UI.sprite = Resources.Load<Sprite>("Throwing Bottle");
            }

            else if (Selected_Character == 1)
            {
                Attack_UI.sprite = Resources.Load<Sprite>("Parademic Shield");
            }

            else if (Selected_Character == 2)
            {
                Attack_UI.sprite = Resources.Load<Sprite>("Syringe");
            }
        }

        Fade = GameObject.Find("/Canvas/Fade").GetComponent<Image>();

        FadeOut();

    }

    public void SaveLayout1()
    {
        Layout = 1;
        ES3.Save("Layout", Layout, "Game Layout");
    }

    public void SaveLayout2()
    {
        Layout = 2;
        ES3.Save("Layout", Layout, "Game Layout");
    }

    public void FadeIn()
    {
        Fade.enabled = true;

        Fade.CrossFadeAlpha(1, 3.0f, false);
    }

    public void FadeOut()
    {
        Fade.CrossFadeAlpha(0, 3.0f, false);

        StartCoroutine(DisableImage());
    }

    IEnumerator DisableImage()
    {
        yield return new WaitForSeconds(2.0f);

        Fade.enabled = false;
    }
}
