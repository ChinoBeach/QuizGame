using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    //Variables
    [Header("Questions")]
    [SerializeField] private TextMeshProUGUI questionText;

    [SerializeField] private QuestionSO question;

    [Header("Answeres")]
    [SerializeField] private GameObject[] arrayAnswerButton;

    private int intCorrectAnswerIndex;
    private bool bolHasAnsweredEarly;

    [Header("Button Colors")]
    [SerializeField] private Sprite defaultAnswerSprite;

    [SerializeField] private Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] private Image timerImage;

    private Timer timer;

    private void Start()
    {
        //setup timer

        timer = FindObjectOfType<Timer>();

        //display the question
        GetNextQuestion();
    }

    private void Update()
    {
        timerImage.fillAmount = timer.fltFillFraction;

        if (timer.bolLoadNextQuestion)
        {
            bolHasAnsweredEarly = false;
            GetNextQuestion();
            timer.bolLoadNextQuestion = false;
        }
        else if (!bolHasAnsweredEarly && !timer.bolIsAnsweringQuestion)
        {
            bolHasAnsweredEarly = true;
            DisplayAnswer(-1);
            SetButtonState(false);        }
    }

    //turn on the buttons and then display the next question
    private void GetNextQuestion()
    {
        SetButtonState(true);
        SetDefualtButtonSprite();
        DisplayQuestion();
    }

    //display question
    private void DisplayQuestion()

    {
        //display the question
        questionText.text = question.GetQuestion();

        //display the answers on the buttons
        for (int i = 0; i < arrayAnswerButton.Length; i++)
        {
            TextMeshProUGUI buttonText = arrayAnswerButton[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = question.GetAnswer(i);
        }
    }

    private void DisplayAnswer(int index)
    {
        //variable for the image of the button
        Image buttonImage;

        //if the correct answer was selected
        if (index == question.GetCorrectAnswerIndex())
        {
            //tell the user they were correct
            questionText.text = "Correct!";
            //change the image of the correct answers button to make it stand out

            buttonImage = arrayAnswerButton[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }

        //if a wrong answer was selected
        else
        {
            //find the correct answer and display it
            intCorrectAnswerIndex = question.GetCorrectAnswerIndex();
            string strCorrectAnswer = question.GetAnswer(intCorrectAnswerIndex);
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
        DisplayAnswer(index);
        
        //make it so you cant change your answer by clicking another button
        SetButtonState(false);

        //turn off the timer
        timer.CancelTimer();
    }
}