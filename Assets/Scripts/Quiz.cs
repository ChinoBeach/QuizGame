using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    //Variables 
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] QuestionSO question;
    [SerializeField] GameObject[] arrayAnswerButton;

    int intCorrectAnswerIndex;
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    

    void Start()
    {

        //display the question
        GetNextQuestion();
        
    }

    //turn on the buttons and then display the next question
    void GetNextQuestion()
    {
        SetButtonState(true);
        SetDefualtButtonSprite();
        DisplayQuestion();

    }
    //display question
   void DisplayQuestion()

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
 
    //turn the  buttons on and off
    void SetButtonState(bool bolState)
    {
        for(int i = 0; i < arrayAnswerButton.Length; i ++)
        {
            Button button = arrayAnswerButton[i].GetComponent<Button>();
            button.interactable = bolState;
        }
    }

    //change all buttons to the default image
    void SetDefualtButtonSprite()
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
            questionText.text = "Sorry, the correct answer was;\n" +strCorrectAnswer;

            //change the sprite

            buttonImage = arrayAnswerButton[intCorrectAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;

        } 
        //make it so you cant change your answer by clicking another button
        SetButtonState(false);
    }


}
