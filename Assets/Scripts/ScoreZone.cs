using TMPro;
using UnityEngine;

public class ScoreZone : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   public TMP_Text scoreRemainingText;
   private int _remainingItems = 0;
    private void Awake()
    {
        _remainingItems = GameObject.FindGameObjectsWithTag("Target").Length;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreRemainingText.text = _remainingItems.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Target")) return;
        _remainingItems--;
        UpdateScoreText();
        Destroy(other.gameObject);
    }
}
