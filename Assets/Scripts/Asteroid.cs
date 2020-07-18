using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed=3f;
    [SerializeField]
    private GameObject _explosionPrefab;
    private SpawnManger _spawnMamger;
    


    private void Start()
    {
        _spawnMamger = GameObject.Find("SpawnManger").GetComponent<SpawnManger>();
    }

    // Update is called once per frame
    void Update()
    {
        //rotate object on the z axis
        
        transform.Rotate(Vector3.forward* _rotationSpeed * Time.deltaTime);
    }

    //check for lase collission (Trigger)
    //insraniate explosion at the position of the astroid (us)
    //destroy the explosion after 3 sec

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            if(_explosionPrefab!=null)
            {
                
                Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
                Destroy(other.gameObject);
                _spawnMamger.StartSpawning();
                Destroy(this.gameObject, 0.25f);

            }
        }
    }

   
}
