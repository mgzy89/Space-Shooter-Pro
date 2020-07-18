using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //handle ti text
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Sprite[] _liveSprite;
    [SerializeField]
    private Image _LiveImage;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;
    private bool _isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            if (_isGameOver)
            {
                _isGameOver = false;
                SceneManager.LoadScene("Game");
            }
        }
    }

    public void UpdateScore(int scoreValue)
    {
        _scoreText.text = "Score: " + scoreValue;
    }

    public void UpdeteLives(int currentLives)
    {
        _LiveImage.sprite = _liveSprite[currentLives];
        if(currentLives==0)
        {
            StartCoroutine(GameOverFlicker());
            _restartText.gameObject.SetActive(true);
            _isGameOver = true;
        }
    }

    IEnumerator GameOverFlicker()
    {
        while(true)
        {
            _gameOverText.gameObject.SetActive(!true);
            yield return new WaitForSeconds(0.5f);
            _gameOverText.gameObject.SetActive(!false);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
