using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public Transform playerTransform;
    public TextMeshProUGUI scoreText;
    void Start()
    {
        playerTransform = FindObjectOfType<PlayerController>().transform;
    }
    void Update()
    {
        scoreText.text = playerTransform.position.z.ToString("0");
    }
}
