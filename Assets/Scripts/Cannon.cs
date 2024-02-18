using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField]
    private float _upperLimitDegree = 90f;

    [SerializeField]
    private float _lowerLimitDegree = 345f;

    [SerializeField] private float _rotationSpeed;
    public float RotationSpeed => _rotationSpeed;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public virtual void Aim(float direction)
    {
        transform.Rotate(Vector3.forward * direction * _rotationSpeed * Time.deltaTime);

        float eulerZ = transform.localEulerAngles.z;

        if (direction == 1 && eulerZ > _upperLimitDegree && eulerZ < _lowerLimitDegree)
            transform.localEulerAngles = new Vector3(0f, 0f,
                Mathf.Clamp(eulerZ, 0f, _upperLimitDegree));
        else if (direction == -1 && eulerZ > _upperLimitDegree && eulerZ < 360f && eulerZ < _lowerLimitDegree)
            transform.localEulerAngles = new Vector3(0f, 0f, _lowerLimitDegree);
    }
}
