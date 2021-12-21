using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponMenuScript : MonoBehaviour
{
    [SerializeField] private GameManager game;
    [SerializeField] public CameraAnchor anchor;
    [SerializeField] public GameObject menu;

    [SerializeField] public Text stonesNumber, swordLevel, swordNextLevel, successUpgradePercent;
    [SerializeField] public Button upgradeButton;
    bool isOpen, move;
    // Start is called before the first frame update
    void Start()
    {
        upgradeButton.onClick.AddListener(game.GetPlayer().upgradeWeapon);
    }

    // Update is called once per frame
    void Update()
    {
        //SET UP ITEM/ LEVEL
        stonesNumber.text = game.GetPlayer().getStoneNumber().ToString();
        swordLevel.text = game.GetPlayer().getWeaponLevel().ToString();
        int nextlv = game.GetPlayer().getWeaponLevel() + 1;
        if (nextlv < 10)
            swordNextLevel.text = nextlv.ToString();
        else
        {
            swordNextLevel.text = "MAX";
            swordLevel.text = "MAX";
        }
        successUpgradePercent.text = game.GetPlayer().getSuccessUpgradePercent().ToString() + "%";

        /*
        if (game.getIsPlaying())
        {
            transform.position = new Vector3(transform.position.x, -4.1f);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, -5.6f);
        }
        */
        //print(transform.position.x);
    }
    public void clickEvent()
    {
        if (!isOpen)
        {
            //anchor.anchorOffset = new Vector3(-1.75f, 0f, 0);
            //transform.position = new Vector3(2.0f, transform.position.y, transform.position.z);
            transform.position = new Vector3(menu.transform.position.x - 1.5f, transform.position.y, transform.position.z);
            isOpen = true;
        }
        else
        {
            isOpen = false;
            transform.position = new Vector3(5.5f, transform.position.y, transform.position.z);
            //anchor.anchorOffset = new Vector3(1, 0f, 0);
        }
    }
    public void setBack()
    {
        isOpen = false;
        transform.position = new Vector3(5.5f, transform.position.y, transform.position.z);
        //anchor.anchorOffset = new Vector3(1, 0f, 0);
    }
}
