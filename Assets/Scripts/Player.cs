using UnityEngine;
using System;
using System.Threading.Tasks;

public class Player : MonoBehaviour
{
    public static bool alive;

    [SerializeField] private float speedDownGain;
    [SerializeField] private float speedUpDecrease;
    [SerializeField] private float defaultSpeedUp;
    [SerializeField] private float defaultSpeedDown;
    [SerializeField] private int delayInTopPosition;

    public AudioClip slap, mentronom;
    private AudioSource audio;
    private Rigidbody rb;
    private MeshRenderer m_r;
    public State state;
    private TypeEvent typeEvent;
    private RaycastHit hit;
    private float speedDown;
    private float speedUp;
    private int currentGainToScore = 1;
    private int untouchablePlatformCount = 0;
    private Pipe pipe;

    public bool powerUpPlatrofmDestroyerIsActive = false;
    private int counter = 0;

    public delegate void DetectPlayer(TypeEvent typeEvent, Collider collider);
    public static event DetectPlayer DetectEvent;

    private void Start()
    {
        alive = true;
        pipe = GameObject.Find("Pipe").GetComponent<Pipe>();
        audio = GetComponent<AudioSource>();
        speedDown = defaultSpeedDown;
        speedUp = defaultSpeedUp;
        rb = GetComponent<Rigidbody>();
        m_r = GetComponent<MeshRenderer>();
    }

    private float getDownAcceleration(){
        if(speedDown < 9)
            return speedDown += speedDownGain;
        else
            return speedDown;
    }

    private float getUpAcceleration(){
        if(speedUp <= 1)
            return speedUp;
        else
            return speedUp -= speedUpDecrease;
    }

