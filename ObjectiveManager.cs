using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Animancer;
public class ObjectiveManager : MonoBehaviour
{
    public Quest quest;
    bool[] isQuestDoneLoading = new bool[20];

    TextMeshProUGUI Quest_Title;
    TextMeshProUGUI Quest_Description;
    GameObject GameOver;

    GameObject Wall1, Wall2, Wall3;


    public bool isFirstQuestDone, isSecondQuestDone, isThirdQuestDone, isFourthQuestDone, isFifthQuestDone;
    Text Time_limit_text;

    public float Timer;

    int Character_Selected;

    //First Monster
    [SerializeField] GameObject Monster1;

    GameObject MiniMonsterSpawnPoint;

    GameObject[] ArrayMiniMonster;
    public List<GameObject> MiniMonster = new List<GameObject>();

    //Second Monster
    [SerializeField] GameObject Monster2;

    GameObject MiniMonster_1_SpawnPoint;
    public List<GameObject> MiniMonster_1 = new List<GameObject>();

    GameObject[] ArrayMiniMonster_1;

    //Mini Boss
    [SerializeField] GameObject go_Boss_1;

    GameObject[] ArrayMiniBoss;

    GameObject MiniBoss_SpawnPoint;

    public List<GameObject> Boss_1 = new List<GameObject>();

    //Final Boss

    [SerializeField] GameObject go_Boss_2;

    GameObject[] ArrayFinalBoss;

    GameObject FinalBoss_SpawnPoint;

    public List<GameObject> Boss_2 = new List<GameObject>();

