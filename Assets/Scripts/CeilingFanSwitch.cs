using UnityEngine;

public class CeilingFanSwitch : Interactable
{
    [SerializeField] private CeilingFan fan;
    [SerializeField] private AudioClip switchOnClip, switchOffClip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void interact() {
        if(fan.GetComponent<Collider2D>().enabled) {
            SoundFXManager.instance.PlaySoundFXClip(switchOffClip, transform, 1f);
            fan.turnOff();
        }
        else{
            SoundFXManager.instance.PlaySoundFXClip(switchOnClip, transform, 1f);
            fan.turnOn();
        }
        Debug.Log("switch");
    }
}
