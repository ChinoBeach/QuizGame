using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")]
public class QuestionSO : ScriptableObject
{
    [TextArea(2,6)] 
    [SerializeField] string strQuestion = "Enter New Question Text Here";
    [SerializeField] string[] strArrayAnswers = new string[4];

    [SerializeField] int intCorrectIndex; 


    public string GetQuestion()
    {
        return strQuestion; 
    }

    public int GetCorrectAnswerIndex()
    {
        return intCorrectIndex;
    }
    public string GetAnswer(int index)
    {
        return strArrayAnswers[index];
    }

}
