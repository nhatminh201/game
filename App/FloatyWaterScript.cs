using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatyWaterScript : EnemyScript
{
    public GameObject gameobj;
    //[SerializeField]private HealthBarScript healthBar;
    public FloatyWaterScript()
    {

    }
    private void Awake()
    {
        anim = GetComponent<Animator>();
        initialize();
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    protected override void Update()
    {
        
        if(currentHealth <=0 && !isDestroy)
        {
            anim.SetBool("isDestroy", true);
            //Destroy(gameObject, 1);
            //isDestroy = true;
            //base.destroy();
            destroy();
        }

        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                
            }
        }


        if (Input.GetMouseButtonDown(1))
        {
        }

        attackTimer += Time.deltaTime;
        if(anim.GetBool("isAttacking") && attackTimer > attackWaitTime)
        {
            anim.SetBool("isAttacking", false);
        }
    }

    void initialize()
    {
        int t = Mathf.RoundToInt(Mathf.Pow(level, 2f));
        if(isBoss)
            setHealth(200 * (1 + t * 5));
        else
            setHealth(5*(1+t*3));
        currentHealth = maxHealth;
        isDestroy = false;
        Vector3 pos = new Vector3(transform.position.x, transform.position.y -400f, transform.position.z);
        print("level "+ level);
        /*
        healthBar = Instantiate(healthBar, pos, Quaternion.identity);
        healthBar.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        healthBar.gameObject.SetActive(true);
        healthBar.gameObject.transform.localScale = new Vector2(2,2);
        healthBar.SetMaxHealth((int)maxHealth);
        healthBar.SetHealth((int)currentHealth);
        */
        //print("spawn floaty");
        //SpawnBubble();
        
        destroyTimer = 1f;
        exp = 2 + level;
        canAttackPlayer = false;
        attackTimer = 0;
        attackWaitTime = 1f;
        print("health floaty" + currentHealth);
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        //currentHealth -= damage;
        //print(damage);
    }

    public override void setHealth(float h)
    {
        maxHealth = h;
    }
    public override void destroy()
    {
        if(destroyTimer >= 0)
        {
            destroyTimer -= Time.deltaTime;
        }
        else
        {
            base.destroy();
            isDestroy = true;
            Destroy(gameObject);
        }
    }
    public override void remove()
    {
        base.remove();
    }
    public override void SpawnBubble()
    {
        //bool isDream = Random.Range(0, 100) < 50;
        dreamChance = Random.Range(0, 100);
        if (dreamChance < 20)
        {
            bubble1 = BubbleScript.Create(new Vector3(transform.position.x - 1f, transform.position.y + 1.5f), 1);
            bubble2 = BubbleScript.Create(new Vector3(transform.position.x + 1.5f, transform.position.y + 1.2f), 2);
            //print("bubble");

        }else if(dreamChance < 50)
        {
            bubble1 = BubbleScript.Create(new Vector3(transform.position.x - 1f, transform.position.y + 1.5f), 1);
        }
    }
    public override int getExp()
    {
        return base.getExp();
    }
    public override bool CanAttackPlayer()
    {
        if(attackTimer > 3 && currentHealth > 0)
        {
            canAttackPlayer = true;
        }
            
        return base.CanAttackPlayer();
    }
    public override void attackPlayer(Vector3 popPos)
    {
        base.attackPlayer(popPos);
        attackTimer = 0;
        anim.SetBool("isAttacking", true);
        if (isBoss)
            anim.SetInteger("idAttack", 2);
        else
            anim.SetInteger("idAttack", 1);

        int t = Mathf.RoundToInt(Mathf.Pow(level, 1.75f) * 5 * 0.015f);

        float i = Random.Range(0, 100f);
        if (i < t * 2)
        {
            damageToPlayer = 10 + t * 2;
        }
        else
        {
            damageToPlayer = 5 + t;
        }
        /*
        if (isBoss)
            damageToPlayer = 25 * (1 + t);
        else
            damageToPlayer = 5 * (1 + t) ;
        */
        canAttackPlayer = false;

        //popPos = new Vector3(popPos.x, popPos.y, popPos.z);
        //DamagePopupScript.Create(popPos, getDamageToPlayer(), 1);

        //print(getAttackDamage());
        Vector3 effectPos = new Vector3(transform.position.x, transform.position.y - 5f, transform.position.z);
        if(isBoss)
            SlashEffectScript.Create(effectPos,4);
        else
            SlashEffectScript.Create(effectPos, 2);
    }
    public override int getDamageToPlayer()
    {
        return base.getDamageToPlayer();
    }
}
