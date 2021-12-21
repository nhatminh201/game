using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashEffectScript : MonoBehaviour
{
    private static int idEffect;
    private float existTime;

    private Animator anim;

    public static SlashEffectScript Create(Vector3 popPos, int id)
    {
        idEffect = id;
        Transform effect = Instantiate(GameAssetsScript.i.slashEffect, popPos, Quaternion.identity) as Transform;
        if(id < 2)
            effect.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 180));

        SlashEffectScript slashEffect = effect.GetComponent<SlashEffectScript>();
        return slashEffect;
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        existTime = .2f;
        anim.SetInteger("id", idEffect);
    }

    // Update is called once per frame
    void Update()
    {
        existTime -= Time.deltaTime;
        if (existTime < 0)
        {
            //print("exist");
            Destroy(gameObject);
        }
    }
}
