using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    private Animator anim;
    private int id;
    private float time = 1;
    private bool des = false;
    private Vector3 position;
    private Vector3 _velocity = Vector3.zero;
    public static ItemScript Create(Vector3 pos)
    {
        Transform itemTrans = Instantiate(GameAssetsScript.i.item, pos, Quaternion.identity) as Transform;

        ItemScript item = itemTrans.GetComponent<ItemScript>();
        return item;
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        RandomItem();
    }

    // Update is called once per frame
    void Update()
    {
        if(des && time > 0)
        {
            time -= Time.deltaTime;
            transform.position = Vector3.SmoothDamp(transform.position, position, ref _velocity, Time.deltaTime * Random.Range(7, 11));
        } else if(des && time < 0)
        {
            Destroy(gameObject);
        }
    }
    private void RandomItem()
    {
        id = Random.Range(1, 100);
        //print(i);
        if(id < 0)
        {
            anim.SetInteger("id", 2);
        }
        else 
        {
            anim.SetInteger("id", 1);
        }
    }
    public void destroy(Vector3 pos)
    {
        position = pos;
        des = true;
        print("destroy" + getId());
        //Destroy(gameObject);
    }
    public void remove()
    {
        Destroy(gameObject);
        print("remove item");
        //Destroy(gameObject);
    }
    public int getId()
    {
        return 1;
    }
}
