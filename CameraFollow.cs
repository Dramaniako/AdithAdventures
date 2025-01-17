using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Referensi ke objek dalam game yang akan diikuti oleh kamera, dikategorikan sebagai player
    public GameObject player;

    // Dipanggil setiap frame untuk memperbarui posisi kamera
    void Update()
    {
        // Mengatur posisi kamera agar selalu mengikuti posisi player
        // Posisi X dan Y kamera disamakan dengan posisi X dan Y player
        // Posisi Z kamera diatur lebih rendah 10 unit dari posisi Z player, sehingga kamera tetap berada di belakang
        transform.position = new Vector3(
            player.transform.position.x, 
            player.transform.position.y, 
            player.transform.position.z - 10
        );
    }
}
