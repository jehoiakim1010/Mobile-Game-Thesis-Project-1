using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class characterSelection : MonoBehaviour
{
    //Create A Array of GameObject and called it Models
    private List<GameObject> models;
    //set the selectedcharacter
    public int selectedcharacter = 0;

    UImanager uimanager;

    Button Confirm;
    bool isPlayerSelected;

    GameObject Character_Description, Chemist_Description, Parademic_Description, Nurse_Description;

    void Start()
    {
        Character_Description = GameObject.Find("/Canvas/Character Descriptions").gameObject;

        Confirm = GameObject.Find("/Canvas/Panel/confirm").GetComponent<Button>();

        Chemist_Description = GameObject.Find("/Canvas/Character Descriptions/Chemist").gameObject;

        Parademic_Description = GameObject.Find("/Canvas/Character Descriptions/Parademic").gameObject;

        Nurse_Description = GameObject.Find("/Canvas/Character Descriptions/Nurse").gameObject;

        Confirm.interactable = false;

        Character_Description.SetActive(false);
        //Create A list
        models = new List<GameObject>();

        uimanager = GameObject.FindObjectOfType<UImanager>() as UImanager;

        //For Each Transform in gameObject it will add into models
        foreach (Transform transformOfCharacter in transform)
        {
            models.Add(transformOfCharacter.gameObject);
            //all will be disabled first
            transformOfCharacter.gameObject.SetActive(false);

        }


    }

    private void Update()
    {
        if (selectedcharacter == 0)
        {
            Chemist_Description.SetActive(true);
            Parademic_Description.SetActive(false);
            Nurse_Description.SetActive(false);
        }

        else if (selectedcharacter == 1)
        {
            Chemist_Description.SetActive(false);
            Parademic_Description.SetActive(true);
            Nurse_Description.SetActive(false);
        }

        else if (selectedcharacter == 2)
        {
            Chemist_Description.SetActive(false);
            Parademic_Description.SetActive(false);
            Nurse_Description.SetActive(true);
        }
    }

    public void CharacterSelect(int select)
    {

        Character_Description.SetActive(true);

        Confirm.interactable = true;

        //set the select first into false
        models[selectedcharacter].SetActive(false);

        //Set The selectedcharacter into Select number
        selectedcharacter = select;


        //Set The model selectedcharacter into true
        models[selectedcharacter].SetActive(true);
    }

    public void SelectedCharacter()
    {
        ES3.Save("Player Level", 0);

        switch (selectedcharacter)
        {
            case 0:
                PersistentData.Selected_Character = 0;
                ES3.Save("Character Selected", PersistentData.Selected_Character);
                StartCoroutine(DelayOpening());

                break;
            case 1:

                PersistentData.Selected_Character = 1;
                ES3.Save("Character Selected", PersistentData.Selected_Character);
                StartCoroutine(DelayOpening());
                break;
            case 2:
                PersistentData.Selected_Character = 2;
                ES3.Save("Character Selected", PersistentData.Selected_Character);
                StartCoroutine(DelayOpening());
                break;

        }

        IEnumerator DelayOpening()
        {
            uimanager.FadeIn();

            yield return new WaitForSeconds(3.0f);


            SceneManager.LoadScene("PreStory");
        }
    }
}
