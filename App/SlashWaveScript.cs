using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashWaveScript : MonoBehaviour
{
    private float existTime;
    private Animator anim;
    private static int formno;
    private static int idno;
    // Start is called before the first frame update
    public static SlashWaveScript Create(Vector3 popPos, int form, int id)
    {
        formno = form;
        idno = id;
        Transform effect = Instantiate(GameAssetsScript.i.slashWave, popPos, Quaternion.identity) as Transform;
        
        SlashWaveScript slashWave = effect.GetComponent<SlashWaveScript>();
        return slashWave;
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        existTime = .5f;


        //anim.SetInteger("form", formno);
        //anim.SetInteger("id", idno);
        
        switch (formno)
        {
            case 4:
                anim.SetInteger("form", 4);
                break;
            case 3:
                anim.SetInteger("form", 3);
                break;
            case 2:
                anim.SetInteger("form", 2);
                break;
            case 1:
            default:
                anim.SetInteger("form", 1);
                break;
        }

        switch (idno)
        {
            case 2:
                anim.SetInteger("id", 2);
                break;
            case 1:
            default:
                anim.SetInteger("id", 1);
                break;
        }
        Debug.Log("id: " + idno + "form: " + formno);
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
