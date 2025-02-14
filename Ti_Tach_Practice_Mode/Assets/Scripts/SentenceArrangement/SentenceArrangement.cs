using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SentenceArrangement : MonoBehaviour
{
    public GameObject wordButtonPrefab;
    public Transform wordPanel;
    public Transform dropPanel;
    public Text resultText;

    private List<string> words;
    private List<GameObject> createdButtons = new List<GameObject>();
    public string sentence;// Câu mẫu

    void Start()
    {
        GenerateWords();
    }

    void GenerateWords()
    {
        // Xóa dữ liệu cũ nếu có
        foreach (GameObject btn in createdButtons)
        {
            Destroy(btn);
        }
        createdButtons.Clear();
        
        // Xóa text kết quả
        resultText.text = "Video mo ta: "+sentence;

        words = new List<string>(sentence.Split(' ')); // Chia câu thành các từ
        Shuffle(words); // Xáo trộn thứ tự các từ

        foreach (string word in words)
        {
            GameObject wordButton = Instantiate(wordButtonPrefab, wordPanel);
            wordButton.GetComponentInChildren<Text>().text = word;
            wordButton.GetComponent<DraggableWord>().SetWord(word);
            createdButtons.Add(wordButton);
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

    public void CheckAnswer()
    {
        List<string> currentOrder = new List<string>();

        foreach (Transform child in dropPanel)
        {
            currentOrder.Add(child.GetComponentInChildren<Text>().text);
        }

        string currentSentence = string.Join(" ", currentOrder);
        if (currentSentence == sentence)
        {
            Debug.Log("Correct");
        }
        else
        {
            Debug.Log("Try again");
        }
    }

    public void ResetGame()
    {
        GenerateWords();
    }
}
