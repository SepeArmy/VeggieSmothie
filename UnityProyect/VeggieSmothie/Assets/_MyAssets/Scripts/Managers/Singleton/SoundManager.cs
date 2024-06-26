using UnityEngine;

/// <summary>
/// 
/// DESCRIPCION: Gestor de los efectos de sonido
/// 
/// </summary>

[RequireComponent (typeof (AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager THIS;

    AudioSource audioSource;

    public AudioClip[] sounds;

    // Start is called before the first frame update
    void Awake()
    {
        THIS = this;
        audioSource = GetComponent<AudioSource>();

        audioSource.loop = false;
        audioSource.playOnAwake = false;
    }

    public void PlaySound(int index)
    {
        audioSource.PlayOneShot (sounds[index]);
    }
}