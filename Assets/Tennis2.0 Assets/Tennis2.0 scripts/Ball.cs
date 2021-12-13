using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
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
   [SerializeField] Text playerTextLabel;
   [SerializeField] Text boTextLabel;
   [SerializeField] Text gameOverT;
   [SerializeField] Text outText;
   [SerializeField] Text winT;

    int scoreToWin = 40;
    public float time = 3;
    public GameObject ball;
    public GameObject rePlay;
   

    void Start()
    {
        initialPos = transform.position;
        playerScore = 0;
        botScore = 0;
        gameOverT.enabled = false;
        //  playerscoreT.enabled = false;
        // botscoreT.enabled = false;
        playerTextAlert.enabled = false;
        boTextAlert.enabled = false;
        playerTextLabel.enabled = false;
        boTextLabel.enabled = false;
        outText.enabled = false;
        winT.enabled = false;
        rePlay.SetActive(false);
        updateScores();
        
    }
    public void RePlay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Wall"))
        {
            if (hitter == "player")
            {
                botScore+=15;
                playerTextAlert.enabled = true;
                boTextAlert.enabled = true;
                playerTextLabel.enabled = true;
                boTextLabel.enabled = true;
                cameraRotator.startRotate();
                playBotSound();
                StartCoroutine(FreeTime());

                if (botScore == 45)
                {
                    botScore = 40;
                }
            }
            else if (hitter == "bot")
            {
                playerScore+=15;
                playerTextAlert.enabled = true;
                boTextAlert.enabled = true;
                playerTextLabel.enabled = true;
                boTextLabel.enabled = true;
                cameraRotator.startRotate();
                playPlayerSound();
                StartCoroutine(FreeTime());

                if (playerScore == 45)
                {
                    playerScore = 40;
                }

            }
            outText.enabled = true;
            playerTextAlert.enabled = true;
            boTextAlert.enabled = true;
            playerTextLabel.enabled = true;
            boTextLabel.enabled = true;
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
        playerTextLabel.enabled = false;
        boTextLabel.enabled = false;
        cameraRotator.stopRotate();
      
    }
    IEnumerator FreeTime()
    {
        yield return new WaitForSeconds(time);
        playerTextAlert.enabled = false;
        boTextAlert.enabled = false;
        playerTextLabel.enabled = false;
        boTextLabel.enabled = false;
        cameraRotator.stopRotate();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Out"))
        {
          
           

            if (hitter == "player")
            {
                botScore+=15;
                playerTextAlert.enabled = true;
                boTextAlert.enabled = true;
                playerTextLabel.enabled = true;
                boTextLabel.enabled = true;
                cameraRotator.startRotate();
                playBotSound();
                StartCoroutine(FreeTime());

                if (botScore==45)
                {
                    botScore = 40;
                }
            }
            else if(hitter == "bot")
            {
                playerScore+=15;
                playerTextAlert.enabled = true;
                boTextAlert.enabled = true;
                playerTextLabel.enabled = true;
                boTextLabel.enabled = true;
                cameraRotator.startRotate();
                playPlayerSound();
                StartCoroutine(FreeTime());

                if (playerScore == 45)
                {
                    playerScore = 40;
                }

            }
            outText.enabled = true;
            playerTextAlert.enabled = true;
            boTextAlert.enabled = true;
            playerTextLabel.enabled = true;
            boTextLabel.enabled = true;
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
                playerTextAlert.enabled = true;
                boTextAlert.enabled = true;
                playerTextLabel.enabled = true;
                boTextLabel.enabled = true;
                cameraRotator.startRotate();
                playBotSound();
                StartCoroutine(FreeTime());

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
                playerTextAlert.enabled = true;
                boTextAlert.enabled = true;
                playerTextLabel.enabled = true;
                boTextLabel.enabled = true;
                cameraRotator.startRotate();
                playPlayerSound();
                StartCoroutine(FreeTime());

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
        playerTextAlert.text = "" + playerScore;
        botscoreT.text = "Bot: " + botScore;
        boTextAlert.text = "" + botScore;

        if(botScore > scoreToWin ) //finish game
        {
            cameraRotator.startRotate();
            gameOverT.enabled = true;
            playerTextAlert.enabled = false;
            boTextAlert.enabled = false;
            playerTextLabel.enabled = false;
            boTextLabel.enabled = false;
            gameObject.SetActive(false);
            playerscoreT.gameObject.SetActive(false);
            botscoreT.gameObject.SetActive(false);
            outText.enabled = false;
            rePlay.SetActive(true);
            playEndSound();
        }
        else if(playerScore > scoreToWin)
        {
            cameraRotator.startRotate();
            winT.enabled = true;
            playerTextAlert.enabled = false;
            boTextAlert.enabled = false;
            playerTextLabel.enabled = false;
            boTextLabel.enabled = false;
            gameObject.SetActive(false);
            playerscoreT.gameObject.SetActive(false);
            botscoreT.gameObject.SetActive(false);
            outText.enabled = false;
            rePlay.SetActive(true);
            playWinSound();
        }
        else if (playerScore == scoreToWin && botScore == scoreToWin)
        {
            gameOverT.enabled = false;
            gameObject.SetActive(true);

        }
    }
    public void playEndSound()
    {
        FindObjectOfType<AudioManager>().Play("GameOver");
    }
    public void playWinSound()
    {
        FindObjectOfType<AudioManager>().Play("Win");
    }
    public void playPlayerSound ()
    {
        FindObjectOfType<AudioManager>().Play("PlayerWinBall");
    }
    public void playBotSound()
    {
        FindObjectOfType<AudioManager>().Play("BotWinBall");
    }
   
    private void resetBall()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        transform.position = initialPos;
        hitter = "";
    }
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        resetBall();
    }
    public void showScore()
    {
        playerscoreT.enabled = false;
        botscoreT.enabled = false;
    }
}