    private void FixedUpdate() 
    {
        if (alive)
        {
            Physics.Raycast(transform.position, Vector3.down, out hit, .3f);
            
            if (counter == 10)
            {
                counter = 0;
                powerUpPlatrofmDestroyerIsActive = false;
                pipe.ChangeSpeedPlatforms(8);
                state = State.BOUNCE;
                AccelerationEffect.ResetTrails();
                
            }
            else if(counter == 9)
            {
                GameObject.Find("Camera").GetComponent<CameraController>().ChangeReset();
            }

            if (hit.collider != null)
            {

                if (powerUpPlatrofmDestroyerIsActive)
                {
                    state = State.IDLE;
                    speedDown = defaultSpeedDown;
                    currentGainToScore = 5;

                    if (hit.collider.tag == "Ground")
                    {
                        AccelerationEffect.IncreaseOpacityTrails(0.02f);
                        AccelerationEffect.IncreaseSpeedTrails(1);
                        DetectEvent(TypeEvent.Abyss, hit.collider);
                        Trail.ShowTrail();
                        pipe.ChangeSpeedPlatforms(15);
                        untouchablePlatformCount = 0;
                        GainScore.changeGain(currentGainToScore * UIMultiplierScore.currentMultiplier * GameObject.Find("Concentration").GetComponent<Concentration>().Multiplier());
                        GameObject.Find("Camera").GetComponent<CameraController>().IncreaseTheEffectOfAcceleration();
                    }

                    if (hit.collider.tag == "Let")
                    {
                        AccelerationEffect.IncreaseOpacityTrails(0.02f);
                        AccelerationEffect.IncreaseSpeedTrails(1);
                        Trail.ShowTrail();
                        DetectEvent(TypeEvent.Abyss, hit.collider);
                        pipe.ChangeSpeedPlatforms(15);
                        untouchablePlatformCount = 0;
                        GainScore.changeGain(currentGainToScore * UIMultiplierScore.currentMultiplier * GameObject.Find("Concentration").GetComponent<Concentration>().Multiplier());
                        GameObject.Find("Camera").GetComponent<CameraController>().IncreaseTheEffectOfAcceleration();
                    }

                    if (hit.collider.tag == "Abyss")
                    {
                        AccelerationEffect.IncreaseOpacityTrails(0.02f);
                        AccelerationEffect.IncreaseSpeedTrails(1);
                        Trail.ShowTrail();
                        DetectEvent(TypeEvent.Abyss, hit.collider);
                        pipe.ChangeSpeedPlatforms(15);
                        untouchablePlatformCount = 0;
                        GainScore.changeGain(currentGainToScore * UIMultiplierScore.currentMultiplier * GameObject.Find("Concentration").GetComponent<Concentration>().Multiplier());
                        GameObject.Find("Camera").GetComponent<CameraController>().IncreaseTheEffectOfAcceleration();
                    }
                    audio.clip = mentronom;
                    audio.Play();
                    counter++;
                }
                else
                {
                    if (hit.collider.tag == "Ground")
                    {
                        audio.clip = slap;
                        audio.Play();
                        state = State.BOUNCE;
                        speedDown = defaultSpeedDown;
                        currentGainToScore = 1;

                        AccelerationEffect.ResetTrails();

                        if (untouchablePlatformCount >= 3)
                        {
                            DetectEvent(TypeEvent.Abyss, hit.collider);
                            Trail.HideTrail();
                        }
                        else
                        {
                            Trail.HideTrail();
                            DetectEvent(TypeEvent.Ground, hit.collider);
                        }

                        hit.collider.gameObject.GetComponent<Ground>().PlayerTouch();
                        GameObject.Find("Camera").GetComponent<CameraController>().ChangeReset();
                        pipe.ChangeSpeedPlatforms(false);
                        untouchablePlatformCount = 0;
                    }

                    if (hit.collider.tag == "Let")
                    {
                        audio.clip = slap;
                        audio.Play();

                        speedDown = defaultSpeedDown;
                        speedUp = defaultSpeedUp;
                        currentGainToScore = 1;
                        
                        if (untouchablePlatformCount >= 3)
                        {
                            state = State.BOUNCE;
                            DetectEvent(TypeEvent.Abyss, hit.collider);
                            Trail.HideTrail();
                        }
                        else
                        {
                            Trail.ShowTrail();
                            state = State.IDLE;
                            DetectEvent(TypeEvent.Let, hit.collider);
                        }
                        GameObject.Find("Camera").GetComponent<CameraController>().ChangeReset();
                        untouchablePlatformCount = 0;
                        AccelerationEffect.ResetTrails();
                        pipe.ChangeSpeedPlatforms(false);
                    }

                    if (hit.collider.tag == "Abyss")
                    {
                        //gameObject.GetComponent<Сoncentration>().plusOneDestroyedPlatform();
                        audio.clip = mentronom;
                        audio.Play();

                        speedDown = defaultSpeedDown;
                        speedUp = defaultSpeedUp;
                        state = State.IDLE;
                        untouchablePlatformCount++;
 
                        GameObject.Find("Camera").GetComponent<CameraController>().IncreaseTheEffectOfAcceleration();
                        if (untouchablePlatformCount > 1)
                        {
                            AccelerationEffect.IncreaseOpacityTrails(0.02f);
                            AccelerationEffect.IncreaseSpeedTrails(1);
                        }

                        if (untouchablePlatformCount > 3)
                        {
                            pipe.ChangeSpeedPlatforms(true);
                            currentGainToScore = 3;
                            
                        }

                        if (untouchablePlatformCount > 5)
                        {
                            pipe.ChangeSpeedPlatforms(true);
                            currentGainToScore = 5;
                        }
                        Trail.ShowTrail();
                        DetectEvent(TypeEvent.Abyss, hit.collider);
                        GainScore.changeGain(currentGainToScore * UIMultiplierScore.currentMultiplier * GameObject.Find("Concentration").GetComponent<Concentration>().Multiplier());
                        if(Time.timeScale < 1.2f) Time.timeScale += 0.001f;
                        else { Debug.Log(Time.timeScale); }
                    }
                }
            }

            if (state == State.FALLING)
            {
                GameObject.Find("Camera").GetComponent<CameraController>().ChangeReset();
                transform.Translate((Vector3.down * getDownAcceleration()) * Time.fixedDeltaTime, Space.World);
            }
                

            if (state == State.BOUNCE)
            {
                Trail.HideTrail();
                transform.Translate((Vector3.up * getUpAcceleration()) * Time.fixedDeltaTime, Space.World);
                GameObject.Find("Camera").GetComponent<CameraController>().ChangeReset();
            }
        }
    }

    public void ResetPlayer()
    {
        
    }

    public void ContinueDetect()
    {
        
        alive = true;
        state = State.BOUNCE;
        DetectEvent(TypeEvent.Abyss, hit.collider);
        Trail.HideTrail();
    }

    private void SetTimeout(Action action, int delay) =>
    Task.Run(async () =>
    {
        await Task.Delay(delay);
        action();
    });

    private void SetFalling()
    {
        state = State.FALLING;
    }

    private void OnTriggerEnter(Collider collider) {
        if(collider.tag == "BounceTrigger")
        {
            state = State.IDLE;
            speedUp = defaultSpeedUp;
            SetTimeout(() => SetFalling(), delayInTopPosition);
        }
    }

    public enum TypeEvent
    {   
        Abyss,
        Let,
        Ground
    }

    public enum State
    {
        FALLING,
        BOUNCE,
        IDLE
    }
}