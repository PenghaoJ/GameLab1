using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public Text scoreText;
    private int score = 0;
    public Text capacityText;
    public GameObject winPanel;
    public GameObject gameOverPanel;
    public GameObject persons;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Win()
    {
        //pause time
        Time.timeScale = 0;
        //Show game over screen
        winPanel.SetActive(true);
    }
    public void GameOver()
    {
        //pause time
        Time.timeScale = 0;
        //Show game over screen
        gameOverPanel.SetActive(true);
    }
    public void StartGame()
    {

    }
    public void SetCount(int a)
    {
        capacityText.text = $"Soldiers In Helicopter:{a}";
    }
    public void AddScore(int a)
    {
        score += a;
        scoreText.text = $"Soldiers Rescued:{score}";

        //Determine if all soldiers are already out of the display and if so, win the game.
        int tmp = 0;
        for (int i = 0; i < persons.transform.childCount; i++)
        {
            Transform t = persons.transform.GetChild(i);
            if (!t.gameObject.active)
            {
                tmp++;
            }
        }
        if (tmp >= persons.transform.childCount)
        {
            Win();
        }
    }
    public void Reset()
    {
        capacityText.text = $"Soldiers In Helicopter:{0}";
        scoreText.text = $"Soldiers Rescued:{0}";
        score = 0;
        //time recovery
        Time.timeScale = 1;
        //Hide the game over screen
        gameOverPanel.SetActive(false);
        //Hide Victory Panel
        winPanel.SetActive(false);
        //Facilitates the activation of all Persons below the game object if it is a Soldier.
        for (int i = 0;i < persons.transform.childCount;i++)
        {
            Transform t = persons.transform.GetChild(i);
            if (t.tag == "Soldier")
            {
               t.gameObject.SetActive(true);
            }
            
        }
        
    }
}
