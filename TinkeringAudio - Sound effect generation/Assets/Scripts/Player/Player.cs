using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Just a floatfor speed;
    [SerializeField] private float speed;

    // Reference for the rigidbody
    Rigidbody RB;

    // Public reference for the audio tinker script
    [SerializeField] private AudioTinker tinkerScript;
    [SerializeField] private Animator doorAnim;

    // bools for creating the repeating heartbeat sound and detector sound
    bool heartSound = true;
    bool alarmSound = false;

    // the gameobject of the light
    [SerializeField] private GameObject alarmLight;

    void Start()
    {
        // Method to grab the rigidbody 
        RB = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // two if statements to check the bools
        if(heartSound)
        {
            StartCoroutine(heartbeatSound());
        }
        else if(alarmSound)
        {
            StartCoroutine(alarmSoundDelay());
        }
        
    }

    #region Movement
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
    #endregion

    // A function for detecting trigger collisions on the objects to play sounds
    private void OnTriggerEnter(Collider other)
    {       
        if (other.gameObject.CompareTag("Pickup"))
        {
            Destroy(other.gameObject);
            // method to play the coin sound throughout the tinker script that will call the play out audio function
            tinkerScript.PlayOutAudio(1000000, 0.25f, 70000);
           
        }
        else if (other.gameObject.CompareTag("Door"))
        {
            // Setting animation trigger for opening the door
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

    // IEnumerator delay function for creating the replaying alarm sound 
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