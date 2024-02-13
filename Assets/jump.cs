using UnityEngine;
using System.Collections;

public class ImmediateJumpAndMove : MonoBehaviour
{
    public GameObject duck;
    private bool isJumping = false;

    void Update()
    {
        // Check for spacebar input
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            // Start the ImmediateJumpAndMove coroutine
            StartCoroutine(ImmediateJumpAndMoveCoroutine());
        }
    }

    IEnumerator ImmediateJumpAndMoveCoroutine()
    {
        isJumping = true;

        float jumpHeight = 2.7f;
        float jumpDuration = 0.3f; // Adjust the duration for a quick jump
        Vector3 startPos = duck.transform.position;
        Vector3 endPos = startPos + Vector3.up * jumpHeight;

        // Jump up quickly
        float elapsedTime = 0f;
        while (elapsedTime < jumpDuration)
        {
            duck.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / jumpDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Move the duck forward immediately by 4 units
        float forwardDistance = 4f;
        duck.transform.Translate(Vector3.forward * forwardDistance);

        isJumping = false; // Allow jumping again
    }
}
