using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Gun : MonoBehaviour
{
    [SerializeField] private ParticleSystem ShotEffect;
    [SerializeField] private GameObject PunchEff;
     
     

    // Update is called once per frame
    public IEnumerator StartShot(Ray EndDist)
    {
        
       
       
        {
            
          

            RaycastHit GunHit;

            if (Physics.Raycast(EndDist, out GunHit, 1000f))
            {
                if (GunHit.transform.tag.Equals("Enemy"))
                {
                    yield return new WaitForSeconds(0.3f);

                    ShotEffect.Play();
                   

                    GameObject Punch = Instantiate( PunchEff, GunHit.point, Quaternion.identity) as GameObject;

                    Punch.GetComponent<ParticleSystem>().Play();

                    Destroy(Punch, 0.2f);

                    StopCoroutine(StartShot(EndDist));
                
                    
                }
                Debug.DrawLine(transform.position, GunHit.point,Color.green);

                 
            }
             

             
        
        
        }


        
    
    }
}
