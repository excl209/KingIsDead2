using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    TextMeshProUGUI[] instructionText;
    PlayerMovement player;

    void Awake()
    {
        instructionText = GetComponentsInChildren<TextMeshProUGUI>();

        Debug.Log("Endgame texts: " + instructionText.Length);

        player = FindObjectOfType<PlayerMovement>();

        bool winState = player.getEndGame();

        Debug.Log("Did I win?: " + winState);

        if (winState)
        {
            instructionText[1].enabled = true;
        }
    }

}
