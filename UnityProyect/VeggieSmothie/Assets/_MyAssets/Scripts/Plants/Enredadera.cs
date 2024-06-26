using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enredadera : MonoBehaviour
{

    [SerializeField] private GameObject ps;
    private Animator anim;
    private Collider col;
    void Start()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
        col = gameObject.GetComponent<Collider>();
        SoundManager.THIS.PlaySound(0);
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

    private void DestroyThis()
    {
        Destroy(gameObject);
    }

    private void Respawn() {
        anim.SetBool("despawn", false);
        SoundManager.THIS.PlaySound(0);
        col.enabled = true;
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.GetComponentInChildren<PlayerMovement>() != null) {
            GameObject clon = Instantiate(ps, new Vector3 (transform.position.x, other.transform.position.y, 1f), Quaternion.identity);
            Destroy(clon, 2f);
            print("s");
        }
    }

    private void OnCollisionExit(Collision other) {
        if (other.gameObject.GetComponentInChildren<PlayerMovement>() != null) {
            GameObject clon = Instantiate(ps, new Vector3(transform.position.x, other.transform.position.y, 1f), Quaternion.identity);
            Destroy(clon, 2f);
        }
    }
}
