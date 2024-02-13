using UnityEngine;
using UnityEngine.UI;

public class quitgame : MonoBehaviour
{
    public Button quitButton; // Reference to the quit button

    void Start()
    {
        // Subscribe to the button click event
        quitButton.onClick.AddListener(OnQuitButtonClick);
    }

    // Method called when the quit button is clicked
    void OnQuitButtonClick()
    {
        // Quit the application
        Application.Quit();
    }
}
