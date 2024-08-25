using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _moveSpeed;
    [SerializeField] float _rotationSpeed;

    public List <ParticleSystem> ps;

    bool _isDeath;

    public static event Action OnPlayerDeath;

    int i = 0;

    private void Update()
    {
        if (_isDeath) return;

        Move();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //_isDeath = true;
            //gameObject.SetActive(false);
            //OnPlayerDeath?.Invoke();

            if(i < ps.Count)
            {
                playParticle(i);
                i++;
            }
            
        }
    }

    void playParticle(int count)
    {
        for (int i = 0; i < ps.Count; i++) 
        {
            ps[i].Stop();
        }
        ps[count].Play();   
    }

    private void Move()
    {
        transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime);
    }

    public void LeftTurn()
    {
        transform.Rotate(Vector3.up * -_rotationSpeed * Time.deltaTime);
    }

    public void RightTurn()
    {
        transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime);
    }

    public void Revive()
    {
        _isDeath = false;
        gameObject.SetActive(true);
    }
}
