using System.Collections;
using System.Collections.Generic;
 
using UnityEngine;

public class CameraFPS : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform PlayerPos;
    public float DistanceToPlayerY = 10;
    public float DistanceToPlayerX = 10;
    
    private float Xpos;
    private float Ypos;

    [SerializeField] private float SpeedRotation;
     
    private void Start()
    {

        Xpos = transform.rotation.x;
        Ypos = transform.rotation.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        RotateObject();

        

        transform.position = Vector3.Lerp(transform.position, PlayerPos.transform.position+new Vector3( DistanceToPlayerX, DistanceToPlayerY,0), 5f * Time.deltaTime);
    }

    public void RotateObject()
    {

         
        if (Input.GetMouseButton(0))
        {
            Xpos += Input.GetAxis("Mouse X");
            Ypos -= Input.GetAxis("Mouse Y");




            

            Ypos = Mathf.Clamp(Ypos, -19.092f, 42.408f);



            transform.rotation = Quaternion.Euler(Ypos * SpeedRotation, Xpos * SpeedRotation, transform.rotation.z);


        }



    }

  


   }
