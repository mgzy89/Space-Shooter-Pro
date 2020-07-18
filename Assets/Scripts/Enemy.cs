using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private float _speed = 4.0f;

    private Animator _anim;
    private Player _player;
    
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("Player is null");
        }
        _anim = GetComponent<Animator>();
        if(_anim==null)
        {
            Debug.LogError("The Animator is null");
        }

    }


    void Update()
    {

        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y< -5.5f)
        {
            float randomX = Random.Range(-9.5f, 9.5f);
            transform.position = new Vector3(randomX, 7f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player")
        {

            Player player = other.transform.GetComponent<Player>();
            
            if(player !=null)
            {
                player.Damage();
            }
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(this.gameObject,2.8f);
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if(_player!=null)
            {
                _player.AddToScore(10);
            }
            _speed = 0;
            _anim.SetTrigger("OnEnemyDeath");
            Destroy(this.gameObject,2.8f);
        }
    }
}
