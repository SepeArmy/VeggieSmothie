using UnityEngine;

/// <summary>
/// 
/// DESCRIPCION: Gestor del hilo musical
/// 
/// </summary>

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public static MusicManager THIS;

    AudioSource audioSource;

    public AudioClip[] musics;

    private void Awake()
    {
        THIS = this;
        audioSource = GetComponent<AudioSource>();

        audioSource.loop = false;
        audioSource.playOnAwake = false;
    }

    public void MusicPlay(bool _isLooping, int _index)
    {
        audioSource.clip = musics[_index];
        audioSource.loop = _isLooping;

        audioSource.Play();
    }

    public void MusicStop()
    {
        if (audioSource.isPlaying) audioSource.Stop();
    }
}
