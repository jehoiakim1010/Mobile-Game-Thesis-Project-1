using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoolDamage : MonoBehaviour
{
    public static PlayerPoolDamage Pooling_Instance;

    private List<GameObject> Objects_Pool = new List<GameObject>();

    private int AmountOfPool = 10;

    [SerializeField] GameObject PlayerAttack;
    private void Awake()
    {
        if (Pooling_Instance == null)
        {
            Pooling_Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < AmountOfPool; i++)
        {
            GameObject Obj = Instantiate(PlayerAttack);

            Obj.SetActive(false);

            Obj.transform.SetParent(this.gameObject.transform);

            Objects_Pool.Add(Obj);
        }
    }

    public GameObject GamePooled()
    {
        for (int i = 0; i < Objects_Pool.Count; i++)
        {
            if (!Objects_Pool[i].activeInHierarchy)
            {
                return Objects_Pool[i];
            }
        }

        return null;
    }
}
