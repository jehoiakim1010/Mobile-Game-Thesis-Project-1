using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;

public class GolemMovement : MonoBehaviour
{
    AnimancerComponent animancer;

    [SerializeField] AnimationClip enemyAttack1, enemyAttack2, Walk, Idle;

    CharacterController enemyController;


    //attack
    bool isAttack;
    float CooldownTimer_Sword_Attack;
    [SerializeField] float Cooldown_Sword_Attack;
    float Cooldown_Reduction_Sword_Attack = 1f;
    bool isAttackDone;
    EnemyMovement enemyMove;

    EnemyHealth enemyHealth;
    [SerializeField] float minDistance;
    [SerializeField] float speed;

    float CooldownTimer_Sword_Attack_1;

    [SerializeField] float Cooldown_Sword_Attack_1;

    float Cooldown_Reduction_Sword_Attack_1 = 1f;

    bool isAttack_1;

    bool isAttackDone_1;

    GameObject player;

    GameObject attack;

    bool isPlayerNear;
    LayerMask playerLayer;

    // public Animator anim;

    int Selected_Character;

    GameObject Chemist, Parademic, Nurse;

    bool runonce;

    private void Awake()
    {
        Selected_Character = ES3.Load<int>("Character Selected");

        enemyHealth = this.gameObject.GetComponent<EnemyHealth>();
    }
    // Start is called before the first frame update
    void Start()
    {
        animancer = this.gameObject.GetComponent<AnimancerComponent>();

        enemyController = GetComponent<CharacterController>();
        enemyMove = this.gameObject.GetComponent<EnemyMovement>();

        if (Selected_Character == 0)
        {
            player = GameObject.Find("/chemist").gameObject;
        }

        else if (Selected_Character == 1)
        {
            player = GameObject.Find("/paramedic").gameObject;
        }

        else if (Selected_Character == 2)
        {
            player = GameObject.Find("/nurse").gameObject;
        }

        attack = gameObject.transform.Find("GolemAttack").gameObject;
        playerLayer = LayerMask.GetMask("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (runonce == false)
        {
            if (Selected_Character == 0)
            {
                player = GameObject.Find("/chemist").gameObject;
            }

            else if (Selected_Character == 1)
            {
                player = GameObject.Find("/paramedic").gameObject;
            }

            else if (Selected_Character == 2)
            {
                player = GameObject.Find("/nurse").gameObject;
            }

            runonce = true;
        }
        if (enemyHealth.EnemyStunned == false)
        {
            GolemMove();
        }

        //  if(distance > 1.2f)
        //   {

        //  }
    }

    void GolemMove()
    {
        if (this.gameObject.name == "Final Boss")
        {
            if (enemyMove.PlayerisNear == true)
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
                        CooldownTimer_Sword_Attack -= Cooldown_Reduction_Sword_Attack * Time.deltaTime;
                        isAttackDone = false;
                    }

                }

                //Attack Cooldown 2
                if (CooldownTimer_Sword_Attack_1 <= 0.0f && isAttack_1 == false)
                {
                    CooldownTimer_Sword_Attack_1 = Cooldown_Sword_Attack_1;
                    isAttack_1 = true;

                }
                else
                {
                    if (isAttack_1 == false)
                    {
                        CooldownTimer_Sword_Attack_1 -= Cooldown_Reduction_Sword_Attack_1 * Time.deltaTime;
                        isAttackDone_1 = false;
                    }

                }

                this.isPlayerNear = Physics.CheckSphere(this.transform.position, minDistance, playerLayer);

                if (this.isAttack_1 && this.isPlayerNear)
                {
                    this.transform.LookAt(player.transform.position);

                    if (this.isAttackDone_1 == false)
                    {

                        var state = animancer.Layers[0].Play(enemyAttack2, 0.25f, FadeMode.FixedSpeed);

                        state.Speed = 1.0f;

                        state.Events.OnEnd = () =>
                        {
                            GameObject GolemAttack = GolemAttackPool.Pooling_Instance.GamePooled();
                            GolemAttack.SetActive(true);
                            GolemAttack.transform.position = this.attack.transform.position;

                            this.isAttackDone_1 = true;
                            this.isAttack_1 = false;
                        };
                    }

                    else
                    {
                        this.transform.LookAt(player.transform.position);

                        //enemyController.Move(transform.TransformDirection(Vector3.forward) * speed * Time.deltaTime);
                        this.transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);

                        var state = animancer.Layers[0].Play(Walk, 0.25f, FadeMode.FixedSpeed);
                        state.Speed = 2.0f;
                    }
                }

