using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatus : MonoBehaviour
{
    CharacterMovement charactermovement;
    SoundManager soundmanager;
    // public Slider playerHealth;
    // public float health;
    float totalDamage;
    float damageMultiplier;

    //public Slider playerMana;
    // public float mana;
    // float maxMana = 100f;

    public Slider playerEXP;
    public float exp;
    float maxEXP = 40f;

    GameObject Spell1, Spell2;

    Button Attack, Skill1, Skill2;
    public int Level = 0;
    // Start is called before the first frame update

    Text Level_Text;

    int Selected_Character;

    private void Awake()
    {
        Level_Text = GameObject.Find("/Canvas/Level_Text").GetComponent<Text>();

        Selected_Character = ES3.Load<int>("Character Selected");

        Spell1 = GameObject.Find("/Canvas/Healbtn").gameObject;

        Spell2 = GameObject.Find("/Canvas/Spell1btn").gameObject;

        Attack = GameObject.Find("/Canvas/AttackBtn").GetComponent<Button>();

        Skill1 = Spell1.GetComponent<Button>();

        Skill2 = Spell2.GetComponent<Button>();

        if (Selected_Character == 0)
        {

            charactermovement = GameObject.Find("/chemist").GetComponent<CharacterMovement>();


        }

        else if (Selected_Character == 1)
        {


            charactermovement = GameObject.Find("/paramedic").GetComponent<CharacterMovement>();

        }

        else if (Selected_Character == 2)
        {

            charactermovement = GameObject.Find("/nurse").GetComponent<CharacterMovement>();

        }
    }
    void Start()
    {

        soundmanager = GameObject.FindObjectOfType<SoundManager>();
        // player health
        /*
        playerHealth = GameObject.Find("/Canvas/Player Health").GetComponent<Slider>();
        health = maxHealth;
        playerHealth.maxValue = maxHealth;
        playerHealth.value = health; //health from 


        // player mana
        playerMana = GameObject.Find("/Canvas/Player Mana").GetComponent<Slider>();
        mana = maxMana;
        playerMana.maxValue = maxMana;
        playerMana.value = mana; // begin of mana
        */

        // player Exp
        playerEXP = GameObject.Find("/Canvas/Player Exp").GetComponent<Slider>();
        exp = 0;
        playerEXP.maxValue = maxEXP;
        playerEXP.value = exp; // begin of EXP

        Spell1.SetActive(false);

        Spell2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Level_Text.text = "Level: " + Level.ToString("0");
        /*
        if (health >= maxHealth)
        {
            health = maxHealth;
        }

        if (mana >= maxMana)
        {
            mana = maxMana;
        }
*/

        if (charactermovement.isThrowingMode)
        {
            if (Attack.interactable == true)
            {
                Attack.interactable = false;
                StartCoroutine(DisableAttackForWhile());

            }
        }

        if (charactermovement.isSkillMode)
        {
            if (Skill1.interactable == true)
            {
                StartCoroutine(DisableSkills());
            }
        }

        if (exp >= maxEXP)
        {
            maxEXP += 10f;
            exp = 0.0f;

            Level += 1;

            playerEXP.value = exp;
        }

        if (Level >= 1)
        {
            if (Spell1.activeSelf == false)
            {
                Spell1.SetActive(true);
            }
        }

        if (Level >= 2)
        {
            if (Spell2.activeSelf == false)
            {
                Spell2.SetActive(true);
            }
        }

    }

    /*
    public void playerTakeDamage(float amount)
    {
        if (health != 0)
        {
            soundmanager.PlaySound("GolemAttack");

            //damageMultiplier = amount / (amount + 50f);
            //totalDamage = amount * damageMultiplier; 
            health -= amount;

            //this.enemyHealth -= totalDamage;
            playerHealth.value = health;
        }
    }
*/
    public void pMana()
    {

        /*
       if (mana != 0)
       {
           damageMultiplier = amount / (amount + 50f);
           //totalDamage = amount * damageMultiplier; 
           //mana -= amount;

           //this.enemyHealth -= totalDamage;
           //playerMana.value = mana;


       }
       */
    }
    IEnumerator DisableAttackForWhile()
    {
        yield return new WaitForSeconds(1.0f);

        Attack.interactable = true;
    }
    IEnumerator DisableSkills()
    {
        Skill1.interactable = false;
        Skill2.interactable = false;

        if (charactermovement.Special_Skill == true)
        {
            yield return new WaitForSeconds(5.0f);
        }
        else
        {
            yield return new WaitForSeconds(charactermovement.BuffAttackSpeedTime + 5.0f);
        }

        Skill1.interactable = true;
        Skill2.interactable = true;
    }

    /*
    public void AddMana(float amount)
    {
        mana += amount;

        playerMana.value = mana;
    }
*/
    public void pEXP(float amount)
    {
        exp += amount;

        playerEXP.value = exp;
    }

    /*
    public void pHealth(float amount)
    {
        health += amount;

        playerHealth.value = health;
    }

    */
}
