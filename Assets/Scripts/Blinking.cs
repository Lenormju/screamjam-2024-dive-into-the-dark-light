using UnityEngine;

public class Blinking : MonoBehaviour
{
    private Light _torchLight;
    private float timerBetweenFailingLights = 15;
    private float timerBetweenBlinks = 666;
    private int currentBlink = 0;
    private int maxBlink = 4;
    private bool isBlinking = false;

    [SerializeField] private float minTimeBlink = 0.02f;
    [SerializeField] private float maxTimeBlink = 0.08f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _torchLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {    
        timerBetweenFailingLights-= Time.deltaTime;
        if (timerBetweenFailingLights <= 0){
            StartBlink();
        }

        timerBetweenBlinks-= Time.deltaTime;
        if (isBlinking && timerBetweenBlinks <= 0){
            Blink();
        }
    }

    private bool isLightOn = true;
    void StartBlink()
    {    
        currentBlink = 0;
        Blink();
        timerBetweenFailingLights = Random.Range(8f, 15f);
        isBlinking = true;
    }

    void Blink(){
        currentBlink++;
        timerBetweenBlinks = Random.Range(minTimeBlink, maxTimeBlink);
        
        isLightOn = !isLightOn;
        _torchLight.intensity = isLightOn ? 50 : 0;

        if (currentBlink >= 8){
            isBlinking = false;
        }
    }
}