                else
                {
                    if (this.isAttack && this.isPlayerNear)
                    {

                        this.transform.LookAt(player.transform.position);

                        if (this.isAttackDone == false)
                        {

                            var state = animancer.Layers[0].Play(enemyAttack1, 0.25f, FadeMode.FixedSpeed);

                            state.Speed = 1.0f;

                            state.Events.OnEnd = () =>
                            {
                                GameObject GolemAttack = GolemAttackPool.Pooling_Instance.GamePooled();
                                GolemAttack.SetActive(true);
                                GolemAttack.transform.position = this.attack.transform.position;

                                this.isAttackDone = true;
                                this.isAttack = false;
                            };
                        }


                        else
                        {

                            this.transform.LookAt(player.transform.position);

                            //enemyController.Move(transform.TransformDirection(Vector3.forward) * speed * Time.deltaTime);
                            this.transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);

                            var state = animancer.Layers[0].Play(Walk, 0.25f, FadeMode.FixedSpeed);
                            state.Speed = 2.0f;
                        }
                    }
                    else
                    {
                        if (isPlayerNear == false)
                        {
                            this.transform.LookAt(player.transform.position);

                            //enemyController.Move(transform.TransformDirection(Vector3.forward) * speed * Time.deltaTime);
                            this.transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);

                            var state = animancer.Layers[0].Play(Walk, 0.25f, FadeMode.FixedSpeed);
                            state.Speed = 2.0f;
                        }
                        else
                        {
                            this.transform.LookAt(player.transform.position);
                            var state = animancer.Layers[0].Play(Idle, 0.25f, FadeMode.FixedSpeed);
                            state.Speed = 2.0f;
                        }
                    }

                }
            }
        }
        else
        {
            if (enemyMove.PlayerisNear == true)
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
                        CooldownTimer_Sword_Attack -= Cooldown_Reduction_Sword_Attack * Time.deltaTime;
                        isAttackDone = false;
                    }

                }

                this.isPlayerNear = Physics.CheckSphere(this.transform.position, minDistance, playerLayer);

                if (this.isAttack && this.isPlayerNear)
                {

                    this.transform.LookAt(player.transform.position);

                    if (this.isAttackDone == false)
                    {

                        var state = animancer.Layers[0].Play(enemyAttack1, 0.25f, FadeMode.FixedSpeed);

                        state.Speed = 1.0f;

                        state.Events.OnEnd = () =>
                        {
                            GameObject GolemAttack = GolemAttackPool.Pooling_Instance.GamePooled();
                            GolemAttack.SetActive(true);
                            GolemAttack.transform.position = this.attack.transform.position;

                            this.isAttackDone = true;
                            this.isAttack = false;
                        };
                    }


                    else
                    {
                        this.transform.LookAt(player.transform.position);

                        //enemyController.Move(transform.TransformDirection(Vector3.forward) * speed * Time.deltaTime);
                        this.transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);

                        var state = animancer.Layers[0].Play(Walk, 0.25f, FadeMode.FixedSpeed);
                        state.Speed = 2.0f;
                    }
                }
                else
                {
                    if (isPlayerNear == false)
                    {
                        transform.LookAt(player.transform.position);

                        //enemyController.Move(transform.TransformDirection(Vector3.forward) * speed * Time.deltaTime);
                        this.transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);

                        var state = animancer.Layers[0].Play(Walk, 0.25f, FadeMode.FixedSpeed);
                        state.Speed = 2.0f;
                    }
                    else
                    {
                        this.transform.LookAt(player.transform.position);
                        
                        var state = animancer.Layers[0].Play(Idle, 0.25f, FadeMode.FixedSpeed);
                        state.Speed = 2.0f;
                    }
                }

            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.transform.position, minDistance);
    }
}
