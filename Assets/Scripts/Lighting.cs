using System;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using UnityEngine;
using UnityEngine.Audio;

public class Lighting : MonoBehaviour
{
    Light _myLight;
    private System.Random _rnd = new System.Random();

    public int BorneMinToActivated = 5;
    public int BorneMaxToActivated = 10;

    public float SecondActivated = 1;

    public AudioSource thunderAudioSource = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {       
        _myLight = GetComponent<Light>();
        PlayWithLight(false);
    }

    float _countSeconds;
    int _activateSeconds;
    bool _isActivated;

    // Update is called once per frame
    void Update()
    {
        _countSeconds += Time.deltaTime;
        // si pas activé , active au bout de X secondes
        if (_isActivated == false && _countSeconds >= _activateSeconds){
            //Debug.Log("Bouuuuuh!");
            PlayWithLight(true);
        }
        // si activé, désactive au bout de X seconde
        else if(_isActivated == true && _countSeconds >= SecondActivated){
            //Debug.Log("You Cant See Me!");            
            PlayWithLight(false);
        }
    }

    private void PlayWithLight(bool toActivated){
        _countSeconds = 0;
        _isActivated = toActivated;
        _myLight.enabled = toActivated;
        _activateSeconds = _rnd.Next(BorneMinToActivated, BorneMaxToActivated);
        if (toActivated) {
            System.Random rnd = new System.Random();
            float delay  = (float) (rnd.NextDouble() * 2) + 1;  // between 1 and 3
            thunderAudioSource.Play((ulong) (delay * 44100)); // delay in samping rate
        }
    }
}
