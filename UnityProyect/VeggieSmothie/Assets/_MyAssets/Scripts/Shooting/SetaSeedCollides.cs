using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetaSeedCollides : MonoBehaviour
{
    [SerializeField] private GameObject setaPrefab;

    private void OnCollisionEnter(Collision collision) {
        // Si choca contra una superficie más o menos plana
        if (collision.GetContact(0).normal.y > 0.7f) {
            // Instanciamos en el punto de colisión
            Instantiate(setaPrefab, collision.GetContact(0).point, Quaternion.identity);            
            Destroy(gameObject);    
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
