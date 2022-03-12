using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    //variables
    [SerializeField] float fltTimeToCompleteQuestion = 30f;
    [SerializeField] float fltTimeToShowCorrectAnswer = 10f;

    public bool bolLoadNextQuestion;
    public float fltFillFraction;

    bool bolIsAnsweringQuestion;
    float fltTimerValue;

    void Update()
    {
        UpdateTimer();
    }
    //turn off the timer
    public void CancelTimer()
    {
        fltTimerValue = 0;
    }


    //update the timer
    void UpdateTimer()
    {
        //reduce the time remaining 
        fltTimerValue -= Time.deltaTime;

        //if user is answerin question
        if(bolIsAnsweringQuestion)
        {
            //is there time left 
            if(fltTimerValue > 0)
            {   
                //show a representation of how much time is left
                fltFillFraction = fltTimerValue / fltTimeToCompleteQuestion;
            }

            else
            {
                bolIsAnsweringQuestion = false;
                fltTimerValue = fltTimeToShowCorrectAnswer;
            }
        }

        //else (displaying the answer)
        else
        {
            //is there time left 
            if (fltTimerValue > 0)
            {
                //show a representation of how much time is left to show the answer
                fltFillFraction = fltTimerValue / fltTimeToShowCorrectAnswer;
            }

            else
            {
                //set up for next question
                bolIsAnsweringQuestion = true;
                fltTimerValue = fltTimeToCompleteQuestion;
                bolLoadNextQuestion = true;
            }
        }
    }
}
