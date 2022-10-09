using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
 

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform PlayerPosition;
    [Range(0, 1)]
    public float Health;
    [SerializeField] private float DistanseAttack;
    
    [SerializeField] private float  Speed;
    private Animator EnemyAnimator;
    private Rigidbody rb;
    [SerializeField] private Collider Weapon;
   
  

    public void EnemyDamagedAnim()
    {
        StopAllCoroutines();

        EnemyAnimator.SetTrigger("Damage");

        StartCoroutine(RunEnemy());
    
    
    }
    private void Awake()
    {
         
        EnemyAnimator = GetComponent<Animator>();

        rb = GetComponent<Rigidbody>();

        PlayerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
      
    }
    
    

    public IEnumerator RunEnemy()
    {
        EnemyAnimator.SetBool("Run", true);

       

         

        while (Vector3.Distance(transform.position,PlayerPosition.position) > DistanseAttack )
        {
            
         

            transform.LookAt(PlayerPosition);


            rb.velocity = Vector3.forward*-1 * Speed;



            yield return null;
        }



 

        EnemyAnimator.SetBool("Run", false);

        EnemyAnimator.SetTrigger("Fight");

        

        StopAllCoroutines();

        StartCoroutine(HitEnemy());




    }


    private IEnumerator HitEnemy()
    {


        while (true)
        {
            

            if (Vector3.Distance(transform.position, PlayerPosition.position) > DistanseAttack)
            {
             

                 EnemyAnimator.SetBool("Run",! false);

        



                StartCoroutine(RunEnemy());

                StopCoroutine(HitEnemy());
            }

            yield return null;
        }
       


       







    }
 public void isHitedAnim()
    {

        Physics.IgnoreCollision(PlayerPosition.GetComponent<Collider>(), Weapon, ignore: false);
    }
    public void GetDamage()
    {

        Health -= 0.35F;

        if (Health < 0)
        {
            EnemyAnimator.SetTrigger("Die");

            Destroy(this.gameObject.GetComponent<Enemy>());

            Destroy(gameObject, 3f);

            EventManager.KilledUnit.Invoke();
        }


    }
}
