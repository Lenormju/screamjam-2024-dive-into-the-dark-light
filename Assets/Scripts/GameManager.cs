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
    public GlobalMusic globalMusic;


    [SerializeField] private Animator deathAnim;
    public void DisplayEndScreen()
    {
        Debug.Log("fin du  game");
        deathAnim.SetTrigger("displayGameOver");
        SetCursorActive(true);
    }
    public int nb_keys = 0;

    public void SetCursorActive(bool active)
    {
        if (!active) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        } else {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
    }

    public void StartAmbientMusic() {
       globalMusic.StartAmbientMusic();
    }

    public void StartRunningMusic() {
        globalMusic.StartRunningMusic();
    }

    public void StartBangingDoorMusic() {
        globalMusic.StartBangingDoorMusic();
    }

}
