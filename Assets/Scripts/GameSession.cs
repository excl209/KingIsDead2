using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    
    [SerializeField] GameObject TombStone;
    [SerializeField] List<Sprite> tombStones;
    [SerializeField] int goldHighScore;
    

    void Awake()
    {
        SetupSingleton();
    }

    private void SetupSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlaceATombStone(Vector2 whereDidIDie)
    {
        var randomNum = Random.Range(0, 7);
        Transform me = FindObjectOfType<GameSession>().gameObject.transform;
        GameObject tomb = Instantiate(TombStone, whereDidIDie, Quaternion.identity);
        tomb.transform.parent = me.transform;
        tomb.GetComponent<SpriteRenderer>().sprite = tombStones[randomNum];
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    public void SetHighScore(int highScore)
    {
        if (goldHighScore <= highScore)
            goldHighScore = highScore;
    }

    
}
