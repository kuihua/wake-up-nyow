using UnityEngine;

public class KnockableObject : MonoBehaviour
{
    private Rigidbody2D rb;
    public bool knockedOver {get; private set;}
    private Vector2 previousVelocity;
    private Vector2 lastRecordedVelocity;

    [SerializeField] float hardnessValue = 1;
    [SerializeField] AudioClip soundClip;
    private float soundTimer = 0.5f;

    public bool muffled = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        knockedOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.E)) {
        //     knockOver(1);
        // }

        // if(knockedOver) {
        if (lastRecordedVelocity != null)
        {
            previousVelocity = lastRecordedVelocity;
        }
        lastRecordedVelocity = rb.velocity;
        // lastRecordedVelocity = rb.linearVelocity;
        // }

        if(soundTimer > 0) {
            soundTimer -= Time.deltaTime;
        }
    }

    public void knockOver(float direction) {
        PlatformCollisions.instance.disableCollisionWith(GetComponent<Collider2D>());
        rb.AddForce(new Vector2(direction * 5, 0), ForceMode2D.Impulse);
        rb.AddTorque(-1 * direction, ForceMode2D.Impulse);
        knockedOver = true;
    }

    public void knockOver(Vector2 playerVelocity, float direction) {
        PlatformCollisions.instance.disableCollisionWith(GetComponent<Collider2D>());
        if(playerVelocity.magnitude == 0) {
            rb.AddForce(new Vector2(direction * 5, 3), ForceMode2D.Impulse);
        }
        else {
            rb.AddForce(playerVelocity * 2, ForceMode2D.Impulse);
        }
        if(Mathf.Abs(playerVelocity.x) < 1) {
            rb.AddTorque(-1 * direction, ForceMode2D.Impulse);
        }
        else {
            rb.AddTorque(-1 * playerVelocity.x, ForceMode2D.Impulse);
        }
        knockedOver = true;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        // if(collision.gameObject.name == "Floor") {
        float awakenessValue = previousVelocity.magnitude * rb.mass
        // * 5 / Mathf.Pow(Vector2.Distance(transform.position, Meter.instance.humanPos.position), 2);
        * hardnessValue / Vector2.Distance(transform.position, Meter.instance.humanPos.position);
        if(muffled) {
            awakenessValue /= 10;
        }
        // if(previousVelocity != null) {
        //     Debug.Log(awakenessValue);
        // }
        Meter.instance.addValue(awakenessValue);
        if(soundClip != null && previousVelocity.magnitude > 1 && soundTimer <= 0) {
            float soundVolume = previousVelocity.magnitude * rb.mass * hardnessValue / 10;
            if(muffled) {
                soundVolume /= 10;
            }
            SoundFXManager.instance.PlaySoundFXClip(soundClip, transform, soundVolume);
            soundTimer = 0.2f;
            Debug.Log(soundVolume);
        }
        // }
    }
}
