using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]private GameManager game;
    [SerializeField]private GameObject[] floatyPrefab;
    public float respawnTime = 1f;
    public GameObject floatySpawnPostion;
    public Vector2 floatySize;
    //[SerializeField] public HealthBarScript healthBar;
    [SerializeField] public PlayerScript player;
    public BubbleScript bubble1, bubble2;

    public static int level;
    public static bool isBoss;
    private static AuraScript aura;


    public int exp;
    public bool hasEarnExp;


    private GameObject floaty;
    public bool isSpawn;
    public float currentHealth, maxHealth;
    public float destroyTimer;
    public bool isDestroy;
    public Animator anim;

    public bool canAttackPlayer;
    public float attackTimer, attackWaitTime;
    public int damageToPlayer;

    public int dreamChance;


    public static EnemyScript Create(Vector3 spawnPos, int playerLevel, bool boss, int min, int max)
    {
        level = playerLevel;
        isBoss = boss;
        int randomPrefab = Random.Range(min, max);
        //Debug.Log("random e: "+randomPrefab);
        Transform enemyTransform = Instantiate(GameAssetsScript.i.enemyPrefab[randomPrefab], spawnPos, Quaternion.identity);
        enemyTransform.transform.localScale = new Vector3(3, 3, 3);


        //if (isBoss) aura = AuraScript.Create(spawnPos);

        EnemyScript enemy = enemyTransform.GetComponent<EnemyScript>();
        return enemy;

    }

    private void Awake()
    {
        //isDestroy = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        //initialize();
        //healthBar.gameObject.SetActive(false);
        //spawnFloaty();
        //StartCoroutine(floatyWave());
    }
    private void initialize()
    {
        /*print("init");
        currentHealth = maxHealth;
        isDestroy = false;
        healthBar.gameObject.SetActive(true);
        healthBar.SetMaxHealth((int)maxHealth);*/
    }
    
    private void spawnFloaty()
    {
        int randomPrefab = Random.Range(0,0);
        floaty = Instantiate(floatyPrefab[randomPrefab], floatySpawnPostion.transform.position, Quaternion.identity);
        floaty.transform.localScale = floatySize;
        isSpawn = true;
        //print("spawn floaty");
        //initialize();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // if (currentHealth <= 0 && !isDestroy)
        //{
        //isDestroy = true;
        //print("true");
        //Destroy(floaty);
        //spawnFloaty();
        //healthBar.gameObject.SetActive(false);
        // }
    }
    public virtual void playSound()
    {
        
    }
    public virtual void SpawnBubble() { }
    public virtual void TakeDamage(float damage){
        currentHealth -= damage;
        //healthBar.SetHealth((int)currentHealth);
    }
    public virtual void setHealth(float h){}
    public int getMaxHealth()
    {
        return (int)maxHealth;
    }
    public int getCurrentHealth()
    {
        return (int)currentHealth;
    }
    public virtual void destroy()
    {
        //Destroy(gameObject, 1);
        //isDestroy = true;
        //SoundManager.PlaySound(SoundManager.Sound.SleepDestroy, false, 0.5f, game.getIsSoundOn());
        //if (isBoss)
        //{
        //    aura.Remove();
        //   print("remove aura");
        //}
    }
    public virtual void remove()
    {
        Destroy(gameObject);
        //healthBar.Remove();
    }
    public bool CanEarnExp()
    {
        if (isDestroy && !hasEarnExp)
        {
            hasEarnExp = true;
            return true;
        }else
        return false;
    }
    public virtual int getExp()
    {
        return exp;
    }
    public virtual bool CanAttackPlayer()
    {
        return canAttackPlayer;
    }
    public virtual void attackPlayer(Vector3 popPos)
    {

    }
    public virtual int getDamageToPlayer()
    {
        return damageToPlayer;
    }
    public int getDreamChance()
    {
        return dreamChance;
    }
}
