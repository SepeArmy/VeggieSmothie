using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnerCollides : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.TryGetComponent<Enredadera>(out Enredadera enredaderaScript))
        {
            enredaderaScript.Despawn();
            
        }
        else if (collision.gameObject.TryGetComponent<Rama>(out Rama ramaScript))
        {
            ramaScript.Despawn();
            
        }
        else if (collision.gameObject.TryGetComponent<Seta>(out Seta setaScript))
        {
            setaScript.Despawn();           
        }

        Destroy(gameObject);
    }

    
}
