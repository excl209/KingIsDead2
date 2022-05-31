using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    TextMeshProUGUI[] healthText;
    PlayerMovement player;

    void Start()
    {
        healthText = GetComponentsInChildren<TextMeshProUGUI>();
        player = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        healthText[0].text = player.GetGold().ToString();
        healthText[1].text = player.GetLives().ToString();
        healthText[2].text = player.GetLevel().ToString();
    }
}
