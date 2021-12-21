using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    public GameObject itemTemplate;

    public GameObject content;
    // Start is called before the first frame update
    void Start()
    {
        add();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void add()
    {
        for(int i=0; i<3; i++)
        {

            GameObject copy = Instantiate(itemTemplate);
            int tg = i;
            copy.transform.GetChild(0).GetChild(0).GetComponent<Text>().text ="button" + tg;
            tg++;
            copy.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "button" + tg;
            tg++;
            copy.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = "button" + tg;
            //print(text);
            copy.transform.SetParent(content.transform, false);
            copy.transform.localPosition = Vector3.zero;
            copy.transform.localScale = new Vector3(1, 1, 1);
        }
        

        //copy.GetComponentInChildren<Text>().text = 
    }
}
