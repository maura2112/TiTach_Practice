using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordMatching : MonoBehaviour
{
    public GameObject wordPrefab; // Prefab Button
    public Transform leftPanel; // Panel chứa từ
    public Transform rightPanel; // Panel chứa nghĩa tương ứng
    public LineRenderer linePrefab; // Prefab đường nối
    public Transform lineParent; // Nơi chứa các đường nối
    public Text resultText;

    private Dictionary<GameObject, string> leftWords = new Dictionary<GameObject, string>();
    private Dictionary<GameObject, string> rightWords = new Dictionary<GameObject, string>();
    private Dictionary<GameObject, LineRenderer> activeLines = new Dictionary<GameObject, LineRenderer>();
    private Dictionary<GameObject, GameObject> connections = new Dictionary<GameObject, GameObject>();

    [System.Serializable]
    public class WordPair
    {
        public string word;
        public string meaning;
    }

    public List<WordPair> wordPairs = new List<WordPair>(); // Nhập dữ liệu từ Unity Editor

    private List<string> words = new List<string>();
    private List<string> meanings = new List<string>();

    void Start()
    {
        GenerateWords();
    }

    void GenerateWords()
    {
        // Xóa dữ liệu cũ
        foreach (Transform child in leftPanel) Destroy(child.gameObject);
        foreach (Transform child in rightPanel) Destroy(child.gameObject);
        foreach (Transform child in lineParent) Destroy(child.gameObject);

        leftWords.Clear();
        rightWords.Clear();
        activeLines.Clear();
        connections.Clear();
        words.Clear();
        meanings.Clear();
        resultText.text = "";

        // Tách từ và nghĩa từ danh sách nhập vào
        foreach (var pair in wordPairs)
        {
            words.Add(pair.word);
            meanings.Add(pair.meaning);
        }

        // Xáo trộn vị trí hiển thị (không làm mất liên kết đúng)
        List<string> shuffledWords = new List<string>(words);
        List<string> shuffledMeanings = new List<string>(meanings);
        Shuffle(shuffledWords);
        Shuffle(shuffledMeanings);

        // Sinh các button từ
        foreach (string word in shuffledWords)
        {
            GameObject btn = Instantiate(wordPrefab, leftPanel);
            btn.GetComponentInChildren<Text>().text = word;
            btn.GetComponent<Button>().onClick.AddListener(() => OnWordSelected(btn, true));
            leftWords[btn] = word;
        }

        // Sinh các button nghĩa
        foreach (string meaning in shuffledMeanings)
        {
            GameObject btn = Instantiate(wordPrefab, rightPanel);
            btn.GetComponentInChildren<Text>().text = meaning;
            btn.GetComponent<Button>().onClick.AddListener(() => OnWordSelected(btn, false));
            rightWords[btn] = meaning;
        }
    }

    void Shuffle(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            string temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    GameObject selectedLeft = null;
    GameObject selectedRight = null;

    void OnWordSelected(GameObject selected, bool isLeft)
    {
        if (isLeft)
        {
            selectedLeft = selected;
        }
        else
        {
            selectedRight = selected;
        }

        if (selectedLeft != null && selectedRight != null)
        {
            DrawLine(selectedLeft, selectedRight);
            selectedLeft = null;
            selectedRight = null;
        }
    }

    void DrawLine(GameObject left, GameObject right)
    {
        if (connections.ContainsKey(left) || connections.ContainsKey(right))
        {
            Debug.Log("Word already connected!");
            return;
        }

        LineRenderer line = Instantiate(linePrefab, lineParent);
        line.positionCount = 2;
        line.SetPosition(0, left.transform.position);
        line.SetPosition(1, right.transform.position);

        activeLines[left] = line;
        activeLines[right] = line;
        connections[left] = right;
        connections[right] = left;
        // Debug để kiểm tra kết nối
        string leftText = leftWords[left];
        string rightText = rightWords[right];
        Debug.Log($"Connected: {leftText} -> {rightText}");
        // 🟢 Kiểm tra ngay sau khi hoàn thành kết nối cuối cùng
        if (connections.Count / 2 == wordPairs.Count)
        {
            CheckAnswer();
        }
    }

    void CheckAnswer()
    {
        bool allCorrect = true;

        foreach (var pair in connections)
        {
            GameObject leftWord = pair.Key;
            GameObject rightWord = pair.Value;

            if (!leftWords.ContainsKey(leftWord) || !rightWords.ContainsKey(rightWord))
            {
                allCorrect = false;
                break;
            }
            // Lấy từ đúng theo danh sách nhập từ Editor
            string leftText = leftWords[leftWord];
            string rightText = rightWords[rightWord];

            if (!IsMatchingPair(leftText, rightText))
            {
                allCorrect = false;
                break;
            }
        }
        resultText.text = allCorrect ? "Correct!" : "Try again!";
        resultText.color = allCorrect ? Color.green : Color.red;
    }

    bool IsMatchingPair(string left, string right)
    {
        foreach (var pair in wordPairs)
        {
            if (pair.word == left && pair.meaning == right)
                return true; // Nếu có cặp đúng, trả về true ngay lập tức
        }
        return false; // Không tìm thấy cặp nào khớp, trả về false
    }


    public void ResetGame()
    {
        GenerateWords();
    }
}
