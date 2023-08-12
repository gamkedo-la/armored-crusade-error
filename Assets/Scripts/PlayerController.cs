using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float horizontalInput;
    public float verticalInput;
    public float speed = 30.0f;
    public float xRangeLeft = 44.0f;
    public float xRangeRight = 22.0f;
    public float zRangeBottom = -17.0f;
    public float zRangeTop = 10.0f;

    public GameObject projectilePrefab;

    // GUI components we can update
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshPro healthText;

    // data shown on the GUI
    private int playerScore = 0;
    private float playerTime = 0;
    private float playerHealth = 60;

    // Start is called before the first frame update
    void Start()
    {

    }

    string formatTime(float timeInSeconds)
    {
        int timeInSecondsInt = (int)timeInSeconds;
        int minutes = (int)(timeInSeconds / 60f);
        int seconds = timeInSecondsInt - (minutes * 60);
        return minutes.ToString("D2") + ":" + seconds.ToString("D2");
    }

    string formatHealth(float hp)
    {
        string healthStr = "";
        int numBars = Mathf.RoundToInt(hp / 10f);
        for (int i = 0; i < numBars; i++)
        {
            healthStr += "█";
        }
        return healthStr;
    }

    void updateGUI()
    {

        if (scoreText) scoreText.text = " SCORE\n<mspace=.75em>" + playerScore.ToString("000000");

        if (timerText) timerText.text = "TIMER\n<mspace=.75em>" + formatTime(playerTime);

        if (healthText) healthText.text = formatHealth(playerHealth);
    }

    // Update is called once per frame
    void Update()
    {
        // fake value to test the gui
        playerScore++;

        // increase player time
        playerTime += Time.deltaTime;

        //Keep play in bounds
        if (transform.position.x < -xRangeLeft)
        {
            transform.position = new Vector3(-xRangeLeft, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xRangeRight)
        {
            transform.position = new Vector3(xRangeRight, transform.position.y, transform.position.z);
        }

        if (transform.position.z < zRangeBottom)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zRangeBottom);
        }
        if (transform.position.z > zRangeTop)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zRangeTop);
        }

        // Player input
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(transform.right * horizontalInput * Time.deltaTime * speed);

        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed);
        updateGUI();
    }
}

