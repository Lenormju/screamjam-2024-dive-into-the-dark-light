using System;
using UnityEngine;

public class keyCollection : MonoBehaviour
{

    private void Start()
    {
        GameManager.Instance.SetNbKey(0);
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.transform.tag == "Key")
        {
            Debug.Log("found a key");
            GameManager.Instance.AddKey();

            if (other.gameObject.name == "Key1"){
                GameManager.Instance.GotKey1 = true;
            } else if (other.gameObject.name == "Key2"){
                GameManager.Instance.GotKey2 = true;
                GameManager.Instance.InvokeKey2();
            } else if (other.gameObject.name == "Key3"){
                GameManager.Instance.GotKey3 = true;
            }

            Destroy(other.gameObject);
        }
    }
}
