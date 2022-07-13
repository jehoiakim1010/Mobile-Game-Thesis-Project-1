using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    ObjectiveManager objectiveManager;
    float maxH = 0;
    [SerializeField] float currentH = 0;
    public GameObject itemDrop;
    GameObject Chemist, Parademic, Nurse;

    PlayerStatus playerstatus;

    Slider EnemyHealthSlider;

    int Selected_Character;

    GameObject EnemyCanvas;

    public bool EnemyStunned;
    // Start is called before the first frame update
    private void Awake()
    {
        objectiveManager = GameObject.FindObjectOfType<ObjectiveManager>();

        currentH = maxH;
    }
    void Start()
    {

        EnemyCanvas = this.gameObject.transform.Find("Canvas").gameObject;

        EnemyHealthSlider = EnemyCanvas.transform.Find("Slider").GetComponent<Slider>();

        EnemyHealthSlider.value = currentH;

        Selected_Character = ES3.Load<int>("Character Selected");


        if (Selected_Character == 0)
        {

            playerstatus = GameObject.Find("/chemist").GetComponent<PlayerStatus>();

        }

        else if (Selected_Character == 1)
        {

            playerstatus = GameObject.Find("/paramedic").GetComponent<PlayerStatus>();
        }

        else if (Selected_Character == 2)
        {

            playerstatus =  GameObject.Find("/nurse").GetComponent<PlayerStatus>();

        }

    }

    private void LateUpdate()
    {
        EnemyCanvas.transform.LookAt(Camera.main.transform.position);
    }
    // Update is called once per frame

    void Update()
    {
        if (this.currentH >= 100f)
        {
            if (this.gameObject.name == "Mini Monster")
            {
                objectiveManager.MiniMonster.Remove(this.gameObject);
                
            }

            if (this.gameObject.name == "Mini Monster (1)")
            {
                objectiveManager.MiniMonster_1.Remove(this.gameObject);
            }

            if (this.gameObject.name == "MonsterIdle")
            {
                objectiveManager.Boss_1.Remove(this.gameObject);
            }

            if (this.gameObject.name == "Final Boss")
            {
                objectiveManager.Boss_2.Remove(this.gameObject);
            }

            Instantiate(itemDrop, this.transform.position, Quaternion.identity);

            playerstatus.pEXP(20f);

            Destroy(gameObject);
        }
    }



    public void TakeDamage(float amount)
    {
        if (this.currentH <= 100f)
        {
            this.currentH += amount;

            this.EnemyHealthSlider.value = currentH;
        }
    }
}