using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPickups : MonoBehaviour
{

    //variable to store objects
    public GameObject[] pickups;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(pickups[Random.Range(0, pickups.Length)], transform.position,
            transform.rotation);
    }

   
}
