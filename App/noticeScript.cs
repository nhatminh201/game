using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noticeScript : MonoBehaviour
{
    public GameManager game;
    [SerializeField] public CameraAnchor anchor;
    [SerializeField] public GameObject rednotice;
    [SerializeField] public GameObject greennotice;
    [SerializeField] public GameObject yellownotice;

    bool trigger;
    float posX, timer;
    // Start is called before the first frame update
    void Start()
    {
        posX = 0;
        timer = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger)
        {
            timer -= Time.deltaTime;
            if (timer > 2 && transform.position.x > 3.5f)
            {
                transform.position = new Vector3(transform.position.x - Time.deltaTime*30, transform.position.y);
            }
            else if(timer < 1 && transform.position.x < 9.5f)
            {
                transform.position = new Vector3(transform.position.x + Time.deltaTime*60, transform.position.y);
            }
            if (timer <= 0)
            {
                trigger = false;
                timer = 3;
                greennotice.SetActive(false);
                rednotice.SetActive(false);
                yellownotice.SetActive(false);
            }
        }
        //print(transform.position.x);
    }

    
    public void clickEvent(int i)
    {
        trigger = true;
        switch (i)
        {
            case 3:
                yellownotice.SetActive(true);
                break;
            case 2:
                greennotice.SetActive(true);
                break;
            case 1:
                rednotice.SetActive(true);
                break;
        }
    }

}
