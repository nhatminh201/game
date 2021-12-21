using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollowCameraScript : MonoBehaviour
{
    [SerializeField] GameManager game;
    private bool move;
    // Start is called before the first frame update
    void Start()
    {
        move = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (game.getIsPlaying() && !move)
        {
            clickEvent();
            move = true;
        }
        else if(!game.getIsPlaying() && move)
        {
            setBack();
            move = false;
        }
        print(transform.position.y);
    }
    public void clickEvent()
    {
        transform.position = new Vector3(transform.position.x, 3.08f);
        //anchor.anchorOffset = new Vector3(0, 1.5f, 0);
    }
    public void setBack()
    {
        transform.position = new Vector3(transform.position.x, 1.42f);
        //anchor.anchorOffset = new Vector3(0, -5, 0);
    }
}

