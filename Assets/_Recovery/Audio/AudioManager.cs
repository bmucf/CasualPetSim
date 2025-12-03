using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource sfxSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
