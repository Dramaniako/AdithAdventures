using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float mMovementSpeed = 3.0f; //kecepatan gerak musuh
    public bool bIsGoingRight = true; //apakah musuh bergerak ke kanan
    private SpriteRenderer _mSpriteRenderer; //mengambil unsur SpriteRenderer dari objek dan mengkategorikannya sebagai _mSpriteRendere
    public float mRaycastingDistance = 1f; //jarak Raycasting akan dilakukan

    public float health = 100f; //darah musuh

    void Start() //dipanggil tiap frame
    {
        _mSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _mSpriteRenderer.flipX = !bIsGoingRight;
    }

    void Update()
    {
        Vector3 direction = (bIsGoingRight) ? transform.right : -transform.right;
        direction *= Time.deltaTime * mMovementSpeed;
        transform.Translate(direction);
        CheckWalls();
    }
    private void CheckWalls()
    {
        Vector3 raycast = (bIsGoingRight) ? Vector3.right : Vector3.left;
        RaycastHit2D hit = Physics2D.Raycast(transform.position + raycast * mRaycastingDistance - new Vector3(0f, 0.25f, 0f), raycast, 0.075f);
        if (hit.collider != null)
        {
            if (hit.transform.tag == "Terrain" || hit.transform.tag == "Ground")
            {
                _mSpriteRenderer.flipX = bIsGoingRight;
                bIsGoingRight = !bIsGoingRight;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D Collision) {
        if (Collision.gameObject.tag == "Projectile")
        {
            health -= 20f;
            if (health <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }

}

