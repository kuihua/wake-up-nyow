using UnityEngine;

public class AlarmClock : MonoBehaviour
{
    float timeItRingsFor = 10;
    float ringCycleTime = 60;
    float timer = 30;
    bool ringing = false;
    [SerializeField] private AudioClip alarmClip;

    private Animator animator;
    private string currentState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(!ringing && timer > ringCycleTime) {
            SoundFXManager.instance.PlaySoundFXClip(alarmClip, transform, 0.75f);
            timer = 0;
            ringing = true;
            Meter.instance.addValue(30);
            Meter.instance.increaseMinValue(30);
            ChangeAnimationState("AlarmClock_Ring");
        }
        else if(ringing && timer > timeItRingsFor) {
            ringing = false;
            Meter.instance.increaseMinValue(-30);
            ChangeAnimationState("AlarmClock_Normal");
        }
    }

    public void ChangeAnimationState(string newState) {
        if(currentState == newState) return;
        animator.Play(newState);
        currentState = newState;
    }
}
