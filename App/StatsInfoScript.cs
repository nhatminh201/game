using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsInfoScript : MonoBehaviour
{
    [SerializeField] private GameManager game;
    [SerializeField] public CameraAnchor anchor;
    [SerializeField] private GameObject statsInfo;
    private float incre;
    bool isOpen;
    // Start is called before the first frame update
    void Start()
    {
        statsInfo.transform.GetChild(0).GetChild(0).GetComponent<Text>().fontSize = 48;
        statsInfo.transform.GetChild(1).GetChild(0).GetComponent<Text>().fontSize = 48;
        statsInfo.transform.GetChild(2).GetChild(0).GetComponent<Text>().fontSize = 48;

        incre = 0;
    }

    // Update is called once per frame
    void Update()
    {
        statsInfo.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Attack: " + game.GetPlayer().getBaseAttack().ToString();
        statsInfo.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "Speed: " + game.GetPlayer().getAttackSpeed().ToString();
        statsInfo.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = "Health: " + game.GetPlayer().getMaxHP().ToString();

       
        /*
        //incre += Time.deltaTime;
        if (game.getIsPlaying() && transform.position.y < -10.5f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + incre, transform.position.z);
            //move = true;
        }
        else if (!game.getIsPlaying() && transform.position.y > -12.35f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - incre, transform.position.z);
            //move = false;
        }
        else
        {
            incre = 0;
        }
        */
        //print(transform.position.x);
    }
    public void clickEvent()
    {
        SoundManager.PlaySound(SoundManager.Sound.UIClick, false, 1, game.getIsSoundOn());
        if (!isOpen)
        {
            transform.position = new Vector3(0, transform.position.y);
            //anchor.anchorOffset = new Vector3(0, 1.2f, 0);
            isOpen = true;
        }
        else
        {
            transform.position = new Vector3(9.5f, transform.position.y);
            isOpen = false;
            //anchor.anchorOffset = new Vector3(0, -1.2f, 0);
        }
    }
    public void setBack()
    {
        transform.position = new Vector3(9.5f, transform.position.y);
        isOpen = false;
        //anchor.anchorOffset = new Vector3(0, -1.2f, 0);
    }
}
