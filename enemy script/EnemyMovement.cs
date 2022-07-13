using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;

public class EnemyMovement : MonoBehaviour
{
    EnemyHealth enemyhealth;
    AnimancerComponent enemyAnimancer;
    [SerializeField] ClipState.Transition Idle, Walk, Run;
    [SerializeField] float speed;
    Vector3 velocity;
    Vector3 gravVelocity;
    float gravity = -100;
    bool isGround;
    LayerMask layerMask;
    CharacterController enemyControl;

    [HideInInspector] public float Distance;
    [HideInInspector] public float MaxDistance;
    GameObject Player;
    [HideInInspector] public bool PlayerisNear;

    [SerializeField] GameObject[] Spot;
    int RandomSpot;
    [SerializeField] float rotation;
    bool isStopMovement;

    [SerializeField] float MinDistance;
    float WaitTime;
    [SerializeField] float MaxWaitTime;

    bool isPlayerNear;
    LayerMask playerLayer;

    bool RunOnceStunned;

    GameObject StunParticlesObject;
    ParticleSystem Stun_Particles;
    // Start is called before the first frame update
    void Start()
    {
        StunParticlesObject = this.gameObject.transform.Find("Stunned").gameObject;
        Stun_Particles = this.gameObject.transform.Find("Stunned").transform.Find("Stun Particles").GetComponent<ParticleSystem>();
        enemyhealth = this.gameObject.GetComponent<EnemyHealth>();
        enemyAnimancer = this.gameObject.GetComponent<AnimancerComponent>();
        enemyControl = GetComponent<CharacterController>();
        layerMask = LayerMask.GetMask("Ground");


        playerLayer = LayerMask.GetMask("Player");

        Stun_Particles.Stop();
        StunParticlesObject.SetActive(false);
        this.enemyAnimancer.enabled = true;
        this.enemyhealth.EnemyStunned = false;
        RunOnceStunned = false;

        if (this.gameObject.name == "Mini Monster")
        {
            Spot[0] = GameObject.Find("/Enemies/Mini Golem Spot 1").gameObject;

            Spot[1] = GameObject.Find("/Enemies/Mini Golem Spot 2").gameObject;
        }

        if (this.gameObject.name == "Mini Monster (1)")
        {
            Spot[0] = GameObject.Find("/Enemies/Mini Golem Spot 1 (1)").gameObject;

            Spot[1] = GameObject.Find("/Enemies/Mini Golem Spot 1 (2)").gameObject;

            Spot[1] = GameObject.Find("/Enemies/Mini Golem Spot 1 (3)").gameObject;
        }

        if (this.gameObject.name == "MonsterIdle")
        {
            Spot[0] = GameObject.Find("/Enemies/Mini Boss Spot").gameObject;
        }

        if (this.gameObject.name == "Final Boss")
        {
            Spot[0] = GameObject.Find("/Enemies/Final Boss Spot").gameObject;

        }

    }
    private void FixedUpdate()
    {
        isGround = Physics.CheckSphere(this.transform.position, 0.8f, layerMask);

        enemyControl.Move(velocity * Time.deltaTime);

        if (isGround)
        {
            velocity.y = -10f;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyhealth.EnemyStunned == false)
        {
            EnemyMove();
            StunParticlesObject.SetActive(false);
            Stun_Particles.Stop();
        }
        else
        {
            if (RunOnceStunned == false)
            {
                enemyAnimancer.enabled = false;

                StartCoroutine(Stun());
                RunOnceStunned = true;
            }
        }
    }

