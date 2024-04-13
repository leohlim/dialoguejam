using UnityEngine;
using TMPro;

public class WordManager : MonoBehaviour
{
    public TextMeshProUGUI wordDisplay;
    public RectTransform panelRectTransform; // Reference to the RectTransform of the UI panel
    public Color correctLetterColor = Color.white; // Color for correctly typed letters
    public Color incorrectLetterColor = Color.gray; // Color for incorrectly typed letters
    public float typingTimeLimit = 10f; // Time limit for typing in seconds
    private string currentWord; // The word the player needs to type
    private int currentLetterIndex; // Index of the current letter the player needs to type
    private float currentTime; // Current time remaining for typing
    private bool typingPaused; // Flag to indicate if typing is paused due to incorrect typing

    private string[] wordList = { "Why did you do it?", "Can you remember anything?", "Tell me their name!", "I know you're hiding something", "dont screw with me!" };

    void Start()
    {
        // Set the width of the text field
        SetTextFieldWidth();

        // Start the game
        StartGame();
    }

    void SetTextFieldWidth()
    {
        // Find the longest word in the word list
        string longestWord = GetLongestWord();

        // Calculate the size of the longest word in TextMeshPro
        TMP_TextInfo textInfo = wordDisplay.GetTextInfo(longestWord);
        float maxCharacterWidth = 0f;
        foreach (TMP_CharacterInfo charInfo in textInfo.characterInfo)
        {
            maxCharacterWidth += charInfo.xAdvance;
        }

        // Set the preferred width of the text field
        wordDisplay.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxCharacterWidth);
    }

    string GetLongestWord()
    {
        string longestWord = "";
        foreach (string word in wordList)
        {
            if (word.Length > longestWord.Length)
            {
                longestWord = word;
            }
        }
        return longestWord;
    }

    void StartGame()
    {
        // Reset variables
        currentLetterIndex = 0;
        currentTime = typingTimeLimit;
        typingPaused = false; // Reset typing pause flag

        // Get a new word to type
        currentWord = wordList[Random.Range(0, wordList.Length)];
        wordDisplay.text = currentWord;

        // Set the size of the panel to fit the text
        panelRectTransform.sizeDelta = new Vector2(wordDisplay.preferredWidth, panelRectTransform.sizeDelta.y);

        // Set initial color of all letters to gray
        SetLettersColor(0, currentWord.Length, incorrectLetterColor);

        // Start the typing timer
        InvokeRepeating("UpdateTimer", 1f, 1f);
    }

    void UpdateTimer()
    {
        currentTime -= 1f;
        if (currentTime <= 0f)
        {
            // Time's up, call the failed method
            Failed();
        }
    }

    void Update()
    {
        if (!typingPaused)
        {
            CheckInput();
        }
    }

    void CheckInput()
    {
        if (Input.anyKeyDown)
        {
            // Check if the pressed key matches the next letter in the word
            char nextLetter = currentWord[currentLetterIndex];
            if (Input.inputString.Length > 0 && Input.inputString[0] == nextLetter)
            {
                // Correct letter typed, move to the next letter
                currentLetterIndex++;

                // Update color of the typed letter to white
                SetLetterColor(currentLetterIndex - 1, correctLetterColor);

                if (currentLetterIndex >= currentWord.Length)
                {
                    // Entire word typed correctly, call the succeeded method
                    Succeeded();
                }
            }
            else
            {
                // Incorrect letter typed, pause typing for a short duration
                typingPaused = true;
                Invoke("ResumeTyping", 0);
            }
        }
    }

    void ResumeTyping()
    {
        typingPaused = false;
    }

    void Succeeded()
    {
        // Cancel the timer
        CancelInvoke("UpdateTimer");

        // Call the succeeded method
        Debug.Log("Success!");
        // Call your succeeded method here

        // Reset current letter index
        currentLetterIndex = 0;

        // Reset typing pause flag
        typingPaused = false;

        // Start the game again
        StartGame();
    }

    void Failed()
    {
        // Cancel the timer
        CancelInvoke("UpdateTimer");

        // Call the failed method
        Debug.Log("Failed!");
        // Call your failed method here
    }

    public void DisplayRandomWord()
    {
        string randomWord = wordList[Random.Range(0, wordList.Length)];
        wordDisplay.text = randomWord;

        // Set the size of the panel to fit the text
        panelRectTransform.sizeDelta = new Vector2(wordDisplay.preferredWidth, panelRectTransform.sizeDelta.y);
    }

    void SetLetterColor(int index, Color color)
    {
        // Change color of the letter at given index
        TMP_TextInfo textInfo = wordDisplay.textInfo;
        int materialIndex = textInfo.characterInfo[index].materialReferenceIndex;
        int vertexIndex = textInfo.characterInfo[index].vertexIndex;
        Color32[] vertexColors = wordDisplay.textInfo.meshInfo[materialIndex].colors32;
        for (int i = 0; i < 4; i++)
        {
            vertexColors[vertexIndex + i] = color;
        }
        wordDisplay.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
    }

    void SetLettersColor(int startIndex, int endIndex, Color color)
    {
        // Change color of letters within given range
        for (int i = startIndex; i < endIndex; i++)
        {
            SetLetterColor(i, color);
        }
    }
}
