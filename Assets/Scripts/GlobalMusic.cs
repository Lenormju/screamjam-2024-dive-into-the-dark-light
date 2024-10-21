using UnityEngine;

public class GlobalMusic : MonoBehaviour
{

    public AudioSource playerAudioSource;
    public AudioClip ambientAudio;
    public AudioClip playerRunningAudio;
    public AudioClip playerBangingDoorAudio;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartAmbientMusic() {
        playerAudioSource.resource = ambientAudio;
        playerAudioSource.Play();
        playerAudioSource.volume = 1.0f;
        playerAudioSource.loop = true;
    }

    public void StartRunningMusic() {
        playerAudioSource.resource = playerRunningAudio;
        playerAudioSource.Play();
        playerAudioSource.volume = 0.2f;
        playerAudioSource.loop = false;
    }

    public void StartBangingDoorMusic() {
        playerAudioSource.resource = playerBangingDoorAudio;
        playerAudioSource.Play();
        playerAudioSource.volume = 0.25f;
        playerAudioSource.loop = false;
    }

}
