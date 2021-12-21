using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameManager game;
    //public GameObject gameController;
    [SerializeField] private ChangeWeaponScript weapon;
    public int maxHealth;
    public int currentHealth;
    public int attackDamage, baseAtk, recoverHp, reduceDmg;
    public float atkSpeed, criticalHitChance, critDmgMultiply;
    public bool chooseStr, chooseAgi, chooseDef;

    private int exp;
    private int exptonextlevel;
    private int level;

    private Animator anim;
    private float attackTimer, attackWaitTime;
    private bool isAttack, attacking;

    private int weaponLevel, weaponEnhanceLevel;
    private int stoneNumber;
    private float successUpgradePercent;

    private float healTime, dodgePercent;

    private int skillPoint,
        strengthSkill1Level, strengthSkill2Level, strengthSkill3Level, 
        agilitySkill1Level, agilitySkill2Level, agilitySkill3Level, 
        defenseSkill1Level, defenseSkill2Level, defenseSkill3Level;

    public HealthBarScript healthBar, expBar;

    public bool isFaint;

    private int adsTimes;
    private string dateReceivedToday;

    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    void Start()
    {

        weapon.idleIronSword();
        anim = GetComponent<Animator>();

        resetGame();
        /*
        maxHealth = 20;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        isFaint = false;

        baseAtk = 2;
        attackWaitTime = 1f;
        attackTimer = 1f;
        atkSpeed = 0.25f;
        criticalHitChance = 5f;
        critDmgMultiply = 1;
        isAttack = false;
        attacking = false;

        weaponLevel = 1;
        weaponEnhanceLevel = 1;
        checkWeaponSkillLevel();

        skillPoint = 1;
        strengthSkill1Level = 0;
        strengthSkill2Level = 0;
        strengthSkill3Level = 0;
        agilitySkill1Level = 0;
        agilitySkill2Level = 0;
        agilitySkill3Level = 0;
        defenseSkill1Level = 0;
        defenseSkill2Level = 0;
        defenseSkill3Level = 0;

        healTime = 3f;
        dodgePercent = 0f;

        stoneNumber = 0;

        exp = 0;
        level = 1;
        exptonextlevel = 5;
        expBar.SetMaxHealth(exptonextlevel);
        expBar.SetHealth(exp);
        */
    }

    // Update is called once per frame
    void Update()
    {
        
        if (anim.GetBool("isAttacking") && attackTimer < attackWaitTime)
        {
            attackTimer += Time.deltaTime;
        }else{
            anim.SetBool("isAttacking", false);
        }
        if (skillPoint > 0 && level <= 30)
        {
            useSkillPoint();
        }
        /*
        if (Input.GetKeyDown(KeyCode.T))
        {
            gainExp(1000000000);
        }
        
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadPlayer();
        }
        */
        for (int i=0;i<Input.touchCount; i++)
        {
            if(Input.GetTouch(i).phase == TouchPhase.Began)
            {
                
            }
        }

        checkLevel();
        if(defenseSkill2Level > 0)
        {
            healOverTime();
        }
        if (weaponLevel < 9)
            successUpgradePercent = 100 / Mathf.Pow(2, weaponLevel + weaponEnhanceLevel - 2);
        else
            successUpgradePercent = 0;

        checkEnhanceWeapon();
    }
    public void TakeDamage(int damage)
    {
        if (defenseSkill3Level > 0)
        {
            counterAttack(damage);
        }
        if(agilitySkill3Level > 0)
        {
            float rand = Random.Range(0f, 100f);
            dodgePercent = agilitySkill3Level * 10;
            if (rand < dodgePercent)
            {
                damage = 0;
                print("dodge");
            }
        }

        reduceDmg = defenseSkill1Level * 5;
        int d = damage - reduceDmg;
        if (d < 0) d = 0;
        //print("d" + damage);
        currentHealth -= d;
        healthBar.SetHealth(currentHealth);

        Vector3 popPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        DamagePopupScript.Create(popPos, d, 1);

        if (currentHealth <= 0)
        {
            isFaint = true;
        }
    }

    public bool getIsFaint()
    {
        return isFaint;
    }
    public void reset()
    {
        currentHealth = maxHealth;
        isFaint = false;
    }

    public int getAttackDamage()
    {
        return attackDamage;
    }
    //PLAYER ATTACK
    public void attack()
    {
        anim.SetBool("isAttacking", true);
        attackTimer = 0;
        if (anim.GetInteger("attacknumber") < 4)
        {
            int i = anim.GetInteger("attacknumber");
            anim.SetInteger("attacknumber", i+1);
            SlashWaveScript.Create(transform.position, i+1, 1);
        }
        else
        {
            anim.SetInteger("attacknumber", 1);
            SlashWaveScript.Create(transform.position, 1, 1);
        }
        criticalHitChance = (strengthSkill3Level+1) * 5;
        critDmgMultiply = (strengthSkill3Level + 4) * 0.5f;
        bool isCriticalHit = Random.Range(0f, 100f) < criticalHitChance;
        int crit;
        if (isCriticalHit)
        {
            attackDamage = Mathf.RoundToInt(Random.Range(baseAtk+2, baseAtk + 4 + level / 3) * critDmgMultiply);
            //if (strengthSkill2Level > 1)
            //    healthBar.SetHealth(currentHealth);
            if(strengthSkill2Level > 0)
                recover();
            crit = 2;
        }
        else
        {
            attackDamage = Random.Range(baseAtk, baseAtk + 2 + level / 3);
            crit = 1;
        }

        if (agilitySkill2Level > 0) healEachAtk(getAttackDamage());

        Vector3 popPos = new Vector3(transform.position.x, transform.position.y + 5f, transform.position.z);
        DamagePopupScript.Create(popPos, getAttackDamage(), crit);
        //print(getAttackDamage());
        Vector3 effectPos = new Vector3(transform.position.x, transform.position.y + 5f, transform.position.z);
        SlashEffectScript.Create(effectPos, 0);

        int ran = Random.Range(0, 3);
        switch (ran)
        {
            case 2:
                SoundManager.PlaySound(SoundManager.Sound.PlayerAttack3, false, 1, game.getIsSoundOn());
                break;
            case 1:
                SoundManager.PlaySound(SoundManager.Sound.PlayerAttack2, false, 1, game.getIsSoundOn());
                break;
            case 0:
            default:
                SoundManager.PlaySound(SoundManager.Sound.PlayerAttack1, false, 1, game.getIsSoundOn());
                break;
        }
    }
    public void counterAttack(int damage)
    {
        attackDamage = damage*defenseSkill3Level;
        Vector3 popPos = new Vector3(transform.position.x, transform.position.y + 5f, transform.position.z);
        DamagePopupScript.Create(popPos, getAttackDamage(), 1);
    }
    private void changeAnimation()
    {
        
    }
    public void gainExp(int e)
    {
        if (level < 30)
        {
            exp += e;
            expBar.SetHealth(exp);
        }
        //print("exp point: " + exp);
    }
    public bool getAttacking()
    {
        return attacking;
    }
    public void setAttacking()
    {
        attacking = false;
    }
    public void goingToAttack()
    {
        isAttack = true;
    }
    public bool canAttack()
    {
        if (attackTimer > atkSpeed && !isAttack)
        {
            return true;
        }
        else return false;
    }
    public int getMaxHP()
    {
        return maxHealth;
    }
    public float getAttackSpeed()
    {
        return atkSpeed;
    }
    public int getBaseAttack()
    {
        return baseAtk;
    }
    private void checkLevel()
    {
        if(exp >= exptonextlevel)
        {
            level++;
            skillPoint++;
            exp -= exptonextlevel;
            exptonextlevel = (int)(exptonextlevel * 1.5f);
            expBar.SetMaxHealth(exptonextlevel);
            expBar.SetHealth(exp);
            LevelUpScript.Create(new Vector3(transform.position.x, transform.position.y + 1f));

            checkPlayerStats();
            SoundManager.PlaySound(SoundManager.Sound.LevelUp, false, 1, game.getIsSoundOn());

        }
    }
    private void checkPlayerStats()
    {
        baseAtk = 5 + (level-1) *2 + (weaponLevel-1) * (5*weaponEnhanceLevel) + strengthSkill1Level * 5;
        print("base atk: "+baseAtk +"level: " +level+ "weapon level: " + weaponLevel + "weapon enhance level: " + weaponEnhanceLevel + "str1: " + strengthSkill1Level);
        maxHealth = 20 + (level - 1)*5 + defenseSkill1Level * 10;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(maxHealth);
        atkSpeed = 0.25f - agilitySkill1Level * 0.03f;
    }
    public int getLevel()
    {
        return level;
    }
    public int getWeaponLevel()
    {
        return weaponLevel;
    }
    public int getEnhanceLevel()
    {
        return weaponEnhanceLevel;
    }
    public int getStoneNumber()
    {
        return stoneNumber;
    }
    public void upgradeWeapon()
    {
        if(stoneNumber >= 1 && weaponLevel < 9)
        {
            float i = Random.Range(0, 100f);
            print("weapon success upgrade percentage = " + i);
            if (i <= successUpgradePercent)
            {
                weaponLevel++;
                //baseAtk += 5;
                game.successUpgradeWeapon();

                if(weaponLevel >= 9 && weaponEnhanceLevel < 2)
                {
                    weaponEnhanceLevel++;
                    weaponLevel = 1;
                }
                checkPlayerStats();
            }
            stoneNumber--;
            SavePlayer();
        }
    }
    public void checkEnhanceWeapon()
    {
        anim.SetInteger("idWeapon", weaponEnhanceLevel);

        //print("weaponEnhanceLevel: " + weaponEnhanceLevel);
    }
    public float getSuccessUpgradePercent()
    {
        return successUpgradePercent;
    }
    public void earnStone()
    {
        stoneNumber++;
    }

    public int getSkillPoint()
    {
        return skillPoint;
    }
    public void useSkillPoint()
    {
        int i = Random.Range(0, 10);
        switch (i)
        {
            case 9:
                if (defenseSkill3Level > 9) useSkillPoint();
                else
                {
                    upgradeDefenseSkill3();
                    //print("9: def 3");
                    skillPoint--;
                }
                break;
            case 8:
                if(defenseSkill2Level > 9) useSkillPoint();
                else
                {
                    upgradeDefenseSkill2();
                    //print("8: def 2");
                    skillPoint--;
                }
                break;
            case 7:
                if (defenseSkill1Level > 9) useSkillPoint();
                else
                {
                    upgradeDefenseSkill1();
                    //print("7: def 1");
                    skillPoint--;
                }
                break;
            case 6:
                if (agilitySkill3Level > 9) useSkillPoint();
                else
                {
                    upgradeAgilitySkill3();
                    //print("6: agi 3");
                    skillPoint--;
                }
                
                break;
            case 5:
                if (agilitySkill2Level > 9) useSkillPoint();
                else
                {
                    upgradeAgilitySkill2();
                    //print("5: agi 2");
                    skillPoint--;
                }
                break;
            case 4:
                if (agilitySkill1Level > 9) useSkillPoint();
                else
                {
                    upgradeAgilitySkill1();
                    //print("4: agi 1");
                    skillPoint--;
                }
                break;
            case 3:
                if (strengthSkill3Level > 9) useSkillPoint();
                else
                {
                    upgradeStrengSkill3();
                    //print("3: str 3");
                    skillPoint--;
                }
                break;
            case 2:
                if (strengthSkill2Level > 9) useSkillPoint();
                else
                {
                    upgradeStrengSkill2();
                    //print("2: str 2");
                    skillPoint--;
                }
                break;
            case 1:
            default:
                if (strengthSkill1Level > 9) useSkillPoint();
                else
                {
                    upgradeStrengSkill1();
                    //print("1: str 1");
                    skillPoint--;
                }
                break;
        }
    }
    public void recover()
    {
        print("recover crit hit");
        recoverHp = getAttackDamage() /5 * strengthSkill2Level;
        currentHealth += recoverHp;
        if (currentHealth > maxHealth) currentHealth = maxHealth;

        healthBar.SetHealth(currentHealth);
        //print("play current hp: " + currentHealth);

        Vector3 popPos = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        DamagePopupScript.Create(popPos, recoverHp, 3);
    }
    public void healOverTime()
    {
        if (healTime >= 0)
        {
            //print("time -" + healTime);
            healTime -= Time.deltaTime;
        }else
        {
            healTime = 2;
            currentHealth += defenseSkill2Level * 20;
            if (currentHealth > maxHealth) currentHealth = maxHealth;
            healthBar.SetHealth(currentHealth);
            Vector3 popPos = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
            DamagePopupScript.Create(popPos, defenseSkill2Level * 10, 3);
            //print("heal over time" + currentHealth);
        }
    }
    public void healEachAtk(int damage)
    {
        int healP = agilitySkill2Level * damage / 10 + 1;
        float i = Random.Range(0, 100f);
        if (i <= agilitySkill2Level * 10)
        {
            //print("heal each atk = " + i);
            currentHealth += healP;
            //print("healP: " + healP);
            if (currentHealth > maxHealth) currentHealth = maxHealth;
            healthBar.SetHealth(currentHealth);
            Vector3 popPos = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
            DamagePopupScript.Create(popPos, healP, 3);
        }

    }
    public void fullHeal()
    {
        //maxHealth = 20 + level * 5;
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
    }
    public bool getChooseStr()
    {
        return chooseStr;
    }
    public bool getChooseAgi()
    {
        return chooseAgi;
    }
    public bool getChooseDef()
    {
        return chooseDef;
    }
    public void chooseStrength()
    {
        chooseStr = true;
    }
    public void chooseAgility()
    {
        chooseAgi = true;
    }
    public void chooseDefense()
    {
        chooseDef = true;
    }

    public int getStrengthSkill1Level()
    {
        return strengthSkill1Level;
    }
    public int getStrengthSkill2Level()
    {
        return strengthSkill2Level;
    }
    public int getStrengthSkill3Level()
    {
        return strengthSkill3Level;
    }
    public int getAgilitySkill1Level()
    {
        return agilitySkill1Level;
    }
    public int getAgilitySkill2Level()
    {
        return agilitySkill2Level;
    }
    public int getAgilitySkill3Level()
    {
        return agilitySkill3Level;
    }
    public int getDefenseSkill1Level()
    {
        return defenseSkill1Level;
    }
    public int getDefenseSkill2Level()
    {
        return defenseSkill2Level;
    }
    public int getDefenseSkill3Level()
    {
        return defenseSkill3Level;
    }
    public void upgradeStrengSkill1()
    {
        strengthSkill1Level++;
        checkPlayerStats();
        print("player streng1 +1");
    }
    public void upgradeStrengSkill2()
    {
        strengthSkill2Level++;
        print("player streng2 +1");
    }
    public void upgradeStrengSkill3()
    {
        strengthSkill3Level++;
        print("player streng3 +1");
    }
    public void upgradeAgilitySkill1()
    {
        agilitySkill1Level++;
        checkPlayerStats();
        print("player agility1 +1");
    }
    public void upgradeAgilitySkill2()
    {
        agilitySkill2Level++;
        print("player agility2 +1");
    }
    public void upgradeAgilitySkill3()
    {
        agilitySkill3Level++;
        print("player agility3 +1");
    }
    public void upgradeDefenseSkill1()
    {
        defenseSkill1Level++;
        checkPlayerStats();
        print("player defense1 +1");
    }
    public void upgradeDefenseSkill2()
    {
        defenseSkill2Level++;
        print("player defense2 +1");
    }
    public void upgradeDefenseSkill3()
    {
        defenseSkill3Level++;
        print("player defense3 +1");
    }
    //ADS
    public int getAdsTimes()
    {
        return adsTimes;
    }
    public void setAdsTimes(int i)
    {
        adsTimes = i;
        SavePlayer();
    }
    public void watchAds()
    {
        stoneNumber += 3;
        adsTimes--;
        SavePlayer();
    }
    public string getDateReceivedToday()
    {
        return dateReceivedToday;
    }
    public void setDateReceivedToday(string s)
    {
        dateReceivedToday = s;
    }

    //SAVE
    public void SavePlayer()
    {
        SaveSystem.Save(this);
    }
    //LOAD
    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.Load();
        if(data != null)
        {
            stoneNumber = data.stones;
            weaponLevel = data.weaponLevel;
            weaponEnhanceLevel = data.enhanceLevel;
            adsTimes = data.adsTimes;
            dateReceivedToday = data.dateReceived;
        }
    }
    
    public void resetGame()
    {
        exp = 0;
        level = 1;
        exptonextlevel = 4;
        expBar.SetMaxHealth(exptonextlevel);
        expBar.SetHealth(exp);

        /*
        maxHealth = 20 + level*5 + defenseSkill1Level*10;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        baseAtk = 2;
        atkSpeed = 0.25f;
        */

        isFaint = false;
        attackWaitTime = 1f;
        attackTimer = 1f;
        criticalHitChance = 5f;
        critDmgMultiply = 1;
        isAttack = false;
        attacking = false;


        chooseStr = false;
        chooseAgi = false;
        chooseDef = false;

        skillPoint = 1;
        strengthSkill1Level = 0;
        strengthSkill2Level = 0;
        strengthSkill3Level = 0;
        agilitySkill1Level = 0;
        agilitySkill2Level = 0;
        agilitySkill3Level = 0;
        defenseSkill1Level = 0;
        defenseSkill2Level = 0;
        defenseSkill3Level = 0;

        stoneNumber = 0;
        LoadPlayer();
        if (weaponLevel==0)  weaponLevel = 1;
        if (weaponEnhanceLevel == 0)
        {
            weaponEnhanceLevel = 1;
            print("weapon enhance lv 0");
        }
        checkPlayerStats();
        //weaponEnhanceLevel = 2;
        //level = 30;

        healTime = 2f;
        dodgePercent = 0f;


        
    }
}
