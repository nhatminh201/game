using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] public PlayerScript player;
    public CameraAnchor anchor;
    public Camera camera;
    public EnemyScript enemy;
    public BubbleScript bubble1, bubble2;
    public ItemScript item1, item2, item3;
    public playButtonScript playButton;
    public CancelButtonScript cancelButton;
    public noticeScript notice;
    [SerializeField] public GameObject spawnEnemy;
    [SerializeField] public HealthBarScript enemyHealthBar;

    [SerializeField] private WeaponButtonScript weaponButton;
    [SerializeField] private BossButtonScript bossButton;
    [SerializeField] private InstructionScript instructionButton;


    [SerializeField] public AdsButtonScript adsButton;
    [SerializeField] public AudioSource backgroundMusic;
    private bool isMusicOn, isSoundOn;


    public Text bannerLevel;
    //public Text stoneNumber;
    //public Text swordLevel, swordNextLevel, successUpgradePercent;
    //public GameObject upgradeWeapon; 


    private float camPos;
    private float incre;
    private bool startSpawn, isFightBoss, isPlaying;

    private int dreamChance, itemNo;

    private Vector3 _velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        //camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y + 1.5f, camera.transform.position.z);

        //print(camera.transform.position.y);


        //Vector3 position = new Vector3(popPos.transform.position.x,
        //    popPos.transform.position.y, popPos.transform.position.z);
        /*
        Transform popup = Instantiate(damagePopup, position, Quaternion.identity) as Transform;
        popup.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);

        DamagePopupScript damagepopup = popup.GetComponent<DamagePopupScript>();
        damagepopup.Setup(20);
        */
        incre = 0;
        startSpawn = false;
        isFightBoss = false;
        isPlaying = false;
        camPos = camera.transform.position.y;

        isMusicOn = true;
        isSoundOn = true;


        backgroundMusic.Play();

        instructionButton.show();
        //upgradeWeapon.GetComponent<Button>().onClick.AddListener(player.upgradeWeapon);

        //SoundManager.PlaySound(SoundManager.Sound.BackgroundMusic, true, 0.1f, getIsMusicOn());
        //skillMenu.setManager(this);

        //skillMenu.getChooseSkill().GetStrengthSkill().setUp(player.getStrengthSkill1Level(), 0, 0);
        //skillMenu.getChooseSkill().GetStrengthSkill().strengButton1.GetComponent<Button>().onClick.AddListener(EventStrengthSkill1);
    }
    // Update is called once per frame
    void Update()
    {
        //print("cam pos: " + camera.transform.position.y);
        //SPAWN ENEMY AND ITEM
        if (startSpawn)
        {
            enemy = EnemyScript.Create(spawnEnemy.transform.position, player.getLevel(), false, 0 , 3);
            enemyHealthBar.gameObject.SetActive(true);
            enemyHealthBar.SetMaxHealth(enemy.getMaxHealth());
            enemyHealthBar.SetHealth(enemy.getCurrentHealth());

            startSpawn = false;
            print("start spawn");

            spawnItem();
            //SoundManager.PlaySound(SoundManager.Sound.SleepDestroy, false, 0.5f, getIsSoundOn());
        }
        //CAMERA START MOVE
        /*
        if (isPlaying && camera.transform.position.y != -4.1f)
        {
            camera.transform.position = new Vector3(camera.transform.position.x, -4.1f);
        }
        if(!isPlaying && camera.transform.position.y != -5.6f)
        {
            camera.transform.position = new Vector3(camera.transform.position.x, -5.6f);
        }
        */
        //print(camera.transform.position.y);
        /*
        incre += 1f * 5f * Time.deltaTime;
        if (isPlaying && camera.transform.position.y < -4f)
        {
            camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y + incre, camera.transform.position.z);
        }else if(!isPlaying && camera.transform.position.y > -5.6f)
        {
            camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y - incre, camera.transform.position.z);
        }
        else
        {
            incre = 0;
        }*/
        //TOUCH SCREEN
        if (Input.GetMouseButtonDown(1))
        {
            
            if (player.canAttack() && isPlaying)
            {
                player.attack();
                enemy.TakeDamage(player.getAttackDamage());
                enemyHealthBar.SetHealth(enemy.getCurrentHealth());
                //DroplootScript.Create(item1.transform.position, player.transform.position);
            }
            
        }
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                if (player.canAttack() && isPlaying)
                {
                    player.attack();
                    enemy.TakeDamage(player.getAttackDamage());
                    enemyHealthBar.SetHealth(enemy.getCurrentHealth());
                    
                    //DroplootScript.Create(item1.transform.position, player.transform.position);
                }
            }
        }
        
        //SET UP ITEM/ LEVEL
        bannerLevel.text = player.getLevel().ToString();
        /*
        stoneNumber.text = player.getStoneNumber().ToString();
        swordLevel.text = player.getWeaponLevel().ToString();
        int nextlv = player.getWeaponLevel() + 1;
        if(nextlv<=10)
            swordNextLevel.text = nextlv.ToString();
        else
            swordNextLevel.text ="MAX";
        successUpgradePercent.text = player.getSuccessUpgradePercent().ToString() + "%";
        */
        //ENEMY ATTACK
        if (enemy.CanAttackPlayer())
        {
            enemy.attackPlayer(player.transform.position);
            player.TakeDamage(enemy.getDamageToPlayer());
            enemy.TakeDamage(player.getAttackDamage());
            enemyHealthBar.SetHealth(enemy.getCurrentHealth());
        }

        //CHECK EARN ITEM
        earnItem();

        //CHECK GAMEOVER
        gameOver();

    }
    //START BUTTON EVENT
    public void beginSpawn()
    {
        camPos = camera.transform.position.y + 1.5f;
        startSpawn = true;
        isPlaying = true;
    }
    //BOSS BUTTON EVENT
    public void spawnBoss()
    {
        //enemy.remove();
        //bubble1.pop();
        //item1.remove();
        //bubble2.pop();
        //item2.remove();
        if (!enemy.CanEarnExp())
        {
            enemy.remove();
            removeItem();
            enemy = EnemyScript.Create(spawnEnemy.transform.position, player.getLevel(), true, 3, 4);
            enemyHealthBar.SetMaxHealth(enemy.getMaxHealth());
            enemyHealthBar.SetHealth(enemy.getCurrentHealth());
            isFightBoss = true;
            spawnItem();
        }
    }
    //SPAWN ITEM
    private void spawnItem()
    {
        dreamChance = Random.Range(0, 100);
        if (dreamChance < 1f * player.getLevel() )
        {
            itemNo = 2;
            bubble1 = BubbleScript.Create(new Vector3(enemy.transform.position.x - 1f, enemy.transform.position.y + 1.5f), 1);
            bubble2 = BubbleScript.Create(new Vector3(enemy.transform.position.x + 1.5f, enemy.transform.position.y + 1.2f), 2);
            item1 = ItemScript.Create(new Vector3(enemy.transform.position.x - 1.15f, enemy.transform.position.y + 1.55f));
            item2 = ItemScript.Create(new Vector3(enemy.transform.position.x + 1.65f, enemy.transform.position.y + 1.25f));
            //print("dream chance" + dreamChance);

        }
        else if (dreamChance < 3f * player.getLevel() )
        {
            itemNo = 1;
            bubble1 = BubbleScript.Create(new Vector3(enemy.transform.position.x - 1f, enemy.transform.position.y + 1.5f), 1);
            item1 = ItemScript.Create(new Vector3(enemy.transform.position.x - 1.15f, enemy.transform.position.y + 1.55f));
            //print("dream chance" + dreamChance);
        }
        else
        {
            itemNo = 0;
            //print("dream chance" + dreamChance);
        }
    }
    public void rewardAdsItem()
    {
        item3 = ItemScript.Create(new Vector3(adsButton.transform.position.x, adsButton.transform.position.y));
        item3.destroy(player.transform.position);
    }

    //EARN ITEM
    private void earnItem()
    {
        if (enemy.CanEarnExp())
        {
            if (itemNo == 2)
            {
                if (item1.getId() == 1)
                {
                    //print("pop2");
                    player.earnStone();
                    bubble1.pop();
                    item1.destroy(player.transform.position);
                    player.earnStone();
                    bubble2.pop();
                    item2.destroy(player.transform.position);
                }
            }
            else if (itemNo == 1)
            {
                //print("can earn" + dreamChance);
                if (item1.getId() == 1)
                {
                    //print("pop1");
                    player.earnStone();
                    bubble1.pop();
                    item1.destroy(player.transform.position);
                }
            }

            print("item no" + itemNo);
            player.gainExp(enemy.getExp());
            SoundManager.PlaySound(SoundManager.Sound.SleepDestroy, false, 0.5f, getIsSoundOn());
            //enemy = EnemyScript.Create(spawnEnemy.transform.position);
            isFightBoss = false;
            startSpawn = true;
            if (isFightBoss)
            {
                isFightBoss = false;
                bossButton.setBack();
            }
            //SAVE ITEM
            player.SavePlayer();
            player.fullHeal();
        }
    }
    private void removeItem()
    {
        if (!enemy.CanEarnExp())
        {
            if (dreamChance < 1f * player.getLevel())
            {
                if (item1.getId() == 1)
                {
                    bubble1.pop();
                    item1.remove();
                    bubble2.pop();
                    item2.remove();
                }
            }
            else if (dreamChance < 3f * player.getLevel())
            {
                if (item1.getId() == 1)
                {
                    bubble1.pop();
                    item1.remove();
                }
            }
        }
    }
    //UPGRADE WEAPON NOTICE
    public void successUpgradeWeapon()
    {
        notice.clickEvent(2);
        SoundManager.PlaySound(SoundManager.Sound.SuccessUpgrade, false, 0.5f, true);
    }
    //GAME OVER
    public void gameOver()
    {
        if (player.getIsFaint())
        {
            cancelGame();
            cancelButton.setBack();

        }
    }
    //END GAME
    public void cancelGame()
    {
        if (!enemy.CanEarnExp())
        {
            enemy.remove();
            /*
            if (dreamChance < 2f * player.getLevel())
            {
                if (item1.getId() == 1)
                {
                    bubble1.pop();
                    item1.remove();
                    bubble2.pop();
                    item2.remove();
                }
            }
            else if (dreamChance < 5 * player.getLevel())
            {
                if (item1.getId() == 1)
                {
                    bubble1.pop();
                    item1.remove();
                }
            }
            */
            removeItem();
            player.reset();
            playButton.setBack();
            isFightBoss = false;
            //camera.transform.position = new Vector3(camera.transform.position.x, -5.6f, camera.transform.position.z);
            //incre = 0;
            //EVENT NOTICE
            notice.clickEvent(1);
            player.resetGame();
            isPlaying = false;
            SoundManager.PlaySound(SoundManager.Sound.Fail, false, 1, getIsSoundOn());
        }
    }
    

    //GETTER
    public PlayerScript GetPlayer()
    {
        return player;
    }
    public bool getIsPlaying()
    {
        return isPlaying;
    }
    public bool getIsMusicOn()
    {
        return isMusicOn;
    }
    public bool getIsSoundOn()
    {
        return isSoundOn;
    }
    //SETTER
    public void setMusicOnOff(bool b)
    {
        isMusicOn = b;
        backgroundMusic.mute = !isMusicOn;
        /*
        if (isMusicOn)
        {
            backgroundMusic.UnPause();
        }
        else
        {
            backgroundMusic.Pause();
        }
        */
        //SoundManager.PlaySound(SoundManager.Sound.BackgroundMusic, false, 0f, isMusicOn);
    }
    public void setSoundOnOff(bool b)
    {
        isSoundOn = b;
    }
    //SWITCH WORLD
    public void switchWorld()
    {
        if(player.getWeaponLevel() >= 1 && player.getEnhanceLevel() == 2 && !isFightBoss && isPlaying)
        {
            spawnBoss();
        }
        else
        {
            notice.clickEvent(3);
        }

    }
}
