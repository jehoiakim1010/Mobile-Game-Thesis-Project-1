using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;
using UnityEngine.UI;
public class CharacterMovement : MonoBehaviour
{
    PlayerDamage playerdamage;
    inventory inventory;
    PlayerStatus playerstatus;

    SoundManager soundmanager;
    AnimancerComponent animancer;
    [SerializeField] ClipState.Transition Idle, Walk, Run;
    [SerializeField] AnimationClip Attack1, Attack2, Heal, Throwing;

    [SerializeField] AnimationClip SpecialSkill1, SpecialSkill2, SpecialSkill3;
    [SerializeField] float speed;
    [SerializeField] float runSpeed;
    float velocity;
    [SerializeField] float rotatation;
    [SerializeField] float yy, zz;
    Vector3 gravVelocity;
    float gravity = -100;
    bool isGround;
    LayerMask layerMask;

    //Skill
    [HideInInspector] public bool BuffSpeed;

    [HideInInspector] public bool Special_Skill;

    bool clickOnce;
    CharacterController charControl;

    //characterAttack
    public int NumberAttack;
    float LastAttack;
    public bool isSwordMode;
    [SerializeField] float LightAttackDelay;
    [SerializeField] float LightAttackANimationSpeed;

    //Attack Speed
    public float BuffAttackSpeedTime;
    Text BuffAttackSpeedText;
    GameObject BuffAttackSpeedObj;

    int Selected_Character;

    public bool isSkillMode;

    GameObject Spawnpoint;
    Vector3 movement;

    public bool isThrowingMode;

