using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    private TMP_Text popups;

    private void Awake()
    {
        popups = GetComponent<TMP_Text>();
    }

    public void PopupMyMessage(string message)
    {
        popups.text = message;
        string lastTxt = message.Substring(message.Length - 1);
        switch (lastTxt)
        {
            case "e": popups.color = Color.red; break;
            case "h": popups.color = Color.green; break;
            case "d": popups.color = Color.yellow; break;
            default: popups.color = Color.red; break;
        }
        GetComponent<Rigidbody2D>().velocity = Vector2.up * 0.1f;
        Destroy(gameObject, 2f);
    }

}
