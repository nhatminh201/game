using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopupScript : MonoBehaviour
{
    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;

    private const float DISAPPEAR_TIMER_MAX = 1f;
    private Vector3 moveVector;
    
    public static DamagePopupScript Create(Vector3 popPos, int damageAmount, int type)
    {
        Transform popup = Instantiate(GameAssetsScript.i.damagePopup, popPos, Quaternion.identity) as Transform;
        popup.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        popup.transform.position = popPos;

        DamagePopupScript damagepopup = popup.GetComponent<DamagePopupScript>();
        damagepopup.Setup(damageAmount, type);

        return damagepopup;
    }
    

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void Setup(int damage, int type)
    {
        textMesh.SetText(damage.ToString());
        Color color;
        switch (type)
        {
            case 3:
                textMesh.fontSize = 60;
                ColorUtility.TryParseHtmlString("#00FF0C", out color);
                break;
            case 2:
                textMesh.fontSize = 88;
                ColorUtility.TryParseHtmlString("#FF3000", out color);
                break;
            case 1:
            default:
                textMesh.fontSize = 60;
                ColorUtility.TryParseHtmlString("#FFBA00", out color);
                break;
        }
        /*
        if (!isCriticalHit)
        {
            textMesh.fontSize = 60;
            ColorUtility.TryParseHtmlString("#FFBA00", out color);
        }
        else
        {
            textMesh.fontSize = 88;
            ColorUtility.TryParseHtmlString("#FF3000", out color);
        }
        */

        //textColor = textMesh.color;
        //textMesh.color = textColor;
        //textMesh.color = color;
        textMesh.outlineColor = color;
        textColor = textMesh.color;
        //textMesh. = textColor;
        disappearTimer = DISAPPEAR_TIMER_MAX;

        moveVector = new Vector3(0f, .5f) * 4f;
    }

    private void Update()
    {
        //float speed = 2f;
        transform.position += moveVector * Time.deltaTime;
        //moveVector -= moveVector * 18f * Time.deltaTime;

        if (disappearTimer > DISAPPEAR_TIMER_MAX * .5f)
        {
            float increaseScaleAmount = .5f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else
        {
            float decreaseScaleAmount = 5f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }
        disappearTimer -= Time.deltaTime;
        if(disappearTimer < 0)
        {
            float disappearSpeed = 10f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if(textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
