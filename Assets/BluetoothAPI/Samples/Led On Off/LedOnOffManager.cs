
using System.Collections;
using UnityEngine;
using ArduinoBluetoothAPI;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
public class LedOnOffManager : MonoBehaviour
{
    private BluetoothHelper helper;
    public GameObject connectBtn;
    public GameObject disconnectBtn;
    public GameObject splash;
    public AudioSource correctAudio;
    public GameObject led;

    private bool isJumping = false;
    private bool jumpKeeper = false;
    private bool hasFirstJumpOccurred = false;
    private bool hasBackwardJumpOccurred = false;
    private bool isGeneratingCommand = false;
    public TextMeshProUGUI commandText;
    public TextMeshProUGUI scoreText;
  
    private int score = 0;
    public TextMeshProUGUI timerText;
    private float timerDuration = 135f; // 2 minutes in seconds
    private float currentTime;
    private bool isTimerRunning = true;
    public GameObject fail;
    public GameObject welcomeIMG;
    public AudioSource splashSound;
    public AudioSource startSound;
    public AudioClip startAudioClip;
    public AudioSource victory;
    void Start()
    {
        BluetoothHelper.BLE = false;
        helper = BluetoothHelper.GetInstance();
        helper.OnConnected += OnConnected;
        helper.OnConnectionFailed += OnConnectionFailed;
        helper.OnDataReceived += OnDataReceived;
        helper.setFixedLengthBasedStream(1); //data is received byte by byte
        helper.setDeviceName("land_to_pond");
        currentTime = timerDuration;
        
        Connect();
        splashSound = GameObject.Find("watersound").GetComponent<AudioSource>();
        victory=GameObject.Find("victory").GetComponent<AudioSource>();
        startAudioClip = Resources.Load<AudioClip>("level_1_instructionsEnglish"); // Replace with your actual audio file name

        // Play the start audio
        if (startAudioClip != null)
        {
            startSound.clip = startAudioClip;
            startSound.Play();
        }  
        StartCoroutine(DelayedStart());
        StartCoroutine(GenerateCommands());
        fail.SetActive(false);
        splash.SetActive(false);
         
        
        
    }
    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(15f); // Wait for the audio clip to finish and an additional 15 seconds
    }
            void Update()
        {   
            
            if (isTimerRunning)
            {
                currentTime -= Time.deltaTime;
                
                if (currentTime <= 0f)
                {

                    currentTime = 0f;
                    isTimerRunning = false;
                    if(score>7){
                        victory.Play();
                        SceneManager.LoadScene("Level2");
                    }
                    if(score<5){
                            fail.SetActive(true);
                            SceneManager.LoadScene("MainMenu");
                    }
                    // Add code to handle timer expiration
                }

                // Format the time and update the UI text
                UpdateTimerText();
            }
        }

        void UpdateTimerText()
        {
            // Format minutes and seconds
            int minutes = Mathf.FloorToInt(currentTime / 60f);
            int seconds = Mathf.FloorToInt(currentTime % 60f);

            // Update the TextMeshProUGUI component
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        
    IEnumerator GenerateCommands()
    {
       yield return new WaitForSeconds(12f); //
       welcomeIMG.SetActive(false);
       for(int i=0;i<17;i++)
        {
            if (!isGeneratingCommand)
            {
                isGeneratingCommand = true;
                if(i==0){
                    commandText.text="pond";
                }
                else{
                // Randomly choose between "land" and "pond"
                string command = Random.Range(0, 2) == 0 ? "land" : "pond";

                // Display the command in the game
                commandText.text = command;
                }

                // Wait for a certain interval before generating the next command
                // float interval = 5f; // Adjust as needed
                yield return new WaitForSeconds(5f);
                

                isGeneratingCommand = false;
            }
            else
            {
                // Wait for a short time before checking again
                yield return null;
            }
        }
    }

    void OnConnected(BluetoothHelper helper)
    {
        helper.StartListening();
        connectBtn.SetActive(false);
        disconnectBtn.SetActive(true);
    }

    void OnConnectionFailed(BluetoothHelper helper)
    {
        Debug.Log("Failed to connect");
        connectBtn.SetActive(true);
        disconnectBtn.SetActive(false);
    }

    void OnDataReceived(BluetoothHelper helper)
    {
        string msg = helper.Read();

        switch (msg)
        {
            case "1":
                if (!isJumping)
                {
                    if (hasFirstJumpOccurred && !hasBackwardJumpOccurred)
                    {
                        StartCoroutine(JumpBackward());
                        hasBackwardJumpOccurred = true;
                        jumpKeeper = false;

                        if (commandText.text == "land")
                        {
                            IncrementScore();
                            
                        }
                       
                    }
                    break;
                }
                break;
            case "0":
                
                // led.GetComponent<Renderer>().material.color = Color.gray;
                if (!isJumping && !jumpKeeper)
                {
                    StartCoroutine(JumpAndMoveForward());

                    hasFirstJumpOccurred = true;
                    hasBackwardJumpOccurred = false;

                    if (commandText.text == "pond")
                    {
                        IncrementScore();
                        
                    }
                   
                }
                break;
            default:
                Debug.Log($"Received unknown message [{msg}]");
                break;
        }
    }

    void IncrementScore()
    {
        score++;
        scoreText.text = score.ToString();
        

       if (correctAudio !=null){
        correctAudio.Play();
       }
    
    }
    
    IEnumerator JumpAndMoveForward()
    {
        isJumping = true;

        // Jump animation or vertical movement (modify as needed)
        float jumpHeight = 2.7f;
        float jumpDuration = 1.6f;
        Vector3 startPos = led.transform.position;
        Vector3 endPos = startPos + Vector3.up * jumpHeight;

        float elapsedTime = 0f;
        while (elapsedTime < jumpDuration)
        {
            led.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / jumpDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Set the LED back to the ground
        led.transform.position = endPos;

        // Move the LED forward by 3 units (modify as needed)
        float forwardDistance = 7f;
        led.transform.Translate(Vector3.forward * forwardDistance);
        if (splashSound != null)
    {
        splashSound.Play();
    }

        splash.SetActive(true);
        isJumping = false;
        jumpKeeper = true;
    }

    IEnumerator JumpBackward()
    {
        isJumping = true;

        // Jump backward animation or vertical movement (modify as needed)
        float jumpBackwardDistance = 7f;
        float jumpBackwardDuration = 1.6f;
        Vector3 startPos = led.transform.position;
        Vector3 endPos = startPos - Vector3.forward * jumpBackwardDistance;

        float elapsedTime = 0f;
        while (elapsedTime < jumpBackwardDuration)
        {
            led.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / jumpBackwardDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        splash.SetActive(false);
        // Set the LED back to the ground
        led.transform.position = endPos;

        isJumping = false;
    }

    public void Connect()
    {
        helper.Connect();
    }

    public void Disconnect()
    {
        helper.Disconnect();
        connectBtn.SetActive(true);
        disconnectBtn.SetActive(false);
    }

    public void sendData(string d)
    {
        helper.SendData(d);
    }
}
