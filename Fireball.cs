using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 10f;

    public CircleCollider2D circleCollider;

    public bool attackRight;

    public float countTimer = 2;

    void Start()
    {
        circleCollider.enabled = false;
        StartCoroutine(nothit());
        attackRight = PlayerMovement.facingRight;
        Debug.Log(PlayerMovement.facingRight);
        
    }
    void Update()
    {
        if (countTimer <= 0)
        {
            Destroy(gameObject);
        }
        if (attackRight == true)
        {
            Vector3 direction = new Vector3(1f, 0f, 0f);
            transform.position += direction * Time.deltaTime * speed;
        }
        else
        {
            Vector3 direction = new Vector3(-1f, 0f, 0f);
            transform.position += direction * Time.deltaTime * speed;
        }
        countTimer -= Time.unscaledDeltaTime;
        
    }
   
    private void OnCollisionEnter2D(Collision2D Collision) {
        if (Collision.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }



    public IEnumerator nothit()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        circleCollider.enabled = true;
    }
}
