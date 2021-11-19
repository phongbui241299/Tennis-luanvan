using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    public string hitter;
    public Vector3 initialPos;
    public int playerScore;
    public int botScore;
    public CameraRotator cameraRotator;
   [SerializeField] Text playerscoreT;
   [SerializeField] Text botscoreT;
   [SerializeField] Text gameOverT;
   [SerializeField] Text winT;

    int scoreToWin = 40;

    void Start()
    {
        initialPos = transform.position;
        playerScore = 0;
        botScore = 0;
        gameOverT.enabled = false;
        winT.enabled = false;
        updateScores();
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Wall"))
        {
            //GameObject.Find("Player").GetComponent<Player>().Reset();

            if (hitter == "player")
            {
                botScore+=15;
                if (botScore == 45)
                {
                    botScore = 40;
                }
            }
            else if (hitter == "bot")
            {
                playerScore+=15;
                if (playerScore == 45)
                {
                    playerScore = 40;
                }

            }

            updateScores(); 
            resetBall();
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Out"))
        {
            if(hitter == "player")
            {
                botScore+=15;
                if (botScore==45)
                {
                    botScore = 40;
                }
            }
            else if(hitter == "bot")
            {
                playerScore+=15;
                if (playerScore == 45)
                {
                    playerScore = 40;
                }

            }

            updateScores();
            resetBall();
        }
        else if (other.CompareTag("InPlayer"))
        {
            if(hitter == "player")
            {
                botScore+=15;
                if (botScore == 45)
                {
                    botScore = 40;
                }
                updateScores();
                resetBall();
            }
            
            hitter = "player";
        }
        else if (other.CompareTag("InBot"))
        {
            if(hitter == "bot")
            {
                playerScore+=15;
                if (playerScore == 45)
                {
                    playerScore = 40;
                }
                updateScores();
                resetBall();
            }

            hitter = "bot";
        }
    }

    void updateScores()
    {
        playerscoreT.text = "Player: " + playerScore;
        botscoreT.text = "Bot: " + botScore;

        if(botScore > scoreToWin ) //finish game
        {
            cameraRotator.startRotate();
            gameOverT.enabled = true;
            gameObject.SetActive(false);
            playerscoreT.gameObject.SetActive(false);
            botscoreT.gameObject.SetActive(false);

        }
        else if(playerScore > scoreToWin)
        {
            cameraRotator.startRotate();
            winT.enabled = true;
            gameObject.SetActive(false);
            playerscoreT.gameObject.SetActive(false);
            botscoreT.gameObject.SetActive(false);
        }
        else if (playerScore == scoreToWin && botScore == scoreToWin)
        {
            gameOverT.enabled = false;
            gameObject.SetActive(true);
        }
    }

    private void resetBall()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        transform.position = initialPos;
        hitter = "";
    }
}
