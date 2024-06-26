using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamaSeedCollides : MonoBehaviour
{
    [SerializeField] private GameObject ramaParedIzqPrefab;
    [SerializeField] private GameObject ramaParedDchaPrefab;

    private void OnCollisionEnter(Collision collision) {
        // Si choca contra una superficie m�s o menos vertical
        if (collision.GetContact(0).normal.x > 0.7f) {
            // Instanciamos en el punto de colisi�n
            Instantiate(ramaParedIzqPrefab, collision.GetContact(0).point, Quaternion.identity);
            Destroy(gameObject);
        }
        if (collision.GetContact(0).normal.x < -0.7f) {
            // Instanciamos en el punto de colisi�n
            Instantiate(ramaParedDchaPrefab, collision.GetContact(0).point, Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
