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
   [SerializeField] Text outText;
   [SerializeField] Text winT;

    int scoreToWin = 500;
    public float time = 5;
    public GameObject ball;


    void Start()
    {
        initialPos = transform.position;
        playerScore = 0;
        botScore = 0;
        gameOverT.enabled = false;
        outText.enabled = false;
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

    IEnumerator StartTime()
    {
        yield return new WaitForSeconds(time);
        outText.enabled = false;

      
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Out"))
        {
            outText.enabled = true;
          //  ball.SetActive(true);
            //ball.GetComponent<Renderer>().enabled = false;
            //ball.GetComponent<SphereCollider>().enabled = false;


            // Ball.GetComponent<Renderer>().enabled = false;

            StartCoroutine(StartTime());
              //ball.GetComponent<SphereCollider>().enabled = true;
            // cameraRotator.startRotate();


            //            if (waitSec > 0)
            //          {
            //               waitSec -= Time.fixedDeltaTime;
            //              waitSecInt = (int)waitSec;
            //               outText.enabled = false;
            //                gameObject.SetActive(true);
            //           }

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
