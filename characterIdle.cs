using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;
using UnityEngine.SceneManagement;
public class characterIdle : MonoBehaviour
{
    [SerializeField] AnimancerComponent animancer;
    [SerializeField] AnimationClip Idle;

    GameObject Player;

    int Selected_Character;

    [SerializeField] float rotation;
    Scene scene;
    void Start()
    {
        animancer = this.gameObject.GetComponent<AnimancerComponent>();
        animancer.Layers[0].Play(Idle, 0.25f, FadeMode.FixedSpeed);

        scene = SceneManager.GetActiveScene();

        if (scene.name == "characterAnimation_Chemist" || scene.name == "characterAnimation_Nurse" || scene.name == "characterAnimation_Paramedic")
        {
            Selected_Character = ES3.Load<int>("Character Selected");
            
            if (Selected_Character == 0)
            {

                Player = GameObject.Find("/chemist").gameObject;

            }

            else if (Selected_Character == 1)
            {


                Player = GameObject.Find("/paramedic").gameObject;

            }

            else if (Selected_Character == 2)
            {

                Player = GameObject.Find("/nurse").gameObject;

            }
        }
    }

    private void Update()
    {
        if (scene.name == "characterAnimation_Chemist")
        {

            Vector3 GetPatrol = Player.transform.position - this.transform.position;

            Quaternion RotateTo = Quaternion.LookRotation(GetPatrol);

            Quaternion LookAt = Quaternion.RotateTowards(this.transform.rotation, RotateTo, rotation * Time.deltaTime);

            this.transform.rotation = LookAt;
        }

        else if (scene.name == "characterAnimation_Paramedic")
        {

            Vector3 GetPatrol = Player.transform.position - this.transform.position;

            Quaternion RotateTo = Quaternion.LookRotation(GetPatrol);

            Quaternion LookAt = Quaternion.RotateTowards(this.transform.rotation, RotateTo, rotation * Time.deltaTime);

            this.transform.rotation = LookAt;
        }

        else if (scene.name == "characterAnimation_Nurse")
        {

            Vector3 GetPatrol = Player.transform.position - this.transform.position;

            Quaternion RotateTo = Quaternion.LookRotation(GetPatrol);

            Quaternion LookAt = Quaternion.RotateTowards(this.transform.rotation, RotateTo, rotation * Time.deltaTime);

            this.transform.rotation = LookAt;
        }
    }

}
