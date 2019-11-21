using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody RB;

    public AudioTinker tinkerScript;
    public GameObject bullet;
    public GameObject shootPoint;

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
            tinkerScript.PlayOutAudio(1000000, 1);
           
        }
    }

    void Shoot()
    {
        Instantiate(bullet, shootPoint.transform.position, shootPoint.transform.rotation);
        tinkerScript.PlayOutAudio(60, 1);
    }

}