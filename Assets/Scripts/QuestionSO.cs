using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Allows the user to create a preset assest named Quiz Question as a file named New Question
[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")]
public class QuestionSO : ScriptableObject
{
    //creates a text box minimum 2 lines, max 6
    [TextArea(2,6)] 
    //sets defult text in the box sayinf "enter New Question Text Here" 
    [SerializeField] string strQuestion = "Enter New Question Text Here";

    //creates an array to house the 4 posisble answers
    [SerializeField] string[] strArrayAnswers = new string[4];

    //allows the user to declare which spot in the index holds the correct answer
    [SerializeField] int intCorrectIndex; 

    //GET METHODS ( Just return what information they are asking for) 
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
