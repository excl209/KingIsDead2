using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SignWork : MonoBehaviour
{
    [SerializeField] List<GameObject> textMessages;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject myMessage = Instantiate(textMessages[0], transform.position + new Vector3(0f, 1f), Quaternion.identity);
        TextMeshPro[] messages = myMessage.GetComponentsInChildren<TextMeshPro>();
        string signPostName = gameObject.name;
        Debug.Log("Signpost: " + signPostName);
        switch (signPostName)
        {
            case "Sign": messages[0].enabled = true; break;
            case "Sign1": messages[1].enabled = true; break;
            case "Sign2": messages[2].enabled = true; break;
            case "Sign3": messages[3].enabled = true; break;
            case "Sign4": messages[4].enabled = true; break;
            case "Sign5": messages[5].enabled = true; break;
            case "Sign6": messages[6].enabled = true; break;
            case "Sign7": messages[7].enabled = true; break;
        }
        Destroy(myMessage, 5f);
    }
}
