using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slot : MonoBehaviour
{
    public void DropItem()
    {
        if (this.transform.childCount > 1)
        {
            for (int i = 0; i < this.gameObject.transform.childCount; i++)
            {
                if (this.gameObject.transform.GetChild(i).GetComponent<drop>() != null)
                {
                    this.gameObject.transform.GetChild(i).GetComponent<drop>().SpawnItem();

                    GameObject.Destroy(this.gameObject.transform.GetChild(i).gameObject);

                    break;
                }
            }
        }
    }
}
