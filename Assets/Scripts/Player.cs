using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Text healthDisplay;
    public Text counterDisplay;
    public float speed;
    public Joystick joystick;
    private float input;
    public GameObject body;
    public GameObject losePanel;
    public GameObject winPanel;
    public GameObject joysticPanel;
    AudioSource source;
    //YandexSDK sdk;

    Rigidbody2D rb;
    Animator anim;
    public int health;
    public int counter;

    public float startDashTime;
    private float dashTime;
    public float extraSpeed;
    private bool isDashing;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        healthDisplay.text = health.ToString();
        counterDisplay.text = counter.ToString();
    }

    private void Update()
    {
        if (input != 0)
        {
            anim.SetBool("isRunning", true);
        }
        else
            anim.SetBool("isRunning", false);
        if (input > 0)
        {
            anim.SetBool("isLeft", false);
        }
        else if (input < 0)
        {
            anim.SetBool("isLeft", true);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isDashing == false)
        {
            speed += extraSpeed;
            isDashing = true;
            dashTime = startDashTime;
        }

        if (dashTime <= 0 && isDashing == true)
        {
            isDashing = false;
            speed -= extraSpeed;
        }
        else
        {
            dashTime -= Time.deltaTime;
        }
    }

    public void OnButton()
    {
        if (isDashing == false)
        {
            speed += extraSpeed;
            isDashing = true;
            dashTime = startDashTime;
        }
    }
    public void TakeDamage(int damageAmount)
    {
        source.Play();
        health -= damageAmount;
        if (health < 0)
            health = 0;
        healthDisplay.text = health.ToString();
        if (health <= 0)
        {
            losePanel.SetActive(true);
            Destroy(gameObject);
            joysticPanel.SetActive(false);
           // sdk.ShowInterstitial();
        }
    }

    public void CounterPlus() { 
        counter++;
        counterDisplay.text = counter.ToString();
        if (counter == 360)
        {
            SceneManager.LoadScene("Win");
           // winPanel.SetActive(true);
          //  Destroy(gameObject);
        }
       // source.Pause();
    }

    void FixedUpdate()
    {
        input = Input.GetAxisRaw("Horizontal");
        if (input==0)
            input = joystick.Horizontal;
        rb.velocity = new Vector2(input * speed, rb.velocity.y);
    }
}
