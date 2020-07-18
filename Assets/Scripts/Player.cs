using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float _speed=5f;
    [SerializeField]
    private float _speedBoostSpeed = 8.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShot;
    [SerializeField]
    private float _fireRate=0.15f;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private bool _isTripleShotEn=false;
    [SerializeField]
    private bool _isSpeedBostEn = false;
    [SerializeField]
    private bool _isShieldEn = false;
    [SerializeField]
    float _timeToWait = 5.0f;
    [SerializeField]
    private GameObject _sheild;
    [SerializeField]
    private GameObject _rightEngine;
    [SerializeField]
    private GameObject _leftEngine;

    private SpawnManger _spawnManger;
    private float _canFire = -1f;
    private Vector3 offsetVector= new Vector3(0, 1.05f, 0);
    private int _hurtWing;
    [SerializeField]
    private int _score=0;

    [SerializeField]
    private AudioSource _laserShotSound;

    [SerializeField]
    private UIManager _uiManager;


    void Start()
    {
        //take the current position= new positiont(0,0,0)
        transform.position=new Vector3(0,0,0);
        _spawnManger = GameObject.Find("SpawnManger").GetComponent<SpawnManger>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_spawnManger == null)
        {
            UnityEngine.Debug.LogError("The Spawn Manger is Null");
        }

        if(_uiManager==null)
        {
            UnityEngine.Debug.LogError("The UI Manger is Null");
        }
    }


    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space)&&Time.time>_canFire)
        {
            
             shootLaser();
        }
    }

    void shootLaser()
    {
        _canFire = Time.time + _fireRate;
        Vector3 offset = transform.position + offsetVector;

        if (_isTripleShotEn)
        {
            Instantiate(_tripleShot, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, offset, Quaternion.identity);
        }
       
       if(_laserShotSound==null)
        {
            UnityEngine.Debug.LogError("Laser sound is null");
        }
       else
        {
            _laserShotSound.Play();
        }
        
    }


    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        if(_isSpeedBostEn)
        {
            transform.Translate(direction * _speedBoostSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }
       

        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -3.9f)
        {
            transform.position = new Vector3(transform.position.x, -3.9f, 0);
        }  


        if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    public void Damage()
    {
        if(_isShieldEn)
        {
            DeactivateShield();
            return;
        }

        _lives--;

        switch (_lives)
        {
            case 2:
                _hurtWing = Random.Range(0, 3);
                if (_hurtWing == 1)
                {
                    _rightEngine.SetActive(true);
                }
                else
                {
                    _leftEngine.SetActive(true);
                }
                break;
            case 1:
                if (_hurtWing == 1)
                {
                    _leftEngine.SetActive(true);
                }
                else
                {
                    _rightEngine.SetActive(true);
                }
                break;
        }

        _uiManager.UpdeteLives(_lives);
        if (_lives == 0)
        {
           
            _spawnManger.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void ActiveTripleShot()
    {
        _isTripleShotEn = true;
        StartCoroutine(TripleShotPowerDown());
    }

    public void ActiveSpeedBoost()
    {
        _isSpeedBostEn = true;
        StartCoroutine(SpeedBostPowerDown());
    }

    public void ActiveShield()
    {
        _isShieldEn = true;
        _sheild.SetActive(true);
        //StartCoroutine(ShieldPowerDown());
    }

    public void DeactivateShield()
    {
        _isShieldEn = false;
        _sheild.SetActive(false);
    }

    private IEnumerator ShieldPowerDown()
    {
        while (_isShieldEn)
        {
            yield return new WaitForSeconds(_timeToWait);
            _isShieldEn = false;
        }

    }

    private IEnumerator SpeedBostPowerDown()
    {
        while (_isSpeedBostEn)
        {
            yield return new WaitForSeconds(_timeToWait);
            _isSpeedBostEn = false;
        }
    }

    private IEnumerator TripleShotPowerDown()
    {
        while (_isTripleShotEn)
        {
            yield return new WaitForSeconds(_timeToWait);
            _isTripleShotEn = false;
        }
    }

    public void AddToScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }



    //method to add 10 t othe score
    //Communicate with the UI to upate Score
}
