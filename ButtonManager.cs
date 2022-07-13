using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ButtonManager : MonoBehaviour
{
    public bool isInventory;
    public bool isQuestLog;

    SoundManager soundManager;
    public bool isPause;

    UImanager uImanager;

    inventory inventory;

    GameObject QuestLog;

    GameObject Pause;

    int Character_Selected;

    GameObject Chemist, Parademic, Nurse;

    PlayerStatus playerstatus;

    ObjectiveManager objectiveManager;

    Scene scene;
    private void Awake()
    {
        uImanager = GameObject.FindObjectOfType<UImanager>() as UImanager;

        soundManager = GameObject.FindObjectOfType<SoundManager>() as SoundManager;

        objectiveManager = GameObject.FindObjectOfType<ObjectiveManager>() as ObjectiveManager;

        scene = SceneManager.GetActiveScene();

        if (scene.name == "characterAnimation_Chemist")
        {
            Chemist = GameObject.Find("/chemist").gameObject;
        }

        else if (scene.name == "characterAnimation_Paramedic")
        {
            Parademic = GameObject.Find("/paramedic").gameObject;
        }

        else if (scene.name == "characterAnimation_Nurse")
        {
            Nurse = GameObject.Find("/nurse").gameObject;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;

        if (scene.name == "characterAnimation_Chemist" || scene.name == "characterAnimation_Paramedic" || scene.name == "characterAnimation_Nurse")
        {
            Character_Selected = ES3.Load<int>("Character Selected");

            QuestLog = GameObject.Find("/Canvas/QuestLog").gameObject;

            Pause = GameObject.Find("/Canvas/PauseMenu").gameObject;

            if (Character_Selected == 0)
            {
                playerstatus = Chemist.GetComponent<PlayerStatus>();

                inventory = Chemist.GetComponent<inventory>();
            }

            else if (Character_Selected == 1)
            {
                playerstatus = Parademic.GetComponent<PlayerStatus>();

                inventory = Parademic.GetComponent<inventory>();
            }

            else if (Character_Selected == 2)
            {
                playerstatus = Nurse.GetComponent<PlayerStatus>();

                inventory = Nurse.GetComponent<inventory>();
            }

            if (PersistentData.isLoadGame == true)
            {
                StartCoroutine(DelayLoading());

                PersistentData.isLoadGame = false;
            }

            if (ES3.KeyExists("Player Level"))
            {
                playerstatus.Level = ES3.Load<int>("Player Level");
            }
        }
    }

    IEnumerator DelayLoading()
    {
        yield return new WaitForSeconds(0.1f);

        LoadData();
    }

    public void Resume()
    {
        soundManager.PlaySound("Button_Pressed");
        isPause = false;
        Time.timeScale = 1.0f;
    }

    public void Load()
    {
        if (ES3.KeyExists("Position"))
        {
            PersistentData.isLoadGame = true;

            StartCoroutine(LoadGame());
        }
    }

    IEnumerator LoadGame()
    {
        soundManager.PlaySound("Button_Pressed");
        uImanager.FadeIn();

        yield return new WaitForSeconds(3.0f);

        SceneManager.LoadScene("Loading");
    }
    public void ExitGame()
    {
        Application.Quit();
    }


    public void btnInventory()
    {
        soundManager.PlaySound("Button_Pressed");
        isInventory = !isInventory;
        isQuestLog = false;
        isPause = false;
    }
    public void btnQuestLog()
    {
        soundManager.PlaySound("Button_Pressed");
        isQuestLog = !isQuestLog;
        isInventory = false;
        isPause = false;
    }

    public void btnPause()
    {
        soundManager.PlaySound("Button_Pressed");
        isPause = !isPause;
        isQuestLog = false;
        isInventory = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (scene.name == "characterAnimation_Chemist" || scene.name == "characterAnimation_Paramedic" || scene.name == "characterAnimation_Nurse")
        {

            if (isQuestLog == true)
            {
                if (QuestLog.activeSelf == false)
                {
                    QuestLog.SetActive(true);
                }
            }
            else
            {
                if (QuestLog.activeSelf == true)
                {
                    QuestLog.SetActive(false);
                }
            }

            if (isPause == true)
            {
                if (Pause.activeSelf == false)
                {
                    Pause.SetActive(true);
                    Time.timeScale = 0.0f;
                }
            }
            else
            {
                if (Pause.activeSelf == true)
                {
                    Pause.SetActive(false);
                    Time.timeScale = 1.0f;
                }
            }

        }
    }


    public void SaveData()
    {
        soundManager.PlaySound("Button_Pressed");
        if (Character_Selected == 0)
        {
            ES3.Save("Position", Chemist.transform.position);
        }

        else if (Character_Selected == 1)
        {
            ES3.Save("Position", Parademic.transform.position);
        }

        else if (Character_Selected == 2)
        {
            ES3.Save("Position", Nurse.transform.position);
        }

        ES3.Save("Scene", SceneManager.GetActiveScene().name);

        /*
        ES3.Save("Player Health", playerstatus.health);

        ES3.Save("Player Mana", playerstatus.mana);
*/
        ES3.Save("Player Exp", playerstatus.exp);

        /*
        ES3.Save("Player Health Slider", playerstatus.playerHealth);

        ES3.Save("Player Mana Slider", playerstatus.playerMana);
        */
        ES3.Save("Player EXP Slider", playerstatus.playerEXP);

        ES3.Save("FirstQuest", objectiveManager.isFirstQuestDone);

        ES3.Save("SecondQuest", objectiveManager.isSecondQuestDone);

        ES3.Save("ThirdQuest", objectiveManager.isThirdQuestDone);

        ES3.Save("FourthQuest", objectiveManager.isFourthQuestDone);

        ES3.Save("Player Level", playerstatus.Level);

        ES3.Save("Inventory", inventory.GameObject_Slots);

        ES3.Save("Number Of Items", inventory.NumberOfItemsInSlot);

        ES3.Save("Time Limit", objectiveManager.Timer);

        ES3.Save("Monster 1", objectiveManager.MiniMonster);

        ES3.Save("Monster 2", objectiveManager.MiniMonster_1);

        ES3.Save("Boss 1", objectiveManager.Boss_1);

        ES3.Save("Boss 2", objectiveManager.Boss_2);
    }

    public void LoadData()
    {

        if (Character_Selected == 0)
        {
            Chemist.transform.position = ES3.Load<Vector3>("Position");
        }

        else if (Character_Selected == 1)
        {
            Parademic.transform.position = ES3.Load<Vector3>("Position");
        }

        else if (Character_Selected == 2)
        {
            Nurse.transform.position = ES3.Load<Vector3>("Position");
        }

        /*
        playerstatus.health = ES3.Load<float>("Player Health");
        playerstatus.playerHealth = ES3.Load<Slider>("Player Health Slider");

        playerstatus.mana = ES3.Load<float>("Player Mana");
        playerstatus.playerMana = ES3.Load<Slider>("Player Mana Slider");
*/
        playerstatus.exp = ES3.Load<float>("Player Exp");
        playerstatus.playerEXP = ES3.Load<Slider>("Player EXP Slider");

        objectiveManager.isFirstQuestDone = ES3.Load<bool>("FirstQuest");

        objectiveManager.isSecondQuestDone = ES3.Load<bool>("SecondQuest");

        objectiveManager.isThirdQuestDone = ES3.Load<bool>("ThirdQuest");

        objectiveManager.isFourthQuestDone = ES3.Load<bool>("FourthQuest");

        playerstatus.Level = ES3.Load<int>("Player Level");

        objectiveManager.Timer = ES3.Load<float>("Time Limit");

        //inventory.GameObject_Slots = ES3.Load<string[]>("Inventory");

    }
}
