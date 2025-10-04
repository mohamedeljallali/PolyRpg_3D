using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{

    CharacterStats stats;
    CharacterController controller;
    Transform cam;
    Animator anim;
    public GameObject restartPanel;
    
    float verticalVelocity = 0;
    
    public float speed;
    public float gravity = 15;
    public float jump;
    float sprint;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main.transform;
        anim = GetComponentInChildren<Animator>();
        stats = GetComponent<CharacterStats>();
    }

    
    void Update()
    {
     
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //Sprint
        bool isSprint = Input.GetKey(KeyCode.LeftShift);
        float sprint = isSprint ? 1.7f : 1;                 //if isSprint true => sprint = 13, else => sprint = 1


        //IF Player not Dead
        if (!LevelManager.Instance.playerDead)
        {
            //Player Movement

            //Walk
            Vector3 moveDirection = new Vector3(horizontal, 0, vertical);
            //Sprint
            anim.SetFloat("Speed", Mathf.Clamp(moveDirection.magnitude, 0, 0.5f) + (isSprint ? 0.5f : 0));
            //Attack
            if (Input.GetMouseButtonDown(0))
                anim.SetTrigger("Attack");


            //Player Rotation
            float angle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            if (moveDirection.magnitude > 0.1f)
            {
                transform.rotation = Quaternion.Euler(0, angle, 0);

                //Player Rotation To Camera Direction
                moveDirection = cam.TransformDirection(moveDirection);
            }


            //Jump
            if (controller.isGrounded)
            {
                if (Input.GetAxis("Jump") > 0)
                    verticalVelocity = jump;
            }
            //Gravity
            else
                verticalVelocity -= gravity * Time.deltaTime;


            //Controle
            moveDirection = new Vector3(moveDirection.x * speed * sprint, verticalVelocity, moveDirection.z * speed * sprint);
            controller.Move(moveDirection * Time.deltaTime);
        }

        else
        {
            anim.Play("Die_SwordShield");
            restartPanel.SetActive(true);
        }
            
            


        if(Input.GetKey(KeyCode.Escape))
            ExitGame();
        if (Input.GetKey(KeyCode.R))
            Restart();


        


    }


    //Collider Enable & Disable
    public void EnableCollider()
    {
        transform.Find("sword").GetComponent<BoxCollider>().enabled = true;
        StartCoroutine(DisableCollider());
    }
    
    IEnumerator DisableCollider()
    {
        yield return new WaitForSeconds(0.5f);
        transform.Find("sword").GetComponent<BoxCollider>().enabled = false;
    }


    
    public void OnTriggerEnter(Collider other)
    {
        //Items Collect
        if (other.CompareTag("Health"))
        {
            LevelManager.Instance.PlaySound(LevelManager.Instance.levelSounds[3], LevelManager.Instance.player.position);
            Instantiate(LevelManager.Instance.particleSystem[0], other.transform.position, other.transform.rotation);
            stats.ChangeHealth(30);
            Destroy(other.gameObject);
        }

        else if (other.CompareTag("Item"))
        {
            LevelManager.Instance.PlaySound(LevelManager.Instance.levelSounds[4], LevelManager.Instance.player.position);
            Instantiate(LevelManager.Instance.particleSystem[1], other.transform.position, other.transform.rotation);
            LevelManager.Instance.levelItems += 10;
            Debug.Log("Score : " + LevelManager.Instance.levelItems);
            Destroy(other.gameObject);
        }

        //Player Get Hit

        else if (other.CompareTag("Axe"))
        {
            stats.ChangeHealth(-other.GetComponentInParent<CharacterStats>().power);
            LevelManager.Instance.PlaySound(LevelManager.Instance.levelSounds[6],  gameObject.transform.position);

        }
    }

    public void ExitGame()
    {
            Application.Quit();
            Debug.Log("Game Exit");
            //Time.timeScale = 0;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);

    }

}