using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityInfoScript : MonoBehaviour
{
    [SerializeField] private GameManager game;
    [SerializeField] public CameraAnchor anchor;
    [SerializeField] private GameObject abilityInfo;
    [SerializeField] private GameObject strengthInfo;
    [SerializeField] private GameObject agilityInfo;
    [SerializeField] private GameObject defenseInfo;
    bool isOpen;

    //public float onY, offY;
    // Start is called before the first frame update
    void Start()
    {
        //onY = 1.2f;
        //offY = -1.2f;
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(game.GetPlayer().getChooseStr() || game.GetPlayer().getChooseAgi() || game.GetPlayer().getChooseDef())
        {
            abilityInfo.SetActive(false);
        }
        else
        {
            abilityInfo.SetActive(true);
            strengthInfo.SetActive(false);
            agilityInfo.SetActive(false);
            defenseInfo.SetActive(false);
        }


        if (game.GetPlayer().getChooseStr())
        {
            strengthInfo.SetActive(true);
        }
        else if (game.GetPlayer().getChooseAgi())
        {
            agilityInfo.SetActive(true);
        }
        else if (game.GetPlayer().getChooseDef())
        {
            defenseInfo.SetActive(true);
        }
        //print(transform.position.y);
        */
    }
    public void eventChooseStr()
    {
        abilityInfo.SetActive(false);
        strengthInfo.SetActive(true);
        //
    }
    public void eventChooseAgi()
    {
        abilityInfo.SetActive(false);
        agilityInfo.SetActive(true);
        //
    }
    public void eventChooseDef()
    {
        abilityInfo.SetActive(false);
        defenseInfo.SetActive(true);
        //
    }
    public void clickEvent()
    {
        if (!isOpen)
        {
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
            //transform.position = new Vector3(2.0f, transform.position.y, transform.position.z);
            //anchor.anchorOffset = new Vector3(-1.75f, 0f, 0f);
            isOpen = true;
        }
        else
        {
            abilityInfo.SetActive(true);
            strengthInfo.SetActive(false);
            agilityInfo.SetActive(false);
            defenseInfo.SetActive(false);
            transform.position = new Vector3(9.5f, transform.position.y, transform.position.z);
            isOpen = false;
            //anchor.anchorOffset = new Vector3(3f, -0f, 0f);
        }
    }
    public void setBack()
    {
        abilityInfo.SetActive(true);
        strengthInfo.SetActive(false);
        agilityInfo.SetActive(false);
        defenseInfo.SetActive(false);

        //
        transform.position = new Vector3(9.5f, transform.position.y, transform.position.z);
        isOpen = false;
        //anchor.anchorOffset = new Vector3(0, -1.2f, 0);
    }
}