    bool EnableAttacks;
    void Awake()
    {


        Selected_Character = ES3.Load<int>("Character Selected");

        inventory = GameObject.FindObjectOfType<inventory>();

        soundmanager = GameObject.FindObjectOfType<SoundManager>();

        BuffAttackSpeedObj = GameObject.Find("/Canvas/AttackSpeedBuff").gameObject;

        BuffAttackSpeedText = GameObject.Find("/Canvas/AttackSpeedBuff/Text").GetComponent<Text>();

    }
    void Start()
    {

        if (Selected_Character == 0)
        {

            playerstatus = GameObject.Find("/chemist").GetComponent<PlayerStatus>();

            Spawnpoint = GameObject.Find("/chemist").transform.Find("itemDrop").gameObject;

            inventory = GameObject.Find("/chemist").GetComponent<inventory>();

            //playerdamage = this.gameObject.transform.Find("mixamorig:Hips/mixamorig:Spine/mixamorig:Spine1/mixamorig:RightShoulder/mixamorig:RightArm/mixamorig:RightForeArm/mixamorig:RightHand").transform.Find("SyringeWeapon").GetComponent<PlayerDamage>();

            if (ES3.KeyExists("Inventory"))
            {
                inventory.NumberOfItemsInSlot = ES3.Load<int[]>("Number Of Items");

                inventory.GameObject_Slots = ES3.Load<string[]>("Inventory");

                for (int a = 0; a < inventory.GameObject_Slots.Length; a++)
                {
                    if (inventory.GameObject_Slots[a] == "healthPotion(Clone)")
                    {
                        for (int i = 0; i < inventory.NumberOfItemsInSlot[a]; i++)
                        {
                            Instantiate(Resources.Load<GameObject>("healthPotion"), inventory.slots[a].transform, false);

                            inventory.isFull[a] = true;
                        }
                    }


                    if (inventory.GameObject_Slots[a] == "manaPotion(Clone)")
                    {

                        for (int i = 0; i < inventory.NumberOfItemsInSlot[a]; i++)
                        {
                            Instantiate(Resources.Load<GameObject>("manaPotion"), inventory.slots[a].transform, false);

                            inventory.isFull[a] = true;
                        }
                    }
                }

            }
        }

        else if (Selected_Character == 1)
        {
            playerstatus = GameObject.Find("/paramedic").GetComponent<PlayerStatus>();
            Spawnpoint = GameObject.Find("/paramedic").transform.Find("itemDrop").gameObject;
            inventory = GameObject.Find("/paramedic").GetComponent<inventory>();

            playerdamage = this.gameObject.transform.Find("mixamorig:Hips").transform.Find("mixamorig:Spine").transform.Find("mixamorig:Spine1").transform.Find("mixamorig:Spine2")
            .transform.Find("mixamorig:RightShoulder").transform.Find("mixamorig:RightArm").transform.Find("mixamorig:RightForeArm").transform.Find("mixamorig:RightHand")
            .transform.Find("Shield").GetComponent<PlayerDamage>();

            LightAttackANimationSpeed = 1.5f;
            LightAttackDelay = 0.4f;

            if (ES3.KeyExists("Inventory"))
            {
                inventory.NumberOfItemsInSlot = ES3.Load<int[]>("Number Of Items");

                inventory.GameObject_Slots = ES3.Load<string[]>("Inventory");

                for (int a = 0; a < inventory.GameObject_Slots.Length; a++)
                {

                    if (inventory.GameObject_Slots[a] == "healthPotion(Clone)")
                    {
                        for (int i = 0; i < inventory.NumberOfItemsInSlot[a]; i++)
                        {
                            Instantiate(Resources.Load<GameObject>("healthPotion"), inventory.slots[a].transform, false);

                            inventory.isFull[a] = true;
                        }
                    }


                    if (inventory.GameObject_Slots[a] == "manaPotion(Clone)")
                    {

                        for (int i = 0; i < inventory.NumberOfItemsInSlot[a]; i++)
                        {

                            Instantiate(Resources.Load<GameObject>("manaPotion"), inventory.slots[a].transform, false);

                            inventory.isFull[a] = true;
                        }
                    }
                }
            }

        }

        else if (Selected_Character == 2)
        {

            playerdamage = this.gameObject.transform.Find("mixamorig:Hips").transform.Find("mixamorig:Spine").transform.Find("mixamorig:Spine1").transform.Find("mixamorig:Spine2")
            .transform.Find("mixamorig:RightShoulder").transform.Find("mixamorig:RightArm").transform.Find("mixamorig:RightForeArm").transform.Find("mixamorig:RightHand")
            .transform.Find("SyringeWeapon").GetComponent<PlayerDamage>();

            playerstatus = GameObject.Find("/nurse").GetComponent<PlayerStatus>();
            Spawnpoint = GameObject.Find("/nurse").transform.Find("itemDrop").gameObject;
            inventory = GameObject.Find("/nurse").GetComponent<inventory>();

            if (ES3.KeyExists("Inventory"))
            {
                inventory.NumberOfItemsInSlot = ES3.Load<int[]>("Number Of Items");

                inventory.GameObject_Slots = ES3.Load<string[]>("Inventory");

                for (int a = 0; a < inventory.GameObject_Slots.Length; a++)
                {

                    if (inventory.GameObject_Slots[a] == "healthPotion(Clone)")
                    {
                        for (int i = 0; i < inventory.NumberOfItemsInSlot[a]; i++)
                        {
                            Instantiate(Resources.Load<GameObject>("healthPotion"), inventory.slots[a].transform, false);

                            inventory.isFull[a] = true;
                        }
                    }


                    if (inventory.GameObject_Slots[a] == "manaPotion(Clone)")
                    {

                        for (int i = 0; i < inventory.NumberOfItemsInSlot[a]; i++)
                        {
                            Instantiate(Resources.Load<GameObject>("manaPotion"), inventory.slots[a].transform, false);

                            inventory.isFull[a] = true;
                        }
                    }
                }
            }

        }


        animancer = this.gameObject.GetComponent<AnimancerComponent>();

        charControl = GetComponent<CharacterController>();
        layerMask = LayerMask.GetMask("Ground");

        BuffAttackSpeedObj.SetActive(false);
    }

