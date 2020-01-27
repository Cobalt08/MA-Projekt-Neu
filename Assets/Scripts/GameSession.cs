using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{

    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;
    

    [SerializeField] Text livesText;
    [SerializeField] Text scoreText;

    public GameObject PortalTic;
    public GameObject PlayerTic;
    public GameObject PortalArc;
    public GameObject PlayerArc;

    public Checkpoint CheckPoint1;
    public Checkpoint CheckPoint2;
    public Checkpoint CheckPoint3;
    public Checkpoint CheckPoint4;
    public Checkpoint CheckPoint5;

    public AudioSource Audio1;
    public AudioSource Audio2;
    public AudioSource Audio3;
    public AudioSource Audio4;
    public AudioSource Audio5;






    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        livesText.text = "Lives: " + playerLives.ToString();
        scoreText.text = "Score: " + score.ToString();
        
    }

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = "Score: " + score.ToString();
    }

    public void AddToLives(int pointsToAdd)
    {
        playerLives += pointsToAdd;
        livesText.text = "Life: " + playerLives.ToString();
    }




    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    private void TakeLife()
    {
        playerLives--;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = "Lives: " + playerLives.ToString();
        PlayerTic.transform.position = new Vector2(PortalTic.transform.position.x, PortalTic.transform.position.y);
        PlayerArc.transform.position = new Vector2(PortalArc.transform.position.x, PortalArc.transform.position.y);
    }

    public void AudioCheck()
    {
        if (CheckPoint1.isActive)
        {
            Audio1.gameObject.SetActive(true);
            Audio2.gameObject.SetActive(false);
            Audio3.gameObject.SetActive(false); 
            Audio4.gameObject.SetActive(false);
            Audio5.gameObject.SetActive(false);
        }

        if (CheckPoint2.isActive)
        {
            Audio1.gameObject.SetActive(false);
            Audio2.gameObject.SetActive(true);
            Audio3.gameObject.SetActive(false);
            Audio4.gameObject.SetActive(false);
            Audio5.gameObject.SetActive(false);
        }

        if (CheckPoint3.isActive)
        {
            Audio1.gameObject.SetActive(false);
            Audio2.gameObject.SetActive(false);
            Audio3.gameObject.SetActive(true);
            Audio4.gameObject.SetActive(false);
            Audio5.gameObject.SetActive(false);
        }
        if (CheckPoint4.isActive)
        {
            Audio1.gameObject.SetActive(false);
            Audio2.gameObject.SetActive(false);
            Audio3.gameObject.SetActive(false);
            Audio4.gameObject.SetActive(true);
            Audio5.gameObject.SetActive(false);
        }
        if (CheckPoint5.isActive)
        {
            Audio1.gameObject.SetActive(false);
            Audio2.gameObject.SetActive(false);
            Audio3.gameObject.SetActive(false);
            Audio4.gameObject.SetActive(false);
            Audio5.gameObject.SetActive(true);
        }
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
