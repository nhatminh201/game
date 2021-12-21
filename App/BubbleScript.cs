using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleScript : MonoBehaviour
{
    private static int no;
    private static ItemScript item;
    private int id;
    public static BubbleScript Create(Vector3 pos, int num)
    {
        //no = num;
        Transform bubbleTrans = Instantiate(GameAssetsScript.i.bubble, pos, Quaternion.identity) as Transform;
        if (num == 2)
        {
            Vector3 flip = bubbleTrans.transform.localScale;
            flip.x *= -1;
            bubbleTrans.transform.localScale = flip;
            //item = ItemScript.Create(new Vector3(pos.x + .15f, pos.y + .05f));
            //print(num);
        }
        else
        {
            //item = ItemScript.Create(new Vector3(pos.x - .15f, pos.y + .05f));
            //print(num);
        }

        BubbleScript bubble = bubbleTrans.GetComponent<BubbleScript>();
        return bubble;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //id = item.getId();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void pop()
    {
        //item.remove();
        Destroy(gameObject);
    }
    public int getId()
    {
        return id;
    }
}
