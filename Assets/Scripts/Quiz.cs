using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Quiz : MonoBehaviour
{
    //Variables 
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] QuestionSO question;
    [SerializeField] GameObject[] arrayAnswerButton;
    

    void Start()
    {
        questionText.text = question.GetQuestion();

        for(int i = 0; i < arrayAnswerButton.Length; i++)
        {
            TextMeshProUGUI buttonText = arrayAnswerButton[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = question.GetAnswer(i);
        }

        
    }

  
}