    bool EnableQuest;
    private void Awake()
    {
        Monster1 = Resources.Load<GameObject>("Mini Monster");
        Monster2 = Resources.Load<GameObject>("Mini Monster 1");
        go_Boss_1 = Resources.Load<GameObject>("MonsterIdle Variant");
        go_Boss_2 = Resources.Load<GameObject>("Final Boss Variant");

        Character_Selected = ES3.Load<int>("Character Selected");
        //First Enemy
        //Array
        ArrayMiniMonster = GameObject.FindGameObjectsWithTag("MiniGolem");

        //Spawnpoint
        MiniMonsterSpawnPoint = GameObject.Find("/Enemies/MiniMonster Spawnpoint").gameObject;

        if (ES3.KeyExists("Monster 1"))
        {
            MiniMonster = ES3.Load<List<GameObject>>("Monster 1");
        }


        //Second Enemy
        ArrayMiniMonster_1 = GameObject.FindGameObjectsWithTag("MiniGolem_1");

        MiniMonster_1_SpawnPoint = GameObject.Find("/Enemies/MiniMonster_1 Spawnpoint").gameObject;

        if (ES3.KeyExists("Monster 2"))
        {
            MiniMonster_1 = ES3.Load<List<GameObject>>("Monster 2");
        }

        //Mini Boss
        ArrayMiniBoss = GameObject.FindGameObjectsWithTag("Enemy_1");

        MiniBoss_SpawnPoint = GameObject.Find("/Enemies/MiniBoss Spawnpoint").gameObject;

        if (ES3.KeyExists("Boss 1"))
        {
            Boss_1 = ES3.Load<List<GameObject>>("Boss 1");
        }

        //Final Boss
        ArrayFinalBoss = GameObject.FindGameObjectsWithTag("Enemy");

        if (Character_Selected != 1)
        {
            FinalBoss_SpawnPoint = GameObject.Find("/Enemies/FinalBoss Spawnpoint").gameObject;

            if (ES3.KeyExists("Boss 2"))
            {
                Boss_2 = ES3.Load<List<GameObject>>("Boss 2");
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Enemy_1());

        StartCoroutine(Enemy_2());

        StartCoroutine(Mini_Boss());

        if (Character_Selected != 1)
        {
            StartCoroutine(Final_Boss_Enemy());
        }

        StartCoroutine(RemoveList());


        Quest_Title = GameObject.Find("/Canvas/QuestLog/Quest Title").GetComponent<TextMeshProUGUI>();

        Quest_Description = GameObject.Find("/Canvas/QuestLog/Quest Description").GetComponent<TextMeshProUGUI>();

        Wall1 = GameObject.Find("/FirstWall");

        Wall2 = GameObject.Find("/SecondWall");

        Wall3 = GameObject.Find("/ThirdWall");

        GameOver = GameObject.Find("/Canvas/GameOver").gameObject;

        Time_limit_text = GameObject.Find("/Canvas/Time Limit").GetComponent<Text>();

        GameOver.SetActive(false);

        if (Character_Selected == 0)
        {
            quest = Resources.Load<Quest>("Quest/Chemist/Clinical Trials");
        }

        else if (Character_Selected == 1)
        {
            quest = Resources.Load<Quest>("Quest/Paramedic/Clinical Trials");
        }

        else if (Character_Selected == 2)
        {
            quest = Resources.Load<Quest>("Quest/Nurse/Clinical Trials");
        }
    }

    IEnumerator Quest()
    {
        yield return new WaitForSeconds(0.3f);

        EnableQuest = true;
    }
    IEnumerator RemoveList()
    {
        yield return new WaitForSeconds(0.2f);

        MiniMonster = new List<GameObject>(0);

        MiniMonster_1 = new List<GameObject>(0);

        Boss_1 = new List<GameObject>(0);

        Boss_2 = new List<GameObject>(0);

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("MiniGolem"))
        {
            MiniMonster.Add(go);

        }

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("MiniGolem_1"))
        {
            MiniMonster_1.Add(go);

        }

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy_1"))
        {
            Boss_1.Add(go);

        }

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Boss_2.Add(go);

        }

        StartCoroutine(Quest());
    }
    IEnumerator Enemy_1()
    {

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("MiniGolem"))
        {
            Destroy(go);
        }

        //Wait For 0.1 Seconds
        yield return new WaitForSeconds(0.1f);

        //Spawn The Enemies
        if (ES3.KeyExists("Monster 1"))
        {
            //MiniMonsterSaved = ES3.Load<List<GameObject>>("Monster 1");

            for (int i = 0; i < MiniMonster.Count; i++)
            {
                GameObject SpawnMonster1 = Instantiate(Monster1, MiniMonsterSpawnPoint.transform.position, Quaternion.identity);
                SpawnMonster1.gameObject.name = "Mini Monster";
                SpawnMonster1.tag = "MiniGolem";
            }

        }
        else
        {
            for (int i = 0; i < ArrayMiniMonster.Length; i++)
            {

                GameObject SpawnMonster1 = Instantiate(Monster1, MiniMonsterSpawnPoint.transform.position, Quaternion.identity);
                SpawnMonster1.gameObject.name = "Mini Monster";
                MiniMonster.Add(SpawnMonster1);
                SpawnMonster1.tag = "MiniGolem";
            }
        }
    }

    IEnumerator Enemy_2()
    {

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("MiniGolem_1"))
        {
            Destroy(go);
        }

        yield return new WaitForSeconds(0.1f);

        //Spawn The Enemies
        if (ES3.KeyExists("Monster 2"))
        {
            //MiniMonsterSaved = ES3.Load<List<GameObject>>("Monster 1");

            for (int i = 0; i < MiniMonster_1.Count; i++)
            {
                GameObject SpawnMonster2 = Instantiate(Monster2, MiniMonster_1_SpawnPoint.transform.position, Quaternion.identity);
                SpawnMonster2.gameObject.name = "Mini Monster (1)";
                SpawnMonster2.tag = "MiniGolem_1";
            }
        }
        else
        {
            for (int i = 0; i < ArrayMiniMonster_1.Length; i++)
            {
                GameObject SpawnMonster2 = Instantiate(Monster1, MiniMonster_1_SpawnPoint.transform.position, Quaternion.identity);
                SpawnMonster2.gameObject.name = "Mini Monster (1)";
                MiniMonster_1.Add(SpawnMonster2);
                SpawnMonster2.tag = "MiniGolem_1";
            }
        }
    }

    IEnumerator Mini_Boss()
    {

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy_1"))
        {
            Destroy(go);
        }

        yield return new WaitForSeconds(0.1f);

        //Spawn The Enemies
        if (ES3.KeyExists("Boss 1"))
        {
            //MiniMonsterSaved = ES3.Load<List<GameObject>>("Monster 1");

            if (Boss_1.Count != 0)
            {
                for (int i = 0; i < Boss_1.Count; i++)
                {
                    GameObject MiniBoss_1 = Instantiate(go_Boss_1, MiniBoss_SpawnPoint.transform.position, Quaternion.identity);
                    MiniBoss_1.gameObject.name = "MonsterIdle";
                    MiniBoss_1.tag = "Enemy_1";
                }
            }

        }
        else
        {
            for (int i = 0; i < ArrayMiniBoss.Length; i++)
            {

                GameObject MiniBoss_1 = Instantiate(go_Boss_1, MiniBoss_SpawnPoint.transform.position, Quaternion.identity);
                MiniBoss_1.gameObject.name = "MonsterIdle";
                MiniBoss_1.tag = "Enemy_1";
                Boss_1.Add(MiniBoss_1);
            }
        }
    }

    IEnumerator Final_Boss_Enemy()
    {

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(go);
        }

        yield return new WaitForSeconds(0.1f);

        //Spawn The Enemies
        if (ES3.KeyExists("Boss 2"))
        {
            //MiniMonsterSaved = ES3.Load<List<GameObject>>("Monster 1");

            if (Boss_2.Count != 0)
            {
                for (int i = 0; i < Boss_2.Count; i++)
                {
                    GameObject FinalBoss = Instantiate(go_Boss_2, FinalBoss_SpawnPoint.transform.position, Quaternion.identity);
                    FinalBoss.gameObject.name = "Final Boss";
                    FinalBoss.tag = "Enemy";
                }
            }

        }
        else
        {
            for (int i = 0; i < ArrayFinalBoss.Length; i++)
            {

                GameObject FinalBoss = Instantiate(go_Boss_2, FinalBoss_SpawnPoint.transform.position, Quaternion.identity);
                FinalBoss.gameObject.name = "Final Boss";
                FinalBoss.tag = "Enemy";
                Boss_2.Add(FinalBoss);

            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        Timer -= 1 * Time.deltaTime;

        Time_limit_text.text = "Time Limit: " + Timer.ToString("0");

        if (EnableQuest == true)
        {

            if (MiniMonster.Count <= 0)
            {
                if (isFirstQuestDone == false)
                {
                    isFirstQuestDone = true;

                    Timer = 60;

                    Wall1.SetActive(false);
                }
            }

            if (MiniMonster_1.Count <= 0)
            {
                if (isSecondQuestDone == false)
                {
                    isSecondQuestDone = true;

                    Timer = 60;

                    Wall2.SetActive(false);

                }

                if (isThirdQuestDone == false)
                {
                    isThirdQuestDone = true;

                    Timer = 60;

                    Wall3.SetActive(false);
                }
            }

            if (Boss_1.Count <= 0)
            {
                if (isFourthQuestDone == false)
                {
                    if (Character_Selected == 1)
                    {
                        StartCoroutine(Outro());
                    }

                    Timer = 60;
                    isFourthQuestDone = true;
                }
            }

            if (Character_Selected != 1)
            {

                if (Boss_2.Count <= 0)
                {
                    if (isFifthQuestDone == false)
                    {
                        Timer = 60;
                        StartCoroutine(Outro());

                        isFifthQuestDone = true;
                    }
                }
            }

            if (Character_Selected == 0)
            {
                if (quest.name == "Clinical Trials")
                {
                    if (Timer <= 0.0f)
                    {
                        if (GameOver.activeSelf == false)
                        {
                            GameOver.SetActive(true);
                            Time.timeScale = 0.0f;
                        }
                    }

                    if (isFirstQuestDone == false)
                    {
                        Quest_Title.text = quest.name;

                        Quest_Description.text = quest.Quest_Description;
                    }
                    else
                    {
                        quest = Resources.Load<Quest>("Quest/Chemist/Againts the Odds");

                        Wall1.SetActive(false);
                    }
                }

                else if (quest.name == "Againts the Odds")
                {
                    if (Timer <= 0.0f)
                    {
                        if (GameOver.activeSelf == false)
                        {
                            GameOver.SetActive(true);
                            Time.timeScale = 0.0f;
                        }
                    }

                    if (isSecondQuestDone == false)
                    {
                        Quest_Title.text = quest.name;

                        Quest_Description.text = quest.Quest_Description;
                    }
                    else
                    {
                        quest = Resources.Load<Quest>("Quest/Chemist/The Bosss Boss");

                        Wall2.SetActive(false);
                    }
                }

                else if (quest.name == "The Bosss Boss")
                {

                    if (Timer <= 0.0f)
                    {
                        if (GameOver.activeSelf == false)
                        {
                            GameOver.SetActive(true);
                            Time.timeScale = 0.0f;
                        }
                    }

                    if (isThirdQuestDone == false)
                    {
                        Quest_Title.text = quest.name;

                        Quest_Description.text = quest.Quest_Description;
                    }
                    else
                    {
                        quest = Resources.Load<Quest>("Quest/Chemist/Clovers Will");

                        Wall3.SetActive(false);
                    }

                }


                else if (quest.name == "Clovers Will")
                {
                    if (Timer <= 0.0f)
                    {
                        if (GameOver.activeSelf == false)
                        {
                            GameOver.SetActive(true);
                            Time.timeScale = 0.0f;
                        }
                    }

                    if (isFourthQuestDone == false)
                    {
                        Quest_Title.text = quest.name;

                        Quest_Description.text = quest.Quest_Description;
                    }

                }
            }

            else if (Character_Selected == 1)
            {
                if (quest.name == "Clinical Trials")
                {
                    if (Timer <= 0.0f)
                    {
                        if (GameOver.activeSelf == false)
                        {
                            GameOver.SetActive(true);
                            Time.timeScale = 0.0f;
                        }
                    }

                    if (isFirstQuestDone == false)
                    {
                        Quest_Title.text = quest.name;

                        Quest_Description.text = quest.Quest_Description;
                    }
                    else
                    {
                        quest = Resources.Load<Quest>("Quest/Paramedic/Againts the Odds");

                        Wall1.SetActive(false);
                    }
                }

                else if (quest.name == "Againts the Odds")
                {
                    if (Timer <= 0.0f)
                    {
                        if (GameOver.activeSelf == false)
                        {
                            GameOver.SetActive(true);
                            Time.timeScale = 0.0f;
                        }
                    }

                    if (isSecondQuestDone == false)
                    {
                        Quest_Title.text = quest.name;

                        Quest_Description.text = quest.Quest_Description;
                    }
                    else
                    {
                        quest = Resources.Load<Quest>("Quest/Paramedic/The Bosss Boss");

                        Wall2.SetActive(false);
                    }
                }

                else if (quest.name == "The Bosss Boss")
                {

                    if (Timer <= 0.0f)
                    {
                        if (GameOver.activeSelf == false)
                        {
                            GameOver.SetActive(true);
                            Time.timeScale = 0.0f;
                        }
                    }

                    if (isThirdQuestDone == false)
                    {
                        Quest_Title.text = quest.name;

                        Quest_Description.text = quest.Quest_Description;
                    }
                    else
                    {
                        quest = Resources.Load<Quest>("Quest/Paramedic/Clovers Will");

                        Wall3.SetActive(false);
                    }

                }


                else if (quest.name == "Clovers Will")
                {
                    if (Timer <= 0.0f)
                    {
                        if (GameOver.activeSelf == false)
                        {
                            GameOver.SetActive(true);
                            Time.timeScale = 0.0f;
                        }
                    }

                    if (isFourthQuestDone == false)
                    {
                        Quest_Title.text = quest.name;

                        Quest_Description.text = quest.Quest_Description;
                    }

                }
            }

            else if (Character_Selected == 2)
            {
                if (quest.name == "Clinical Trials")
                {
                    if (Timer <= 0.0f)
                    {
                        if (GameOver.activeSelf == false)
                        {
                            GameOver.SetActive(true);
                            Time.timeScale = 0.0f;
                        }
                    }

                    if (isFirstQuestDone == false)
                    {
                        Quest_Title.text = quest.name;

                        Quest_Description.text = quest.Quest_Description;
                    }
                    else
                    {
                        quest = Resources.Load<Quest>("Quest/Nurse/Againts the Odds");

                        Wall1.SetActive(false);
                    }
                }

                else if (quest.name == "Againts the Odds")
                {
                    if (Timer <= 0.0f)
                    {
                        if (GameOver.activeSelf == false)
                        {
                            GameOver.SetActive(true);
                            Time.timeScale = 0.0f;
                        }
                    }

                    if (isSecondQuestDone == false)
                    {
                        Quest_Title.text = quest.name;

                        Quest_Description.text = quest.Quest_Description;
                    }
                    else
                    {
                        quest = Resources.Load<Quest>("Quest/Nurse/The Bosss Boss");

                        Wall2.SetActive(false);
                    }
                }

                else if (quest.name == "The Bosss Boss")
                {

                    if (Timer <= 0.0f)
                    {
                        if (GameOver.activeSelf == false)
                        {
                            GameOver.SetActive(true);
                            Time.timeScale = 0.0f;
                        }
                    }

                    if (isThirdQuestDone == false)
                    {
                        Quest_Title.text = quest.name;

                        Quest_Description.text = quest.Quest_Description;
                    }
                    else
                    {
                        quest = Resources.Load<Quest>("Quest/Nurse/Clovers Will");

                        Wall3.SetActive(false);
                    }

                }


                else if (quest.name == "Clovers Will")
                {
                    if (Timer <= 0.0f)
                    {
                        if (GameOver.activeSelf == false)
                        {
                            GameOver.SetActive(true);
                            Time.timeScale = 0.0f;
                        }
                    }

                    if (isFourthQuestDone == false)
                    {
                        Quest_Title.text = quest.name;

                        Quest_Description.text = quest.Quest_Description;
                    }

                }
            }
        }


        IEnumerator Outro()
        {
            yield return new WaitForSeconds(5.0f);

            SceneManager.LoadScene("Outro");
        }

    }
}
