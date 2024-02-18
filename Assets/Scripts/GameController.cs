using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Cannon _cannon;
    [SerializeField] private BulletType _currentType;
    [SerializeField] private GameObject _whiteBullet;
    [SerializeField] private GameObject _blueBullet;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    /// <summary>
    /// Call this function to rotate the cannon.
    /// </summary>
    /// <param name="direction">Set 1 to right, -1 to left, and 0 to stop.</param>
    public void RotateCannon(int direction)
    {
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
}
