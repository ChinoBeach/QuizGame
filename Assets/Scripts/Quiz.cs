using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Quiz : MonoBehaviour
{
    //Variables 
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] QuestionSO question;   
    

    void Start()
    {
        questionText.text = question.GetQuestion(); 
    }

  
}
