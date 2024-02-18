using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField]
    private float _leftLimitDegree = 30f;

    [SerializeField]
    private float _rightLimitDegree = 330f;

    [SerializeField] private float _rotationSpeed;
    public float RotationSpeed => _rotationSpeed;

    [SerializeField] private GameObject _blueBullet;
    [SerializeField] private GameObject _whiteBullet;
    [SerializeField] private Transform _bulletPos;

    private int _direction = 0;

    void Start()
    {
        
    }

    void Update()
    {
        if(_direction != 0)
            Rotate(_direction);
    }

    public void SetRotationCannon(int directionToRotate)
    {
        _direction = directionToRotate;
    }

    public virtual void Rotate(int direction)
    {
        transform.parent.Rotate(Vector3.forward * direction * _rotationSpeed * Time.deltaTime);

        float eulerZ = transform.parent.localEulerAngles.z;

        if (direction == 1 && eulerZ > _leftLimitDegree && eulerZ < _rightLimitDegree)
            transform.parent.localEulerAngles = new Vector3(0f, 0f,
                Mathf.Clamp(eulerZ, 0f, _leftLimitDegree));
        else if (direction == -1 && eulerZ > _leftLimitDegree && eulerZ < 360f && eulerZ < _rightLimitDegree)
            transform.parent.localEulerAngles = new Vector3(0f, 0f, _rightLimitDegree);
    }

    public void Shoot(BulletType type)
    {
        var bulletPrefab = type == BulletType.White ? _whiteBullet : _blueBullet;
        var bulletInstance = Instantiate(bulletPrefab, _bulletPos.position, transform.rotation);
    }
}
