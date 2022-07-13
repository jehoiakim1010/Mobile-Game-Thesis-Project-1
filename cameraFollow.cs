using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    GameObject Player;

    [SerializeField] float y;
    [SerializeField] float x;

    int Character_Selected;

    public Transform Target;

    [SerializeField] float RotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        Character_Selected = ES3.Load<int>("Character Selected");
    }

    // Update is called once per frame
    void Update()
    {
        if (Player != null)
        {
        }
        else
        {
            if (Character_Selected == 0)
            {
                Player = GameObject.Find("/chemist").gameObject;

                Target = Player.transform.Find("Target").gameObject.transform;
            }

            else if (Character_Selected == 1)
            {
                Player = GameObject.Find("/paramedic").gameObject;

                Target = Player.transform.Find("Target").gameObject.transform;
            }

            else if (Character_Selected == 2)
            {
                Player = GameObject.Find("/nurse").gameObject;

                Target = Player.transform.Find("Target").gameObject.transform;
            }
        }
    }

}
