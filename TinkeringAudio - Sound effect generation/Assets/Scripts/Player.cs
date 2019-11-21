using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody RB;

    public AudioTinker tinkerScript;
    public GameObject bullet;
    public GameObject shootPoint;
    public Animator doorAnim;

     bool heartSound = true;

    void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        if (Input.GetKey(KeyCode.W))
        {
            
        }

        if(heartSound)
        {
            StartCoroutine(heartbeatSound());
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
            tinkerScript.PlayOutAudio(1000000, 0.25f);
           
        }
        else if (other.gameObject.CompareTag("Door"))
        {
            doorAnim.SetTrigger("OpenDoor");
            Destroy(other.gameObject.GetComponent<Collider>());
            tinkerScript.PlayOutAudio(60, 1.25f);

        }
    }

    private void Shoot()
    {
        Instantiate(bullet, shootPoint.transform.position, shootPoint.transform.rotation);
        tinkerScript.PlayOutAudio(70, 0.25f);
    }

    IEnumerator heartbeatSound()
    {
        tinkerScript.PlayOutAudio(20, 0.25f);
        heartSound = false;
        yield return new WaitForSeconds(1);
        heartSound = true;
    }

}