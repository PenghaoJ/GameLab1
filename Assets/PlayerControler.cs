using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerControler : MonoBehaviour
{
    public GameManager gameManager;
    private Animator animator;
    private Vector2 oldLocation;
    public int speed = 5;
    //Total capacity of aircraft
    public int capacity = 3;
    //Current number of persons on board the aircraft
    private int count = 0;
    private AudioSource audio;
    public AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        oldLocation = transform.position;
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2 (x, y);
        transform.Translate(dir*Time.deltaTime*speed);
        if (x > 0)
        {
            animator.SetBool("back", false);
        }
        else if(x < 0)
        {
            animator.SetBool("back", true);
        }
        //Reset the game if R is pressed
        if (Input.GetKeyDown(KeyCode.R)) {
            Reset();
            gameManager.Reset();
        }
    }
    private void Reset()
    {
        count = 0;
        transform.position = oldLocation;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == 3)
        {
            gameManager.GameOver();

        }else if(collision.gameObject.tag == "Soldier")
        {
            //Determine if the number of people currently on board is less than or equal to the maximum capacity of the aircraft,
            //if it is then the number of people on board is +1 and the game manager is notified and the collided soldiers are destroyed
            if (count<capacity)
            {
                count++;
                gameManager.SetCount(count);
                collision.gameObject.SetActive(false);
                audio.PlayOneShot(clip);
            }
        }else if(collision.gameObject.tag == "House")
        {
            //Determine the current number of people on the plane if ! = 0 then call the game manager to add points,
            //set the number of people on the plane to 0 and call the game manager to set the current number of people to 0.
            if (count != 0)
            {
                gameManager.AddScore(count);
                count = 0;
                gameManager.SetCount(count);
            }
        }
    }

}
