using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Action OnTimeIsOver;

    [SerializeField] private float _duration;
   
    private TextMeshProUGUI _timer;
    private float _counter;

    private void OnEnable()
    {
        _timer = GetComponent<TextMeshProUGUI>();
        _counter = _duration;
    }

    private void OnDisable()
    {
        _counter = _duration;
    }

    void Update()
    {
        if (_counter > 0)
        {
            _counter -= Time.deltaTime;
            _timer.text = ((int)_counter).ToString();
        }
        else
            OnTimeIsOver?.Invoke();
    }
}
