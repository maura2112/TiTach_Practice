using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultipleChoice : MonoBehaviour
{
    public GameObject answerButtonPrefab; // Prefab Button
    public Transform answerPanel; // Panel chứa đáp án
    public Text resultText; // Kết quả trả lời

    public List<string> answers;
    public string correctAnswer;
    private List<GameObject> createdButtons = new List<GameObject>();

    void Start()
    {
        GenerateQuestion();
    }

    void GenerateQuestion()
    {
        // Xóa các Button cũ nếu có
        foreach (GameObject btn in createdButtons)
        {
            Destroy(btn);
        }
        createdButtons.Clear();

        resultText.text = "Video co dap an la: " + correctAnswer ; // Xóa kết quả hiển thị


        Shuffle(answers); // Xáo trộn thứ tự các đáp án

        // Tạo button cho từng đáp án
        foreach (string answer in answers)
        {
            GameObject answerButton = Instantiate(answerButtonPrefab, answerPanel);
            answerButton.GetComponentInChildren<Text>().text = answer;
            answerButton.GetComponent<Button>().onClick.AddListener(() => CheckAnswer(answer));
            createdButtons.Add(answerButton);
        }
    }

    void Shuffle(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            string temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public void CheckAnswer(string selectedAnswer)
    {
        if (selectedAnswer == correctAnswer)
        {
            Debug.Log("Correct");
        }
        else
        {
            Debug.Log("Wrong");
        }
    }

    public void ResetGame()
    {
        GenerateQuestion();
    }
}
