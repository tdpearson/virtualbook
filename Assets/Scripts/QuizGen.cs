using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

/*
public class QuizAnswers
{
    public string answer;
    public string response;
}

public class QuizQuestion
{
    public string question;
    public QuizAnswers[] answers;

}

public class Quiz
{
    public QuizQuestion[] questions;
}
*/



public class QuizGen : MonoBehaviour {

    public GameObject QuestionCanvas;
    public GameObject AnswerCanvas;

    Button[] AnswerButtons;
    Text[] AnswerTexts;

    // Use this for initialization
    void Start () {
        string questionsFilePath = Application.streamingAssetsPath + "/Quiz/Questions.json";
        if ( ! File.Exists(questionsFilePath))
        {
            Debug.Log("Using Questions.json");
            string dataAsJson = File.ReadAllText(questionsFilePath);
            // TODO: add functionality to get questions from json
            //Quiz questions = JsonUtility.FromJson<Quiz>(dataAsJson);
            //Debug.Log(questions);
        } else
        {
            Debug.Log("Could not find Questions.json in streaming assets path");
            Text QuestionText = QuestionCanvas.AddComponent<Text>();
            QuestionText.text = "What is your favorite color?";
            QuestionText.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
            //QuestionText.fontSize = 96;
            QuestionText.resizeTextForBestFit = true;
            QuestionText.alignment = TextAnchor.MiddleCenter;

            /*for (int answer = 0; answer < 3; answer++)
            {
                AnswerButtons[answer] = AnswerCanvas.AddComponent<Button>();
                // TODO: Add answer text to button
            }*/
            Button answerButton = AnswerCanvas.AddComponent<Button>();
            Text ButtonText = answerButton.gameObject.AddComponent<Text>();
            ButtonText.text = "This is a test...";
            ButtonText.resizeTextForBestFit = true;
            ButtonText.alignment = TextAnchor.MiddleCenter;

        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
