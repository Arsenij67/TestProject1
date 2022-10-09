using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class AskGirl : MonoBehaviour
{
    [SerializeField] private Image tips;

    [SerializeField] private ParticleSystem Ligt;
    private  bool Asked = false;
    private Animator GirlAnimator;

    [SerializeField] private GameObject enemyGO;

    [SerializeField] private Transform [] TargetsSpawn;

    [SerializeField] private Text TextTask;

    private int QuantityKilled = 0;

    private GameObject CameraGirl;

    [SerializeField] private GameObject CameraPlayer;

  
    private void Awake()
    {
        GirlAnimator = GetComponent<Animator>();

        EventManager.KilledUnit.AddListener(AddKilledEnemy);

        CameraGirl = gameObject.transform.GetChild(0).gameObject;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") && !Asked)
        {

            tips.gameObject.SetActive(true);
        
        
        
        }

        if (other.gameObject.tag.Equals("Player") && QuantityKilled == 3)
        {


            tips.gameObject.SetActive(true);



        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.F) && !Asked)
        {


            GirlAnimator.SetTrigger("isSpeaked");

            TextTask.gameObject.SetActive(true);

            Ligt.Play();

            Asked = true;

            CameraGirl.SetActive(true);
            CameraPlayer.SetActive(false);


            tips.gameObject.SetActive(false);
            



        }

        if (Input.GetKey(KeyCode.F) && QuantityKilled == 3)
        {

            GirlAnimator.SetTrigger("Dancing");

            QuantityKilled = 0;

            Ligt.Play();

            tips.gameObject.SetActive(false);

            EventManager.MissionCompleteAudio.Invoke();



        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") && !Asked)
        {

            tips.gameObject.SetActive(false);


        }
        if (other.gameObject.tag.Equals("Player") && QuantityKilled == 3)
        {

             tips.gameObject.SetActive(false);
        }


    }


   public void GirlFinishSpoked()
    {
        CameraGirl.SetActive(false);

        CameraPlayer.SetActive(true);

        for (int i = 0; i < TargetsSpawn.Length; i++)
        {
            GameObject Enemy = Instantiate(enemyGO, TargetsSpawn[i].position, Quaternion.identity);

            StartCoroutine(Enemy.GetComponent<Enemy>().RunEnemy());
        }

        
    }

    private void AddKilledEnemy()
    {
        QuantityKilled += 1;

        TextTask.text =string.Format($"{QuantityKilled}/3");
         
       
    
    
    }


}
