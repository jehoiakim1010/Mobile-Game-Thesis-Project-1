using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    PlayerStatus playerstatus;
    public float damage;
    CharacterMovement charactermovement;


    int Selected_Character;

    int Level;

    float CooldownTimer_Sword_Attack;

    float Cooldown_Sword_Attack = 1f;

    bool isAttack;

    bool isStunned;
    Rigidbody rb;

    int Player_Level;

    bool runonce;
    private void Awake()
    {
        Selected_Character = ES3.Load<int>("Character Selected");

        if (ES3.KeyExists("Player Level"))
        {
            Player_Level = ES3.Load<int>("Player Level");
        }

        if (Selected_Character == 0)
        {

            charactermovement = GameObject.Find("/chemist").GetComponent<CharacterMovement>();

            rb = this.gameObject.GetComponent<Rigidbody>();

            playerstatus = GameObject.Find("/chemist").GetComponent<PlayerStatus>();


        }

        else if (Selected_Character == 1)
        {


            charactermovement = GameObject.Find("/paramedic").GetComponent<CharacterMovement>();

            playerstatus = GameObject.Find("/paramedic").GetComponent<PlayerStatus>();


        }

        else if (Selected_Character == 2)
        {

            charactermovement = GameObject.Find("/nurse").GetComponent<CharacterMovement>();

            playerstatus = GameObject.Find("/nurse").GetComponent<PlayerStatus>();

        }
    }

    // Start is called before the first frame update

    public void OnEnable()
    {
        if (this.gameObject.name == "Player Damage Combo(Clone)")
        {
            if (playerstatus.Level == 0)
            {
                if (gameObject.GetComponent<PlayerDamage>().damage != 23f)
                {
                    gameObject.GetComponent<PlayerDamage>().damage = 23f;
                }
            }

            if (playerstatus.Level == 1)
            {
                if (gameObject.GetComponent<PlayerDamage>().damage != 25f)
                {
                    gameObject.GetComponent<PlayerDamage>().damage = 25f;
                }
            }

            if (playerstatus.Level == 2)
            {
                if (gameObject.GetComponent<PlayerDamage>().damage != 25f)
                {
                    gameObject.GetComponent<PlayerDamage>().damage = 25f;
                }
            }

            if (playerstatus.Level == 3)
            {
                if (gameObject.GetComponent<PlayerDamage>().damage != 25f)
                {
                    gameObject.GetComponent<PlayerDamage>().damage = 25f;
                }
            }

            if (playerstatus.Level == 4)
            {
                if (gameObject.GetComponent<PlayerDamage>().damage != 25f)
                {
                    gameObject.GetComponent<PlayerDamage>().damage = 25f;
                }
            }

            if (playerstatus.Level == 5)
            {
                if (gameObject.GetComponent<PlayerDamage>().damage != 25f)
                {
                    gameObject.GetComponent<PlayerDamage>().damage = 25f;
                }
            }
        }

        else
        {
            if (playerstatus.Level == 0)
            {
                if (gameObject.GetComponent<PlayerDamage>().damage != 19f)
                {
                    gameObject.GetComponent<PlayerDamage>().damage = 19f;
                }
            }

            if (playerstatus.Level == 1)
            {
                if (gameObject.GetComponent<PlayerDamage>().damage != 21f)
                {
                    gameObject.GetComponent<PlayerDamage>().damage = 21f;
                }
            }

            if (playerstatus.Level == 2)
            {
                if (gameObject.GetComponent<PlayerDamage>().damage != 23f)
                {
                    gameObject.GetComponent<PlayerDamage>().damage = 23f;
                }
            }

            if (playerstatus.Level == 3)
            {
                if (gameObject.GetComponent<PlayerDamage>().damage != 25f)
                {
                    gameObject.GetComponent<PlayerDamage>().damage = 25f;
                }
            }

            if (playerstatus.Level == 4)
            {
                if (gameObject.GetComponent<PlayerDamage>().damage != 25f)
                {
                    gameObject.GetComponent<PlayerDamage>().damage = 25f;
                }
            }

            if (playerstatus.Level == 5)
            {
                if (gameObject.GetComponent<PlayerDamage>().damage != 25f)
                {
                    gameObject.GetComponent<PlayerDamage>().damage = 25f;
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.name == "Player Damage Combo(Clone)")
        {
            if (this.gameObject.activeSelf == true && runonce == false)
            {
                StartCoroutine(RemoveWhenNotCollided());

                runonce = true;
            }
        }
        else
        {
            if (Selected_Character == 0)
            {
                if (this.gameObject.name == "Throwing Bottle(Clone)")
                {
                    this.transform.rotation = Quaternion.LookRotation(rb.velocity);
                }
            }

            else
            {
                //Attack Cooldown
                if (CooldownTimer_Sword_Attack <= 0.0f && isAttack == false)
                {
                    CooldownTimer_Sword_Attack = Cooldown_Sword_Attack;
                    isAttack = true;
                }
                else
                {
                    if (isAttack == false)
                    {
                        CooldownTimer_Sword_Attack -= 1 * Time.deltaTime;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Enemy_1" || other.gameObject.tag == "MiniGolem" || other.gameObject.tag == "MiniGolem_1")
        {

            if (Selected_Character == 0)
            {
                if (other.gameObject.GetComponent<EnemyHealth>() != null)
                {

                    int Stun = Random.Range(0, 2);

                    switch (Stun)
                    {
                        case 0:
                            break;

                        case 1:
                            isStunned = true;
                            break;

                        default:
                            break;
                    }

                    if (isStunned)
                    {
                        other.gameObject.GetComponent<EnemyHealth>().EnemyStunned = true;

                        Debug.Log("Stunned");
                        isStunned = false;
                    }

                    other.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);

                    other.gameObject.GetComponent<EnemyMovement>().MaxDistance += 20f;

                    isAttack = false;

                    this.gameObject.SetActive(false);

                }
            }


            else
            {
                if (this.gameObject.name == "Player Damage Combo(Clone)")
                {
                    if (other.gameObject.GetComponent<EnemyHealth>() != null)
                    {

                        int Stun = Random.Range(0, 2);

                        switch (Stun)
                        {
                            case 0:
                                break;

                            case 1:
                                isStunned = true;
                                break;

                            default:
                                break;
                        }

                        if (isStunned)
                        {
                            other.gameObject.GetComponent<EnemyHealth>().EnemyStunned = true;

                            isStunned = false;
                        }

                        other.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);

                        this.gameObject.SetActive(false);
                    }
                }
                else
                {
                    if (charactermovement.NumberAttack >= 1 && isAttack == true)
                    {
                        if (other.gameObject.GetComponent<EnemyHealth>() != null)
                        {

                            int Stun = Random.Range(0, 2);

                            switch (Stun)
                            {
                                case 0:
                                    break;

                                case 1:
                                    isStunned = true;
                                    break;

                                default:
                                    break;
                            }

                            if (isStunned)
                            {
                                other.gameObject.GetComponent<EnemyHealth>().EnemyStunned = true;

                                isStunned = false;
                            }

                            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);

                            isAttack = false;

                            //this.gameObject.SetActive(false);

                        }
                    }
                }
            }
        }

        if (other.gameObject.tag == "Ground")
        {
            this.gameObject.SetActive(false);
        }
    }

    IEnumerator RemoveWhenNotCollided()
    {
        yield return new WaitForSeconds(0.2f);
        runonce = false;
        this.gameObject.SetActive(false);
    }
}
