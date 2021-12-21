using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroplootScript : MonoBehaviour
{
    public Transform Target;
    public float MinModifier = 0.7f;
    public float MaxModifier = 1.1f;
    static Vector3 pos;

    Vector3 _velocity = Vector3.zero;
    bool _isfollowing = false;
    float time = 3;

    /*
    public static DroplootScript Create(Vector3 popPos, Vector3 player)
    {
        pos = player;
        Transform popup = Instantiate(GameAssetsScript.i.droploot, popPos, Quaternion.identity) as Transform;
        DroplootScript droploot = popup.GetComponent<DroplootScript>();
        return droploot;
    }
    */
    
    public void StartFollowing()
    {
        _isfollowing = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        time -= Time.deltaTime;
        if (_isfollowing && time<0)
        {
            print(time);
            transform.position = Vector3.SmoothDamp(transform.position, Target.position, ref _velocity, Time.deltaTime * Random.Range(MinModifier, MaxModifier));
            //Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), Target.position, 3f * Time.deltaTime);
        }
        
    }
}
