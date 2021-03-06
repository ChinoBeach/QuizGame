using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Quiz : MonoBehaviour
{
    //Variables
    [Header("Questions")]

    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;

    [Header("Answeres")]

    [SerializeField] private GameObject[] arrayAnswerButton;
    private int intCorrectAnswerIndex;
    private bool bolHasAnsweredEarly = true;

    [Header("Button Colors")]

    [SerializeField] private Sprite defaultAnswerSprite;
    [SerializeField] private Sprite correctAnswerSprite;

    [Header("Timer")]

    [SerializeField] private Image timerImage;
    private Timer timer;

    [Header("Scoring")]

    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("ProgressBar")]
    [SerializeField] Slider progressbar;

    public bool bolIsComplete;

    void Awake()
    {
        //setup timer and progress bar
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressbar.maxValue = questions.Count;
        progressbar.value = 0;

    }

    private void Update()
    {
        timerImage.fillAmount = timer.fltFillFraction;

        if (timer.bolLoadNextQuestion)
        {
            bolHasAnsweredEarly = false;
            GetNextQuestion();
            timer.bolLoadNextQuestion = false;

            if (progressbar.value == progressbar.maxValue)
            {
                bolIsComplete = true;
                return;
            }

        }
        else if (!bolHasAnsweredEarly && !timer.bolIsAnsweringQuestion)
        {
            DisplayAnswer(-1); 
            SetButtonState(false);        
        }
    }

    //turn on the buttons and then display the next question
    private void GetNextQuestion()
    {

        if(questions.Count > 0)
        { 
            SetButtonState(true);
            SetDefualtButtonSprite();
            GetRandomQuestion();
            DisplayQuestion();

            progressbar.value++;

            scoreKeeper.IncrementQuestionsSeen();
        }
        
    }

    void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];
        
        if(questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion); 
        }
        
    }

    //display question
    private void DisplayQuestion()

    {
        //display the question
        questionText.text = currentQuestion.GetQuestion();

        //display the answers on the buttons
        for (int i = 0; i < arrayAnswerButton.Length; i++)
        {
            TextMeshProUGUI buttonText = arrayAnswerButton[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }

    private void DisplayAnswer(int index)
    {
        //variable for the image of the button
        Image buttonImage;

        //if the correct answer was selected
        if (index == currentQuestion.GetCorrectAnswerIndex())
        {

            
            //tell the user they were correct
            questionText.text = "Correct!";
            //change the image of the correct answers button to make it stand out

            buttonImage = arrayAnswerButton[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();
        }

        //if a wrong answer was selected
        else
        {
            //find the correct answer and display it
            intCorrectAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            string strCorrectAnswer = currentQuestion.GetAnswer(intCorrectAnswerIndex);
            questionText.text = "Sorry, the correct answer was;\n" + strCorrectAnswer;

            //change the sprite

            buttonImage = arrayAnswerButton[intCorrectAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
    }

    //turn the  buttons on and off
    private void SetButtonState(bool bolState)
    {
        for (int i = 0; i < arrayAnswerButton.Length; i++)
        {
            Button button = arrayAnswerButton[i].GetComponent<Button>();
            button.interactable = bolState;
        }
    }

    //change all buttons to the default image
    private void SetDefualtButtonSprite()
    {
        for (int i = 0; i < arrayAnswerButton.Length; i++)
        {
            Image buttonImage = arrayAnswerButton[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }

    //when a user clicks a button ( selects an answer)
    public void OnAnswerSelected(int index)
    {
        bolHasAnsweredEarly = true;
        DisplayAnswer(index);
        
        //make it so you cant change your answer by clicking another button
        SetButtonState(false);

        //turn off the timer
        timer.CancelTimer();
        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";

       
    }
}