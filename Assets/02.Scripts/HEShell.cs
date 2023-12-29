using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HEShell : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player")
        {
            FireCannon fireCannon = other.GetComponent<FireCannon>();
            fireCannon.getShell();
            Destroy(gameObject);
        }
    }
}
