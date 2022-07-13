using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drop : MonoBehaviour
{
    PlayerStatus playerstatus;
    [SerializeField] GameObject item;
    GameObject player;
    GameObject itemDrop;

    int Selected_Character;

    GameObject Chemist, Parademic, Nurse;
    private void Start()
    {
        Selected_Character = ES3.Load<int>("Character Selected");

        if (Selected_Character == 0)
        {
            Chemist = GameObject.Find("/chemist").gameObject;

            playerstatus = Chemist.GetComponent<PlayerStatus>();

            itemDrop = Chemist.gameObject.transform.Find("itemDrop").gameObject;

        }

        else if (Selected_Character == 1)
        {
            Parademic = GameObject.Find("/paramedic").gameObject;

            playerstatus = Parademic.GetComponent<PlayerStatus>();

            itemDrop = Parademic.gameObject.transform.Find("itemDrop").gameObject;
        }

        else if (Selected_Character == 2)
        {
            Nurse = GameObject.Find("/nurse").gameObject;

            playerstatus = Nurse.GetComponent<PlayerStatus>();

            itemDrop = Nurse.gameObject.transform.Find("itemDrop").gameObject;
        }
    }
    public void SpawnItem()
    {


        if (Selected_Character == 0)
        {
            Destroy(this.gameObject);
            GameObject SpawnedItem = Instantiate(item, itemDrop.transform.position, Quaternion.identity);

        }

        else if (Selected_Character == 1)
        {
            Destroy(this.gameObject);
            GameObject SpawnedItem = Instantiate(item, itemDrop.transform.position, Quaternion.identity);
        }

        else if (Selected_Character == 2)
        {
            Destroy(this.gameObject);
            GameObject SpawnedItem = Instantiate(item, itemDrop.transform.position, Quaternion.identity);
        }

    }
}
    /*
    public void heal()
    {
        playerstatus.pHealth(50f);

        Destroy(this.gameObject);
    }
*/

/*
    public void mana()
    {
        playerstatus.AddMana(50f);

        Destroy(this.gameObject);
    }
}
*/