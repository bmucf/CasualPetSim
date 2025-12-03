using UnityEngine;

public class BowlDing : MonoBehaviour
{
    private AudioSource bowlAudio;
    public AudioClip bowlSFX;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bowlAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Food"))
        {
            bowlAudio.PlayOneShot(bowlSFX);
        }
    }
}
