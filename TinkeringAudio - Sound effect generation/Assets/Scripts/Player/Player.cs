using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody RB;

    public AudioTinker tinkerScript;
    public Animator doorAnim;

    bool heartSound = true;
    bool alarmSound = false;
    public GameObject alarmLight;

    void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(heartSound)
        {
            StartCoroutine(heartbeatSound());
        }
        else if(alarmSound)
        {
            StartCoroutine(alarmSoundDelay());
        }
        
    }

    void FixedUpdate()
    {
        Move();
    }
    
    private void Move()
    {
        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(hAxis, 0, vAxis) * speed * Time.fixedDeltaTime;

        Vector3 newPosition = RB.position + RB.transform.TransformDirection(movement);

        RB.MovePosition(newPosition);
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            Destroy(other.gameObject);
            tinkerScript.PlayOutAudio(1000000, 0.25f, 70000);
           
        }
        else if (other.gameObject.CompareTag("Door"))
        {
            doorAnim.SetTrigger("OpenDoor");
            Destroy(other.gameObject.GetComponent<Collider>());
            tinkerScript.PlayOutAudio(75, 1.25f, 70000);

        }
        else if (other.gameObject.CompareTag("Scanner"))
        {
            Destroy(other.gameObject.GetComponent<Collider>());
            alarmSound = true;
        }
    }

    IEnumerator alarmSoundDelay()
    {
        tinkerScript.PlayOutAudio(2000, 0.7f, 70000);
        alarmSound = false;
        alarmLight.SetActive(true);
        yield return new WaitForSeconds(1);
        alarmSound = true;
    }

    //Here is where I created the hearbeat sound by just using a delay
    IEnumerator heartbeatSound()
    {
        tinkerScript.PlayOutAudio(20, 0.25f, 70000);
        heartSound = false;
        yield return new WaitForSeconds(1);
        heartSound = true;
    }

}