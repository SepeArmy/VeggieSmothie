using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedCollectable : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private SeedSelector seedSelector;
    [SerializeField] private SeedSelector.ESeeds seed;

    private void Awake() {
        seedSelector = player.GetComponentInChildren<SeedSelector>();
    }
    private void OnTriggerEnter(Collider other) {

        if(other.gameObject.GetComponentInChildren<PlayerMovement>() != null) {
            seedSelector.UnlockSeed((int)seed);
            SoundManager.THIS.PlaySound(10);
            Destroy(gameObject);
        }

    }
}
