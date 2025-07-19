using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CurtainInteract : Interactable
{
    [SerializeField] private PlayableDirector playableDirector;

    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    public override void interact() {
        playableDirector.Play();
        // gameObject.SetActive(false);
        GetComponent<Collider2D>().enabled = false;
    }

    public void increaseMeter() {
        Meter.instance.addValue(30);
        Meter.instance.increaseMinValue(20);
    }
}
