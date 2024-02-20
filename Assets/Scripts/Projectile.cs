using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public enum BulletType
{
    White = 0,
    Blue = 1,
}

public class Projectile : MonoBehaviour
{
    public delegate void OnScoreAddedEvent();
    public static event OnScoreAddedEvent OnScoreAdded;

    [SerializeField] private BulletType _type;
    [SerializeField] private float _speed;
    [SerializeField] private bool _move = true;

    private Rigidbody2D _rb;
    private bool _isOnCeiling = false;

    public float Speed
    {
        get => _speed;
        set => _speed = value;
    }

    public bool DestroyOnChain { get; set; } = false;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public BulletType Type => _type;

    public virtual void Update()
    {
        if(_move)
            transform.Translate(Vector3.up * Speed * Time.deltaTime);
    }

    private void DestroyAllBallsInContact()
    {
        DestroyOnChain = true;
        OnScoreAdded?.Invoke();

        List<Collider2D> colliders = new List<Collider2D>();
        _rb.GetContacts(colliders);

        colliders = colliders.Where(col => col.gameObject.tag != "Ceiling" && 
            col.gameObject.tag != "Edges").ToList();

        foreach (var collider in colliders)
        {
            var ball = collider.GetComponent<Projectile>();
            if (ball == null) continue;

            if(ball.Type == _type)
            {
                if(!ball.DestroyOnChain)
                {
                    ball.DestroyAllBallsInContact();
                    Destroy(ball.gameObject);
                }
            }
            else if(!_isOnCeiling)
            {
                var ballRB = ball.GetComponent<Rigidbody2D>();
                ballRB.constraints -= RigidbodyConstraints2D.FreezePositionY;
                ReleaseFreezeYConstraint(collider.transform, ballRB);

                OnScoreAdded?.Invoke();
            }
        }
    }

    public void ReleaseFreezeYConstraint(Transform rootBall, Rigidbody2D ballRB)
    {
        var _otherBallColliders = new List<Collider2D>();
        ballRB.GetContacts(_otherBallColliders);
        _otherBallColliders = _otherBallColliders.Where(col => col.gameObject.tag != "Ceiling" &&
            col.gameObject.tag != "Edges").ToList();

        foreach (var otherBallCollider in _otherBallColliders)
        {
            if (otherBallCollider.transform.position.y < rootBall.transform.position.y)
            {
                var otherBallRb = otherBallCollider.GetComponent<Rigidbody2D>();
                otherBallRb.constraints -= RigidbodyConstraints2D.FreezePositionY;
                OnScoreAdded?.Invoke();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!_move)
            return;

        if (other.gameObject.tag == "Ceiling")
        {
            _isOnCeiling = true;
            _move = false;
        }
        else if(other.gameObject.tag == "Projectile")
        {
            _isOnCeiling = false;
            _move = false;
            var _otherBullet = other.gameObject.GetComponent<Projectile>();

            if(_otherBullet.Type == _type)
            {
                DestroyAllBallsInContact();
                Destroy(gameObject);
            }
        }
        else if (other.gameObject.tag == "Wall")
            transform.rotation = Quaternion.Euler(0f, 0f, -transform.rotation.eulerAngles.z);
    }
}
