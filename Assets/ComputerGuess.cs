using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerGuess : MonoBehaviour
{
    public Text instructionText;
    public Text resultText;
    public Button startButton;

    private int targetNumber;
    private int attemptsLeft;
    private List<int> guessedNumbers;

    int lower = 1, upper = 100;

    void Start()
    {
        startButton.onClick.AddListener(StartGame);
        InitializeGame();
    }

    void InitializeGame()
    {
        instructionText.text = "Click Start to begin guessing!";
        resultText.text = "";
        startButton.gameObject.SetActive(true);
        guessedNumbers = new List<int>();
    }

    void StartGame()
    {
        lower = 1;
        upper = 100;
        targetNumber = Random.Range(lower, upper);
        attemptsLeft = 10;
        guessedNumbers.Clear();
        instructionText.text = "The computer is guessing the number...";
        startButton.gameObject.SetActive(false);
        InvokeRepeating("ComputerGuessNumber", 1.0f, 2.0f); // Start guessing every 2 seconds
    }

    void ComputerGuessNumber()
    {
        if (attemptsLeft <= 0)
        {
            CancelInvoke("ComputerGuessNumber");
            resultText.text = $"Game Over! The number was {targetNumber}.";
            instructionText.text = "Click Start to play again.";
            startButton.gameObject.SetActive(true);
            return;
        }

        int guess = GenerateGuess();
        Debug.Log($"Computer guesses: {guess}");

        if (guess == targetNumber)
        {
            CancelInvoke("ComputerGuessNumber");
            resultText.text = $"Congratulations! The computer guessed the number {targetNumber} correctly!";
            instructionText.text = "Click Start to play again.";
            startButton.gameObject.SetActive(true);
        }
        else
        {
            attemptsLeft--;
            UpdateInstructionText(guess);
        }
    }

    int GenerateGuess()
    {
        int guess;
        do
        {
            guess = Random.Range(lower, upper);
        } while (guessedNumbers.Contains(guess));
        guessedNumbers.Add(guess);
        return guess;
    }

    void UpdateInstructionText(int guess)
    {
        string rangeHint;

        if (guess < targetNumber)
        {
            rangeHint = "The target number is higher.";
            lower = guess + 1;
        }
        else
        {
            rangeHint = "The target number is lower.";
            upper = guess - 1;
        }

        instructionText.text = $"{rangeHint} Attempts left: {attemptsLeft}";
        resultText.text = $"Computer guessed {guess}.";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
