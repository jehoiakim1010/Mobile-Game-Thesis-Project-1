using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider MainVolumeSlider;

    [SerializeField] Slider SFXVolumeSlider;

    AudioClip PlayerHit, Button_Pressed, Heal, Rage, GolemAttack, ItemCollect;
    AudioSource BGMusic, SFX;

    List<GameObject> MainMusic = new List<GameObject>(10);
    List<GameObject> SFXMusic = new List<GameObject>(10);
    GameObject[] MainMusicStorage;
    GameObject[] SFXStorage;

    // Start is called before the first frame update
    void Start()
    {
        Button_Pressed = Resources.Load<AudioClip>("Audio/80921__justinbw__buttonchime02up");

        PlayerHit = Resources.Load<AudioClip>("Audio/77611__joelaudio__sfx-attack-sword-001");

        Heal = Resources.Load<AudioClip>("Audio/587603__eminyildirim__heal-short");

        Rage = Resources.Load<AudioClip>("Audio/133018__cosmicd__annulet-of-rage");

        GolemAttack = Resources.Load<AudioClip>("Audio/420250__redroxpeterpepper__monster-attack");

        ItemCollect = Resources.Load<AudioClip>("Audio/545238__mr-fritz__item-sparkle");

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            BGMusic = GameObject.Find("/Main Audio").GetComponent<AudioSource>();

            SFX = GameObject.Find("/SFX Audio").GetComponent<AudioSource>();

            MainMusicStorage = GameObject.FindGameObjectsWithTag("Main");

            SFXStorage = GameObject.FindGameObjectsWithTag("SFX");

            foreach (GameObject main in MainMusicStorage)
            {
                MainMusic.Add(main);
            }

            foreach (GameObject main in SFXStorage)
            {
                SFXMusic.Add(main);
            }

            if (ES3.FileExists("Game Volume"))
            {
                for (int i = 0; i < MainMusicStorage.Length; i++)
                {
                    MainMusic[i].GetComponent<AudioSource>().volume = ES3.Load<float>("Main Volume", "Game Volume");

                    MainVolumeSlider.value = ES3.Load<float>("Main Volume", "Game Volume");
                }
                for (int i = 0; i < SFXStorage.Length; i++)
                {
                    SFXMusic[i].GetComponent<AudioSource>().volume = ES3.Load<float>("SFX Volume", "Game Volume");

                    SFXVolumeSlider.value = ES3.Load<float>("SFX Volume", "Game Volume");
                }
            }
        }
        else
        {
            if (ES3.FileExists("Game Volume"))
            {
                MainMusicStorage = GameObject.FindGameObjectsWithTag("Main");

                SFXStorage = GameObject.FindGameObjectsWithTag("SFX");

                foreach (GameObject main in MainMusicStorage)
                {
                    MainMusic.Add(main);
                }

                foreach (GameObject main in SFXStorage)
                {
                    SFXMusic.Add(main);
                }

                for (int i = 0; i < MainMusicStorage.Length; i++)
                {
                    MainMusic[i].GetComponent<AudioSource>().volume = ES3.Load<float>("Main Volume", "Game Volume");
                }
                for (int i = 0; i < SFXStorage.Length; i++)
                {
                    SFXMusic[i].GetComponent<AudioSource>().volume = ES3.Load<float>("SFX Volume", "Game Volume");
                }
            }

            if (SceneManager.GetActiveScene().name == "characterAnimation_Chemist" || SceneManager.GetActiveScene().name == "characterAnimation_Paramedic" ||
            SceneManager.GetActiveScene().name == "characterAnimation_Nurse")
            {
                BGMusic = GameObject.Find("/SpawnPoint/Audio Source").GetComponent<AudioSource>();

                SFX = GameObject.Find("/SpawnPoint/SFXMusic").GetComponent<AudioSource>();
            }


        }

    }
    public void ChangeVolume()
    {
        for (int i = 0; i < MainMusicStorage.Length; i++)
        {
            MainMusic[i].GetComponent<AudioSource>().volume = MainVolumeSlider.value;
        }
        for (int i = 0; i < SFXStorage.Length; i++)
        {
            SFXMusic[i].GetComponent<AudioSource>().volume = SFXVolumeSlider.value;
        }
        //Save();
    }

    /*
        private void Load()
        {
            volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        }

        private void Save()
        {
            PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
        }
    */
    public void SaveSettings()
    {
        ES3.Save("Main Volume", MainVolumeSlider.value, "Game Volume");

        ES3.Save("SFX Volume", SFXVolumeSlider.value, "Game Volume");
    }
    
    public void PlaySound(string SoundName)
    {
        switch (SoundName)
        {
            case "PlayerHit":
                SFX.PlayOneShot(PlayerHit);
                break;

            case "Button_Pressed":
                SFX.PlayOneShot(Button_Pressed);
                break;

            case "Heal":
                SFX.PlayOneShot(Heal);
                break;

            case "Rage":
                SFX.PlayOneShot(Rage);
                break;

            case "GolemAttack":
                SFX.PlayOneShot(GolemAttack);
                break;

            case "ItemCollect":
                SFX.PlayOneShot(ItemCollect);
                break;
        }
    }
}
