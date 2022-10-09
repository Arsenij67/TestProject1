using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
   
    [SerializeField] private float speed;

    [SerializeField] private Gun gun;
    public Camera cam;
    private Animator PlayerAnimator;

    public List<AudioClip> Clips = new List<AudioClip>(3);

   private Enemy enemy;
    

    private AudioSource Source;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        PlayerAnimator = GetComponent<Animator>();

        Source = GetComponent<AudioSource>();

        EventManager.MissionCompleteAudio.AddListener(MissionCompleteAudio);
        
    }
   
    
    private void Update()
    {
         

        MovePlayer();
        ShootPlayer();

    }

    void ShootPlayer()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;
            

            if (Physics.Raycast(ray, out hitInfo,1000f))
            {
                Debug.DrawLine(cam.transform.position, hitInfo.point, Color.red);


                if (hitInfo.transform.tag.Equals("Enemy"))
                {
                    enemy = hitInfo.transform.gameObject.GetComponent<Enemy>();

                    PlayerAnimator.SetTrigger("Shoot");

                    if (enemy != null)
                    {
                        enemy.GetDamage();

                        enemy.EnemyDamagedAnim();
                    }
                   

                    transform.LookAt(hitInfo.transform.position);

                    StartCoroutine(gun.StartShot(ray));

                     

                    Source.Play();


                }
              

                

                 


            }
          
        }
    }

    void MovePlayer()
    {
       

        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = transform.forward * Time.deltaTime * speed;

            SetRotation(-90);

        }

        else if (Input.GetKey(KeyCode.S))
        {
          

            SetRotation(90);
            rb.velocity = transform.forward * Time.deltaTime * speed;

        }
        else if (Input.GetKey(KeyCode.A))
        {
            
            SetRotation(-180);

            rb.velocity = transform.forward * Time.deltaTime * speed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            
            rb.velocity = transform.forward * Time.deltaTime * speed;
            SetRotation(0);
        }
        else {

            PlayerAnimator.SetBool("Run", !true);

        }


 
    }
    void SetRotation( float AngleY)
    {

        PlayerAnimator.SetBool("Run", true);

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, AngleY, 0), 5f * Time.deltaTime);


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag.Equals("Weapon"))
        {

         PlayerAnimator.SetTrigger("Damage");
             
            Source.PlayOneShot(Clips[1]);

            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), collision.gameObject.GetComponent<Collider>(), ignore: true);




        }
    }
    private void MissionCompleteAudio()
    {

        Source.PlayOneShot(Clips[2]);



    }

}
public static class EventManager
{


    public static UnityEvent KilledUnit =  new UnityEvent();
    public static UnityEvent MissionCompleteAudio = new UnityEvent();


}

 

    


 

