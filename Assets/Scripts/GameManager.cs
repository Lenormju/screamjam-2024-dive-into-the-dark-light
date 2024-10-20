using TMPro.Examples;
using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour {

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
        set
        {
            _instance = value;
        }
    }

    void Awake() {
        _instance = FindFirstObjectByType<GameManager>();
    }

    public Player Player;
    public bool GotKey1 = false;
    public bool GotKey2 = false;
    public bool GotKey3 = false;
    public string currentGroundWalkingCategory = "none by default";  // the audio clip of the noisy groudn we are currently working on

    public void DisplayEndScreen()
    {
        Debug.Log("fin du  game");
    }
    public int nb_keys = 0;

}
