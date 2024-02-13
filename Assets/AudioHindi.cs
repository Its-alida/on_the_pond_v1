// using UnityEngine;
// using UnityEngine.UI;

// public class AudioHindi : MonoBehaviour
// {
//     public Button yourButton; // Reference to your button
//     public AudioSource audioSource; // Reference to your AudioSource
//     public GameObject panelToDisable; // Reference to the panel you want to disable

//     void Start()
//     {
//         Button btn = yourButton.GetComponent<Button>();
//         btn.onClick.AddListener(TaskOnClick);
//     }

//     void TaskOnClick()
//     {
//         // Disable the panel
//         if (panelToDisable != null)
//         {
//             panelToDisable.SetActive(false);
//         }

//         // Play the audio clip when the button is clicked
//         if (audioSource != null)
//         {
//             audioSource.Play();
//         }
//     }
// }
using UnityEngine;
using UnityEngine.UI;

public class AudioHindi : MonoBehaviour
{
    public Button yourButton; // Reference to your button
    public AudioSource audioSource; // Reference to your AudioSource

    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        // Play the audio clip when the button is clicked
        audioSource.Play();
    }
}
