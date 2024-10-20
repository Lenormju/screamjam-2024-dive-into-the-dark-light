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
    }

    public void StartRunningMusic() {
        playerAudioSource.resource = playerRunningAudio;
        playerAudioSource.Play();
    }

    public void StartBangingDoorMusic() {
        playerAudioSource.resource = playerBangingDoorAudio;
        playerAudioSource.Play();
    }

}
