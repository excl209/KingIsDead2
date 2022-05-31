using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    TextMeshProUGUI[] instructionText;
    Button[] buttons;
    Image[] images;
    

    void Start()
    {
        instructionText = GetComponentsInChildren<TextMeshProUGUI>();
        buttons = GetComponentsInChildren<Button>();
        images = GetComponentsInChildren<Image>();
    }

    public void ReadInstructions(int page)
    {
        SwitchMain(false);
        if(page == 1)
        {
            instructionText[page + 3].enabled = true;

            //continue button
            instructionText[5].enabled = true;
            buttons[3].enabled = true;
            images[3].enabled = true;
        }

        if (page == 2)
        {
            instructionText[page + 4].enabled = true;
            instructionText[page + 2].enabled = false;

            instructionText[5].enabled = false;
            buttons[3].enabled = false;
            images[3].enabled = false;

            //back to main menu
            instructionText[9].enabled = true;
            buttons[5].enabled = true;
            images[5].enabled = true;
        }

        if (page == 3)
        {
            instructionText[page + 4].enabled = true;
            instructionText[page + 3].enabled = false;

            //back to main menu
            instructionText[9].enabled = false;
            buttons[5].enabled = false;
            images[5].enabled = false;
        }

        //back to main menu
        instructionText[8].enabled = true;
        buttons[4].enabled = true;
        images[4].enabled = true;
    }

    public void BacktoMainMenu()
    {
        SwitchMain(true);

        instructionText[4].enabled = false;
        instructionText[5].enabled = false;
        instructionText[6].enabled = false;
        instructionText[7].enabled = false;
        buttons[3].enabled = false;
        images[3].enabled = false;

        //2nd continue button
        instructionText[9].enabled = false;
        buttons[5].enabled = false;
        images[5].enabled = false;

        //back to main menu
        instructionText[8].enabled = false;
        buttons[4].enabled = false;
        images[4].enabled = false;
    }

    private void SwitchMain(bool switchy)
    {
        instructionText[0].enabled = switchy;
        instructionText[1].enabled = switchy;
        instructionText[2].enabled = switchy;
        instructionText[3].enabled = switchy;
        buttons[0].enabled = switchy;
        buttons[1].enabled = switchy;
        buttons[2].enabled = switchy;
        images[0].enabled = switchy;
        images[1].enabled = switchy;
        images[2].enabled = switchy;
    }

    public void DisplayEndGame()
    {
        instructionText[10].enabled = true;
    }
}
