using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private Slider slider;

    AsyncOperation operation;


    private void Start()
    {
        StartCoroutine(StartLoader());
    }

    IEnumerator StartLoader()
    {
        operation = SceneManager.LoadSceneAsync(ES3.Load<string>("Scene"));

        while (!operation.isDone)
        {
            float Progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = Progress;

            if (Progress >= 0.9f)
            {
                SceneManager.LoadScene(ES3.Load<string>("Scene"));
            }

            yield return null;
        }
    }
}
