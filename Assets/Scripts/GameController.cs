using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Cannon _cannon;
    [SerializeField] private BulletType _currentType;
    [SerializeField] private GameObject _whiteBullet;
    [SerializeField] private GameObject _blueBullet;
    [SerializeField] private GameObject _inGameContainer;
    [SerializeField] private GameObject _inGameUIContainer;
    [SerializeField] private GameObject _endGameContainer;
    [SerializeField] private TextMeshProUGUI _scoreTMPRO;
    [SerializeField] private TextMeshProUGUI _endGameScoreTMPRO;
    [SerializeField] private Button _leftArrowBtn;
    [SerializeField] private Button _rightArrowBtn;
    [SerializeField] private Button _throwBallBtn;

    private int _score;
    private List<GameObject> _ballsInScene;

    void Start()
    {
        Projectile.OnScoreAdded += HandleOnScoreAdded;
        Timer.OnTimeIsOver += HandleOnGameOver;

        _ballsInScene = GameObject.FindGameObjectsWithTag("Projectile").ToList();
    }

    /// <summary>
    /// Call this function to rotate the cannon.
    /// </summary>
    /// <param name="direction">Set 1 to right, -1 to left, and 0 to stop.</param>
    public void RotateCannon(int direction)
    {
        bool canRotate = _leftArrowBtn.interactable && _rightArrowBtn.interactable;

        if (canRotate)
            _cannon.SetRotationCannon(direction);
    }

    public void Shoot()
    {
        _cannon.Shoot(_currentType);
        _currentType = (BulletType)Random.Range(0, 2);

        if(_currentType == BulletType.White)
        {
            _whiteBullet.SetActive(true);
            _blueBullet.SetActive(false);
        }
        else
        {
            _whiteBullet.SetActive(false);
            _blueBullet.SetActive(true);
        }
    }

    public void HandleOnScoreAdded()
    {
        _score++;
        _scoreTMPRO.text = _score.ToString();
    }

    public void HandleOnGameOver()
    {
        _leftArrowBtn.interactable = false;
        _rightArrowBtn.interactable = false;
        _throwBallBtn.interactable = false;
        Invoke(nameof(CallEndGameUI), 3f);
    }

    private void CallEndGameUI()
    {
        _inGameContainer.SetActive(false);
        _inGameUIContainer.SetActive(false);
        _endGameContainer.SetActive(true);
        _endGameScoreTMPRO.text =  "Score: " + _scoreTMPRO.text;
    }
}
