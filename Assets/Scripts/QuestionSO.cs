using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")]
public class QuestionSO : ScriptableObject
{
    [TextArea(2,6)] 
    [SerializeField] string strQuestion = "Enter New Question Text Here";

    public string GetQuestion()
    {
        return strQuestion; 
    }

}
