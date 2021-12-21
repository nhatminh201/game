using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionScript : MonoBehaviour
{
    [SerializeField] public GameManager game;
    [SerializeField] GameObject[] instructions;
    private int i;
    // Start is called before the first frame update
    void Start()
    {
        i = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void show()
    {
        SoundManager.PlaySound(SoundManager.Sound.UIClick, false, 1, game.getIsSoundOn());
        if (i>0)
        instructions[i-1].SetActive(false);
        if(i<5)
        instructions[i].SetActive(true);
        i++;
        if (i == 6) i = 0;
    }
}
