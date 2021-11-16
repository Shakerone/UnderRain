using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float minSpeed;
    public float maxSpeed;
    float speed;

    public GameObject explosion;
    public GameObject spawner;

    Player playerScript;
    public int damage;
    void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);
        if (GameObject.FindWithTag("Player")!=null)
            playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D hitObject)
    {
        if (hitObject.tag == "Player")
        {
            playerScript.TakeDamage(damage);
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (hitObject.tag == "Ground")
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
            if (GameObject.FindWithTag("Player") != null)
                playerScript.CounterPlus();
        }
    }
}
