using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<GameObject> models = new List<GameObject>(5);

    int NumberTutorial = 0;
    void Start()
    {
        models[0] = GameObject.Find("/Canvas/Tutorial/Image1/Image").gameObject;

        models[1] = GameObject.Find("/Canvas/Tutorial/Image1/Image1").gameObject;


        models[2] = GameObject.Find("/Canvas/Tutorial/Image1/Image2").gameObject;
    }

    private void Update()
    {
        NumberTutorial = Mathf.Clamp(NumberTutorial, 0, 2);
        Tutorial_Change();
    }

    public void Tutorial_Change()
    {
        switch (NumberTutorial)
        {
            case 0:
                models[0].SetActive(true);
                models[1].SetActive(false);
                models[2].SetActive(false);
                break;

            case 1:
                models[0].SetActive(false);
                models[1].SetActive(true);
                models[2].SetActive(false);
                break;

            case 2:
                models[0].SetActive(false);
                models[1].SetActive(false);
                models[2].SetActive(true);
                break;

            default:
                models[0].SetActive(true);
                models[1].SetActive(false);
                models[2].SetActive(false);
                break;
        }
    }


    public void Upward()
    {
        NumberTutorial++;
    }

    public void Downward()
    {
        NumberTutorial--;
    }
}