    IEnumerator Stun()
    {
        StunParticlesObject.SetActive(true);

        Stun_Particles.Play();

        yield return new WaitForSeconds(Random.Range(0f, 5f));

        Stun_Particles.Stop();

        StunParticlesObject.SetActive(false);

        this.enemyAnimancer.enabled = true;
        this.enemyhealth.EnemyStunned = false;
        RunOnceStunned = false;
    }
    void EnemyMove()
    {

        isPlayerNear = Physics.CheckSphere(this.transform.position, MaxDistance, playerLayer);

        if (isPlayerNear)
        {
            Debug.Log(this.gameObject.name + "Working");
            PlayerisNear = true;
        }
        else
        {
            PlayerisNear = false;

            if (this.gameObject.name == "Mini Monster")
            {
                Vector3 GetPatrol = Spot[RandomSpot].transform.position - this.transform.position;

                Quaternion RotateTo = Quaternion.LookRotation(GetPatrol);

                Quaternion LookAt = Quaternion.RotateTowards(this.transform.rotation, RotateTo, rotation * Time.deltaTime);

                this.transform.rotation = LookAt;

                if (isStopMovement == false)
                {
                    if (Vector3.Distance(this.transform.position, Spot[RandomSpot].transform.position) < MinDistance)
                    {
                        var state = enemyAnimancer.Layers[0].Play(Idle, 0.25f, FadeMode.FixedSpeed);
                        state.Speed = 1.0f;
                    }
                    else
                    {
                        var state = enemyAnimancer.Layers[0].Play(Walk, 0.25f, FadeMode.FixedSpeed);
                        state.Speed = 1.0f;
                    }

                    this.enemyControl.Move(transform.TransformDirection(Vector3.forward) * speed * Time.deltaTime);
                }
                else
                {
                    var state = enemyAnimancer.Layers[0].Play(Idle, 0.25f, FadeMode.FixedSpeed);
                    state.Speed = 1.0f;
                }

                if (Vector3.Distance(this.transform.position, Spot[RandomSpot].transform.position) < MinDistance)
                {
                    if (WaitTime <= 0.0f)
                    {
                        RandomSpot = Random.Range(0, Spot.Length);

                        WaitTime = MaxWaitTime;

                        isStopMovement = false;
                    }
                    else
                    {
                        WaitTime -= 2 * Time.deltaTime;

                        isStopMovement = true;
                    }
                }
                else
                {
                    isStopMovement = false;
                }
            }
        }

        if (this.gameObject.name == "Mini Monster (1)")
        {

            Vector3 GetPatrol = Spot[RandomSpot].transform.position - this.transform.position;

            Quaternion RotateTo = Quaternion.LookRotation(GetPatrol);

            Quaternion LookAt = Quaternion.RotateTowards(this.transform.rotation, RotateTo, rotation * Time.deltaTime);

            this.transform.rotation = LookAt;

            if (isStopMovement == false)
            {
                if (Vector3.Distance(this.transform.position, Spot[RandomSpot].transform.position) < MinDistance)
                {
                    var state = enemyAnimancer.Layers[0].Play(Idle, 0.25f, FadeMode.FixedSpeed);
                    state.Speed = 1.0f;
                }
                else
                {
                    var state = enemyAnimancer.Layers[0].Play(Walk, 0.25f, FadeMode.FixedSpeed);
                    state.Speed = 1.0f;
                }

                this.enemyControl.Move(transform.TransformDirection(Vector3.forward) * speed * Time.deltaTime);
            }
            else
            {
                var state = enemyAnimancer.Layers[0].Play(Idle, 0.25f, FadeMode.FixedSpeed);
                state.Speed = 1.0f;
            }

            if (Vector3.Distance(this.transform.position, Spot[RandomSpot].transform.position) < MinDistance)
            {
                if (WaitTime <= 0.0f)
                {
                    RandomSpot = Random.Range(0, Spot.Length);

                    WaitTime = MaxWaitTime;

                    isStopMovement = false;
                }
                else
                {
                    WaitTime -= 2 * Time.deltaTime;

                    isStopMovement = true;
                }
            }
            else
            {
                isStopMovement = false;
            }

        }

        if (this.gameObject.name == "MonsterIdle")
        {

            Vector3 GetPatrol = Spot[RandomSpot].transform.position - this.transform.position;

            Quaternion RotateTo = Quaternion.LookRotation(GetPatrol);

            Quaternion LookAt = Quaternion.RotateTowards(this.transform.rotation, RotateTo, rotation * Time.deltaTime);

            this.transform.rotation = LookAt;

            if (isStopMovement == false)
            {
                if (Vector3.Distance(this.transform.position, Spot[RandomSpot].transform.position) < MinDistance)
                {
                    var state = enemyAnimancer.Layers[0].Play(Idle, 0.25f, FadeMode.FixedSpeed);
                    state.Speed = 1.0f;
                }
                else
                {
                    var state = enemyAnimancer.Layers[0].Play(Walk, 0.25f, FadeMode.FixedSpeed);
                    state.Speed = 1.0f;
                }

                this.enemyControl.Move(transform.TransformDirection(Vector3.forward) * speed * Time.deltaTime);
            }
            else
            {
                var state = enemyAnimancer.Layers[0].Play(Idle, 0.25f, FadeMode.FixedSpeed);
                state.Speed = 1.0f;
            }

            if (Vector3.Distance(this.transform.position, Spot[RandomSpot].transform.position) < MinDistance)
            {
                if (WaitTime <= 0.0f)
                {
                    RandomSpot = Random.Range(0, Spot.Length);

                    WaitTime = MaxWaitTime;

                    isStopMovement = false;
                }
                else
                {
                    WaitTime -= 2 * Time.deltaTime;

                    isStopMovement = true;
                }
            }
            else
            {
                isStopMovement = false;
            }

        }

        if (this.gameObject.name == "Final Boss")
        {

            Vector3 GetPatrol = Spot[RandomSpot].transform.position - this.transform.position;

            Quaternion RotateTo = Quaternion.LookRotation(GetPatrol);

            Quaternion LookAt = Quaternion.RotateTowards(this.transform.rotation, RotateTo, rotation * Time.deltaTime);

            this.transform.rotation = LookAt;

            if (isStopMovement == false)
            {
                if (Vector3.Distance(this.transform.position, Spot[RandomSpot].transform.position) < MinDistance)
                {
                    var state = enemyAnimancer.Layers[0].Play(Idle, 0.25f, FadeMode.FixedSpeed);
                    state.Speed = 1.0f;
                }
                else
                {
                    var state = enemyAnimancer.Layers[0].Play(Walk, 0.25f, FadeMode.FixedSpeed);
                    state.Speed = 1.0f;
                }

                this.enemyControl.Move(transform.TransformDirection(Vector3.forward) * speed * Time.deltaTime);
            }
            else
            {
                var state = enemyAnimancer.Layers[0].Play(Idle, 0.25f, FadeMode.FixedSpeed);
                state.Speed = 1.0f;
            }

            if (Vector3.Distance(this.transform.position, Spot[RandomSpot].transform.position) < MinDistance)
            {
                if (WaitTime <= 0.0f)
                {
                    RandomSpot = Random.Range(0, Spot.Length);

                    WaitTime = MaxWaitTime;

                    isStopMovement = false;
                }
                else
                {
                    WaitTime -= 2 * Time.deltaTime;

                    isStopMovement = true;
                }
            }
            else
            {
                isStopMovement = false;
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.transform.position, MaxDistance);
        Gizmos.DrawWireSphere(this.transform.position, MinDistance);
    }
}


