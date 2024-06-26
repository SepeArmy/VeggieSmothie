using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VictoryTrigger : MonoBehaviour
{
    
    
    void Start()
    {
       
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponentInChildren<PlayerMovement>())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }
    }
}
