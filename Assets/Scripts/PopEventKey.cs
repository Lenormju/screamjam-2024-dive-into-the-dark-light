using System;
using UnityEngine;
using UnityEngine.Audio;

public class PopEventKey : MonoBehaviour
{
    public GameObject MyObject;
    public AudioSource AudioSource;
    public bool SetActive;

    void Start()
    {
        MyObject.SetActive(!SetActive);
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
