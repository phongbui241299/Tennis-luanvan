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
   [SerializeField] Text playerTextAlert;
   [SerializeField] Text botscoreT;
   [SerializeField] Text boTextAlert;
   [SerializeField] Text gameOverT;
   [SerializeField] Text outText;
   [SerializeField] Text winT;

    int scoreToWin = 100;
    public float time = 3;
    public GameObject ball;
   

    void Start()
    {
        initialPos = transform.position;
        playerScore = 0;
        botScore = 0;
        gameOverT.enabled = false;
        playerTextAlert.enabled = false;
        boTextAlert.enabled = false;
        outText.enabled = false;
        winT.enabled = false;
        updateScores();
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Wall"))
        {
            
            

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
            outText.enabled = true;
            playerTextAlert.enabled = true;
            boTextAlert.enabled = true;
            cameraRotator.startRotate();
            StartCoroutine(StartTime());
            updateScores(); 
            resetBall();
            
        }
    }

    IEnumerator StartTime()
    {
        yield return new WaitForSeconds(time);
        outText.enabled = false;
        playerTextAlert.enabled = false;
        boTextAlert.enabled = false;
        cameraRotator.stopRotate();
      
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Out"))
        {
          
           

            if (hitter == "player")
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
            outText.enabled = true;
            playerTextAlert.enabled = true;
            boTextAlert.enabled = true;
            cameraRotator.startRotate();
            StartCoroutine(StartTime());
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
                outText.enabled = false;
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
                outText.enabled = false;
             
                updateScores();
                resetBall();
            }

            hitter = "bot";
        }
    }



    void updateScores()
    {
        playerscoreT.text = "Player: " + playerScore;
        playerTextAlert.text = "Player: " + playerScore;
        botscoreT.text = "Bot: " + botScore;
        boTextAlert.text = "Bot: " + botScore;

        if(botScore > scoreToWin ) //finish game
        {
            cameraRotator.startRotate();
            gameOverT.enabled = true;
            gameObject.SetActive(false);
            playerscoreT.gameObject.SetActive(false);
            botscoreT.gameObject.SetActive(false);
            outText.enabled = false;
        }
        else if(playerScore > scoreToWin)
        {
            cameraRotator.startRotate();
            winT.enabled = true;
            gameObject.SetActive(false);
            playerscoreT.gameObject.SetActive(false);
            botscoreT.gameObject.SetActive(false);
            outText.enabled = false;

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
