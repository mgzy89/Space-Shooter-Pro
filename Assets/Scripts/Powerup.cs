using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    [SerializeField]
    private float _speed = 3.0f;

   
    [SerializeField]//0 = Triple shot 1 = speed 2 = shield
    int powerupID;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //move down at the speed of 3 
        //When we leave the screen, destroy this object

        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -6.0f)
        {
            Destroy(this.gameObject);
        }
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                switch (powerupID)
                {
                    case 0:
                        player.ActiveTripleShot();
                        break;
                    case 1:
                        player.ActiveSpeedBoost();
                        break;
                    case 2:
                        player.ActiveShield();
                        break;
                    default:
                        break;
                }
              
            }
            Destroy(this.gameObject);
        }
    }
}
