using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    // Kecepatan gerak musuh
    public float mMovementSpeed = 3.0f; 

    // Menentukan apakah musuh bergerak ke kanan
    public bool bIsGoingRight = true;

    // Mengambil komponen SpriteRenderer dari objek dan menyimpannya dalam variabel _mSpriteRenderer
    private SpriteRenderer _mSpriteRenderer;

    // Jarak Raycasting yang akan dilakukan untuk mendeteksi dinding atau halangan
    public float mRaycastingDistance = 1f;

    // Referensi ke script EnemyHealth yang mengelola kesehatan musuh
    public EnemyHealth enemyHealth;

    // Dipanggil saat objek pertama kali diinisialisasi
    void Start() 
    {
        // Mengambil komponen SpriteRenderer dari objek ini
        _mSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        // Membalikkan tampilan sprite musuh berdasarkan arah pergerakan
        _mSpriteRenderer.flipX = !bIsGoingRight;
    }

    // Dipanggil setiap frame untuk memperbarui posisi musuh
    void Update()
    {
        // Menentukan arah pergerakan musuh (kanan atau kiri) berdasarkan nilai bIsGoingRight
        Vector3 direction = (bIsGoingRight) ? transform.right : -transform.right;

        // Mengalikan arah dengan kecepatan gerak dan waktu frame untuk pergerakan yang mulus
        direction *= Time.deltaTime * mMovementSpeed;

        // Menggerakkan musuh sesuai arah yang ditentukan
        transform.Translate(direction);

        // Memeriksa apakah musuh bertemu dengan dinding atau halangan
        CheckWalls();
    }

    // Memeriksa apakah musuh bertemu dengan dinding atau halangan menggunakan raycasting
    private void CheckWalls()
    {
        // Menentukan arah raycast berdasarkan arah musuh
        Vector3 raycast = (bIsGoingRight) ? Vector3.right : Vector3.left;

        // Melakukan raycast di depan musuh untuk mendeteksi adanya halangan
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position + raycast * mRaycastingDistance - new Vector3(0f, 0.25f, 0f), 
            raycast, 
            0.075f
        );

        // Jika raycast mengenai objek dan objek tersebut adalah Terrain atau Ground, maka musuh berbalik arah
        if (hit.collider != null)
        {
            if (hit.transform.tag == "Terrain" || hit.transform.tag == "Ground")
            {
                _mSpriteRenderer.flipX = bIsGoingRight; // Membalikkan tampilan sprite
                bIsGoingRight = !bIsGoingRight; // Membalikkan arah gerakan
            }
        }
    }

    // Dipanggil saat musuh bertabrakan dengan objek lain
    private void OnCollisionEnter2D(Collision2D Collision) 
    {
        // Mengecek apakah objek yang bertabrakan adalah proyektil
        if (Collision.gameObject.tag == "Projectile")
        {
            // Mengambil referensi ke script EnemyHealth
            enemyHealth = GetComponent<EnemyHealth>();

            // Mengurangi kesehatan musuh ketika terkena proyektil
            enemyHealth.SetHealth(enemyHealth.currentHealth - 20f);

            // Jika kesehatan musuh habis, musuh dihancurkan
            if (enemyHealth.currentHealth <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
