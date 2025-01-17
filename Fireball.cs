using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    // Kecepatan gerak fireball
    public float speed = 10f;

    // Mengambil komponen CircleCollider2D untuk fireball
    public CircleCollider2D circleCollider;

    // Menyimpan apakah fireball bergerak ke kanan
    public bool attackRight;

    // Waktu hitung mundur sebelum fireball dihancurkan
    public float countTimer = 2;

    // Dipanggil saat objek pertama kali diinisialisasi
    void Start()
    {
        // Menonaktifkan collider fireball pada awalnya
        circleCollider.enabled = false;

        // Memulai coroutine untuk menunggu sebelum collider diaktifkan
        StartCoroutine(nothit());

        // Menentukan arah serangan berdasarkan arah pemain
        attackRight = PlayerMovement.facingRight;
        Debug.Log(PlayerMovement.facingRight);
    }

    // Dipanggil setiap frame untuk menggerakkan fireball dan menghitung waktu
    void Update()
    {
        // Jika countTimer mencapai 0, hancurkan fireball
        if (countTimer <= 0)
        {
            Destroy(gameObject);
        }

        // Menggerakkan fireball sesuai dengan arah serangan
        if (attackRight == true)
        {
            Vector3 direction = new Vector3(1f, 0f, 0f); // Arah ke kanan
            transform.position += direction * Time.deltaTime * speed;
        }
        else
        {
            Vector3 direction = new Vector3(-1f, 0f, 0f); // Arah ke kiri
            transform.position += direction * Time.deltaTime * speed;
        }

        // Mengurangi countTimer
        countTimer -= Time.unscaledDeltaTime;
    }

    // Dipanggil saat fireball bersentuhan dengan objek lain
    private void OnCollisionEnter2D(Collision2D Collision)
    {
        // Jika fireball bersentuhan dengan objek yang bukan pemain, hancurkan fireball
        if (Collision.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }

    // Coroutine untuk menunggu sebelum collider diaktifkan
    public IEnumerator nothit()
    {
        // Menunggu selama 0.1 detik
        yield return new WaitForSecondsRealtime(0.1f);

        // Mengaktifkan collider setelah 0.1 detik
        circleCollider.enabled = true;
    }
}
