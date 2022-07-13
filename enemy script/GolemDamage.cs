using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GolemDamage : MonoBehaviour
{
    float golemdamage = 20;
    PlayerStatus playerstatus;

    int Selected_Character;

    // Start is called before the first frame update

    void Start()
    {
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

            playerstatus = GameObject.Find("/nurse").GetComponent<PlayerStatus>();

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeSelf == true)
        {
            StartCoroutine(EnemyShowDamage());
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            
            StartCoroutine(EnemyShowDamage());

           // playerstatus.playerTakeDamage(golemdamage);

        }
    }
    IEnumerator EnemyShowDamage()
    {

        yield return new WaitForSeconds(0.2f);

        this.gameObject.SetActive(false);
    }
}
