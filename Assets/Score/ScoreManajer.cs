using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _scoreText;
    private int _maxScore;
    private int _score; // Deklarasi _score

    public void UpdateUI()
    {
        _scoreText.text = "Score: " + _score + " / " + _maxScore;
    }

    public void SetMaxScore(int value)
    {
        _maxScore = value;
        UpdateUI();
    }

    public void AddScore(int value)
    {
        _score += value;
        UpdateUI();
    }

    private void Awake()
    {
        Debug.Log("Awake set max score");
        _score = 0;
        _maxScore = 0;
    }

    private void Start()
    {
        UpdateUI();
    }
}