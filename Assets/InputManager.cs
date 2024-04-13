using UnityEngine;

public class InputManager : MonoBehaviour
{
    public WordManager wordManager;

    void Update()
    {
        if (Input.anyKeyDown)
        {
            CheckInput();
        }
    }

    void CheckInput()
    {
        string typedWord = Input.inputString.Trim();
        string displayedWord = wordManager.wordDisplay.text;

        if (typedWord.Equals(displayedWord))
        {
            // Word typed correctly, handle success
            Debug.Log("Correct!");
            wordManager.DisplayRandomWord();
        }
        else
        {
            // Handle incorrect typing
            Debug.Log("Incorrect!");
        }
    }

}
