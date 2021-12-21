using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossButtonScript : MonoBehaviour
{
    [SerializeField] public CameraAnchor anchor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void clickEvent()
    {
        anchor.anchorOffset = new Vector3(0, 1, 0);
    }
    public void setBack()
    {
        anchor.anchorOffset = new Vector3(0, -1, 0);
    }
}
