using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnredaderaSeedCollides : MonoBehaviour
{
    [SerializeField] private GameObject enredaderaPrefab;

    private void OnCollisionEnter(Collision collision) {
        // Si choca contra una superficie más o menos plana
        if (collision.GetContact(0).normal.y > 0.7f) {
            // Instanciamos en el punto de colisión
            Instantiate(enredaderaPrefab, collision.GetContact(0).point, Quaternion.identity);
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
