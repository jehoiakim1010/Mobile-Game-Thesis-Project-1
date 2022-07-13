using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory : MonoBehaviour
{
    ButtonManager buttonmanager;
    GameObject Inventory;

    public bool[] isFull;
    public GameObject[] slots;

    public int[] NumberOfItemsInSlot;

    public string[] GameObject_Slots;

    void Start()
    {
        Inventory = GameObject.Find("/Canvas/Inventory").gameObject;

        buttonmanager = GameObject.FindObjectOfType<ButtonManager>() as ButtonManager;

    }

    private void Update()
    {

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].transform.childCount >= 3)
            {
                Destroy(slots[i].transform.GetChild(2).gameObject);
            }
        }


        if (isFull[0] && GameObject_Slots[0] == "")
        {
            GameObject_Slots[0] = slots[0].transform.GetChild(1).gameObject.name.ToString();
        }

        if (isFull[1] && GameObject_Slots[1] == "")
        {
            GameObject_Slots[1] = slots[1].transform.GetChild(1).gameObject.name.ToString();
        }

        if (isFull[2] && GameObject_Slots[2] == "")
        {
            GameObject_Slots[2] = slots[2].transform.GetChild(1).gameObject.name.ToString();
        }

        if (isFull[3] && GameObject_Slots[3] == "")
        {
            GameObject_Slots[3] = slots[3].transform.GetChild(1).gameObject.name.ToString();
        }

        if (isFull[4] && GameObject_Slots[4] == "")
        {
            GameObject_Slots[4] = slots[4].transform.GetChild(1).gameObject.name.ToString();
        }

        if (isFull[5] && GameObject_Slots[5] == "")
        {
            GameObject_Slots[5] = slots[5].transform.GetChild(1).gameObject.name.ToString();
        }

        if (isFull[6] && GameObject_Slots[6] == "")
        {
            GameObject_Slots[6] = slots[6].transform.GetChild(1).gameObject.name.ToString();
        }

        if (isFull[7] && GameObject_Slots[7] == "")
        {
            GameObject_Slots[7] = slots[7].transform.GetChild(1).gameObject.name.ToString();
        }

        if (buttonmanager.isInventory == true)
        {
            Inventory.SetActive(true);
        }
        else
        {
            Inventory.SetActive(false);
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].transform.childCount <= 1)
            {
                this.NumberOfItemsInSlot[i] = 0;
                this.isFull[i] = false;
                this.GameObject_Slots[i] = "";
            }
            else
            {
                for (int a = 0; a < slots[i].transform.childCount; a++)
                {
                    if (slots[i].gameObject.transform.GetChild(a).GetComponent<drop>() != null)
                    {
                        this.NumberOfItemsInSlot[i] = a;
                    }
                }
            }
        }
    }
}
