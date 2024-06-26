using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rama : MonoBehaviour
{
    private Animator anim;
    private Collider col;
    void Start()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
        col = gameObject.GetComponent<Collider>();
        SoundManager.THIS.PlaySound(0);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.GetComponentInChildren<PlayerMovement>())
        {
            anim.SetBool("hold", true);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.transform.GetComponentInChildren<PlayerMovement>())
        {
            anim.SetBool("hold", false);
        }
    }


    public void Despawn()
    {
        anim.SetBool("despawn", true);
        SoundManager.THIS.PlaySound(8);
        col.enabled = false;
        if (gameObject.TryGetComponent<PlantEnviro>(out PlantEnviro p))
            Invoke("Respawn", 5f);
        else
            Invoke("DestroyThis", 1f);
    }

    private void DestroyThis() {
        Destroy(gameObject);
    }

    private void Respawn() {
        anim.SetBool("despawn", false);
        SoundManager.THIS.PlaySound(0);
        col.enabled = true;
    }
}