    private void FixedUpdate()
    {
        playerGravity();
    }
    void playerGravity()
    {
        isGround = Physics.CheckSphere(this.gameObject.transform.position, 1f, layerMask);

        gravVelocity.y += gravity * Time.deltaTime;
        charControl.Move(gravVelocity * Time.deltaTime);

        if (isGround && gravVelocity.y < 0)
        {
            gravVelocity.y = -10f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isSkillMode == false && isThrowingMode == false)
        {
            if (isSwordMode == false)
            {
                movement = new Vector3(SimpleInput.GetAxis("Horizontal"), 0.0f, SimpleInput.GetAxis("Vertical")) * Time.deltaTime;
            }
            else
            {
                movement = new Vector3(0.0f, 0.0f, 0.0f);
            }
            //Range of Attack 0 to N
            NumberAttack = Mathf.Clamp(NumberAttack, 0, 2);

            //movement of character

            if (movement.magnitude >= 0.001f)
            {
                isSwordMode = false;

                charControl.Move(transform.forward * speed * Time.deltaTime);
                var state = animancer.Layers[0].Play(Run, 0.25f, FadeMode.FixedSpeed);
                state.Speed = 1.0f;

                float PlayerAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;

                float Angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, PlayerAngle, ref velocity, rotatation * Time.deltaTime);

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0.0f, Angle, 0.0f), 7f * Time.deltaTime);
            }

            else
            {
                if (Time.time - LastAttack > LightAttackDelay)
                {
                    NumberAttack = 0;
                    isSwordMode = false;
                }
                if (isSwordMode == false)
                {
                    animancer.Layers[0].Play(Idle, 0.25f, FadeMode.FixedSpeed);
                }
            }

        }
        else
        {
            if (this.BuffSpeed == true)
            {
                var state = animancer.Layers[0].Play(Heal, 0.25f, FadeMode.FixedSpeed);
                state.Speed = 2.0f;

                state.Events.OnEnd = () =>
                {
                    StartCoroutine(AddSpeed());

                };

            }

            if (this.Special_Skill == true)
            {
                if (Selected_Character == 0)
                {
                    var state = animancer.Layers[0].Play(SpecialSkill1, 0.25f, FadeMode.FixedSpeed);
                    state.Speed = 2.0f;

                    state.Events.OnEnd = () =>
                    {
                        GameObject PlayerAttack = PlayerPoolDamage.Pooling_Instance.GamePooled();
                        PlayerAttack.SetActive(true);
                        PlayerAttack.transform.position = this.Spawnpoint.transform.position;

                        this.isSkillMode = false;
                        this.Special_Skill = false;
                        clickOnce = false;
                    };
                }

                else if (Selected_Character == 1)
                {
                    var state = animancer.Layers[0].Play(SpecialSkill2, 0.25f, FadeMode.FixedSpeed);
                    state.Speed = 2.0f;

                    state.Events.OnEnd = () =>
                    {
                        GameObject PlayerAttack = PlayerPoolDamage.Pooling_Instance.GamePooled();
                        PlayerAttack.SetActive(true);
                        PlayerAttack.transform.position = this.Spawnpoint.transform.position;

                        this.isSkillMode = false;
                        this.Special_Skill = false;
                        clickOnce = false;
                    };
                }

                else if (Selected_Character == 2)
                {
                    var state = animancer.Layers[0].Play(SpecialSkill3, 0.25f, FadeMode.FixedSpeed);
                    state.Speed = 2.0f;

                    state.Events.OnEnd = () =>
                    {
                        GameObject PlayerAttack = PlayerPoolDamage.Pooling_Instance.GamePooled();
                        PlayerAttack.SetActive(true);
                        PlayerAttack.transform.position = this.Spawnpoint.transform.position;

                        this.isSkillMode = false;
                        this.Special_Skill = false;
                        clickOnce = false;
                    };
                }
            }
        }
    }

    public void btnAttack()
    {
        if (movement.magnitude <= 0f)
        {
            if (this.isSkillMode == false)
            {
                if (Selected_Character == 0)
                {

                    if (isThrowingMode == false)
                    {
                        isThrowingMode = true;

                        soundmanager.PlaySound("PlayerHit");


                        var state = animancer.Layers[0].Play(Throwing, 0.25f, FadeMode.FixedSpeed);

                        state.Speed = LightAttackANimationSpeed;

                        state.Events.OnEnd = () =>
                        {
                            if (isThrowingMode == true)
                            {
                                isThrowingMode = false;

                                GameObject PlayerAttack = PlayerPool.Pooling_Instance.GamePooled();
                                PlayerAttack.SetActive(true);
                                PlayerAttack.transform.position = this.Spawnpoint.transform.position;
                                PlayerAttack.GetComponent<Rigidbody>().velocity = this.transform.forward * 200f;
                            }

                        };
                    }

                }

                else if (Selected_Character == 1)
                {
                    LastAttack = Time.time;
                    NumberAttack++;
                    isSwordMode = true;

                    if (NumberAttack == 1)
                    {
                        soundmanager.PlaySound("PlayerHit");

                        var state = animancer.Layers[0].Play(Attack1, 0.25f, FadeMode.FixedSpeed);
                        state.Speed = LightAttackANimationSpeed;
                    }
                }
                else if (Selected_Character == 2)
                {
                    LastAttack = Time.time;
                    NumberAttack++;
                    isSwordMode = true;

                    if (NumberAttack == 1)
                    {
                        soundmanager.PlaySound("PlayerHit");

                        var state = animancer.Layers[0].Play(Attack1, 0.25f, FadeMode.FixedSpeed);
                        state.Speed = LightAttackANimationSpeed;

                    }
                }
            }
        }
    }

    public void btnAttack2()
    {
        if (NumberAttack >= 2)
        {
            soundmanager.PlaySound("PlayerHit");
            var state = animancer.Layers[0].Play(Attack2, 0.25f, FadeMode.FixedSpeed);
            state.Speed = LightAttackANimationSpeed;
        }
        else
        {
            animancer.Stop(Attack1);
            NumberAttack = 0;
        }
    }
    public void reset()
    {

        animancer.Stop(Attack2);

        if (NumberAttack == 1)
        {
            soundmanager.PlaySound("PlayerHit");
            var state = animancer.Layers[0].Play(Attack1, 0.25f, FadeMode.FixedSpeed);
            state.Speed = LightAttackANimationSpeed;
            NumberAttack = 1;
        }
        else
        {
            animancer.Stop(Attack1);
        }
        NumberAttack = 0;

    }

    public void Buff_AttackSpeed()
    {
        if (isThrowingMode == false && isSwordMode == false)
        {
            if (clickOnce == false)
            {
                soundmanager.PlaySound("Heal");
                this.isSkillMode = true;
                this.BuffSpeed = true;

                clickOnce = true;

                BuffAttackSpeedTime = Random.Range(0, 15f);

            }
        }
    }

    IEnumerator AddSpeed()
    {
        BuffAttackSpeedObj.SetActive(true);

        BuffAttackSpeedText.text = BuffAttackSpeedTime.ToString("0");

        this.isSkillMode = false;
        this.BuffSpeed = false;

        clickOnce = false;

        if (Selected_Character == 1)
        {
            LightAttackANimationSpeed = 2.5f;
            LightAttackDelay = 0.3f;
        }
        else
        {
            LightAttackANimationSpeed = 3.0f;
            LightAttackDelay = 0.2f;
        }

        yield return new WaitForSeconds(BuffAttackSpeedTime);

        if (Selected_Character == 1)
        {
            LightAttackANimationSpeed = 1.5f;
            LightAttackDelay = 0.4f;
        }
        else
        {
            LightAttackANimationSpeed = 2.0f;
            LightAttackDelay = 0.5f;
        }

        BuffAttackSpeedObj.SetActive(false);
    }

    public void SpecialSkill()
    {
        if (isThrowingMode == false && isSwordMode == false)
        {
            if (clickOnce == false)
            {
                soundmanager.PlaySound("Rage");

                this.isSkillMode = true;
                this.Special_Skill = true;
                clickOnce = true;
            }
        }
    }
}
