using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemDrops : MonoBehaviour
{
    SoundManager soundmanager;
    inventory inven;
    public Items items;
    [SerializeField] GameObject itemButton;
    int Selected_Character;

    GameObject Chemist, Parademic, Nurse;
    // Start is called before the first frame update

    void Start()
    {
        soundmanager = GameObject.FindObjectOfType<SoundManager>() as SoundManager;

        Selected_Character = ES3.Load<int>("Character Selected");

        if (Selected_Character == 0)
        {

            inven = GameObject.Find("/chemist").GetComponent<inventory>();

        }

        else if (Selected_Character == 1)
        {

            inven = GameObject.Find("/paramedic").GetComponent<inventory>();
        }

        else if (Selected_Character == 2)
        {
            inven = GameObject.Find("/nurse").GetComponent<inventory>();
        }

    }
    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            for (int i = 0; i < inven.slots.Length; i++)
            {
                if (inven.isFull[i] == false)
                {
                    soundmanager.PlaySound("ItemCollect");
                    
                    inven.isFull[i] = true;
                    //Instantiate an ItemButton to the new slots
                    GameObject ItemButton = Instantiate(itemButton, inven.slots[i].transform, false);

                    ItemButton.transform.SetSiblingIndex(1);

                    Destroy(this.gameObject);
                    break;
                }
            }
        }

    }
}
