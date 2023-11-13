using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    AudioSource m_AudioSource;

    public AudioClip pointsAddedClip;
    public AudioClip BasketAddedClip;
    public AudioClip BasketDestroyedClip;

    private void Awake()
    {
        Instance = this;
        m_AudioSource = GetComponent<AudioSource>();
    }

    public void PlayAudio(AudioClip clip)
    {
        m_AudioSource.PlayOneShot(clip);
    }
}
