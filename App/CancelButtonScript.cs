using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelButtonScript : MonoBehaviour
{
    public GameManager game;
    [SerializeField] public CameraAnchor anchor;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //print(transform.position.y);
    }

    public void cancelGame()
    {
        game.cancelGame();
    }
    public void clickEvent()
    {
        transform.position = new Vector3(transform.position.x, 1.5f);
        //anchor.anchorOffset = new Vector3(0, -1, 0);
    }
    public void setBack()
    {
        transform.position = new Vector3(transform.position.x, 4.5f);
        //anchor.anchorOffset = new Vector3(0, 3, 0);
        //cancelGame();
    }
}
