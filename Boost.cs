using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    // Referensi ke komponen BoxCollider2D pada game object ini
    public BoxCollider2D boxCollider;

    // Method ini dipanggil saat terjadi tabrakan dengan collider lain
    private void OnCollisionEnter2D(Collision2D other) {
        // Periksa apakah objek yang bertabrakan memiliki tag "Player"
        if (other.gameObject.tag == "Player")
        {
            // Memulai coroutine Boosted untuk menonaktifkan dan mengaktifkan kembali collider
            StartCoroutine(Boosted());
        }
    }

    // Coroutine untuk menangani efek boost
    public IEnumerator Boosted(){
        // Mendapatkan komponen BoxCollider2D pada game object ini
        boxCollider = GetComponent<BoxCollider2D>();

        // Menonaktifkan BoxCollider2D untuk mencegah interaksi lebih lanjut
        boxCollider.enabled = false;

        // Menunggu selama 1 detik secara realtime
        yield return new WaitForSecondsRealtime(1f);

        // Mengaktifkan kembali BoxCollider2D
        boxCollider.enabled = true;
    }
}
