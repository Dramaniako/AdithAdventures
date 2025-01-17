using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    // Referensi ke objek dalam game yang dikategorikan sebagai finish (tidak digunakan di dalam kode ini, tetapi dapat digunakan untuk pengembangan lebih lanjut)
    public GameObject finish;

    // Dipanggil saat terjadi tabrakan dengan collider lain
    private void OnCollisionEnter2D(Collision2D Collision) {
        // Mengecek apakah objek yang bersentuhan memiliki tag "Player"
        if (Collision.gameObject.tag == "Player")
        {
            // Menambahkan 1 poin ke variabel point dalam script GameManager
            GameManager.point += 1;

            // Menghancurkan objek coin ini setelah diambil oleh player
            Destroy(gameObject);
        }
    }
}
