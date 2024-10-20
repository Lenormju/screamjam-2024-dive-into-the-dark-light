using System;
using UnityEngine;
using UnityEngine.Audio;

public class PopEventKey : MonoBehaviour
{
    private GameObject MyObject;
    private AudioSource AudioSource;
    public bool SetActive;

    void Start()
    {
        MyObject = GetComponent<GameObject>();
        AudioSource = GetComponent<AudioSource>();

        GameManager.Instance.EventKey2 += DisplayObject;
    }

    private void DisplayObject(object sender, EventArgs e)
    {
        MyObject.SetActive(SetActive);
        AudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
