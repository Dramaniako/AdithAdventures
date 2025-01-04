using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
   public GameObject player; //mengambil objek dalam game yang kemudian dikategorikan sebagai player

    void Update() //fungsi dipanggil tiap frame
    {
        //posisi objek dirubah menjadi posisi baru dengan vektor 3 pada x dan y yang sama dengan vektor 3 objek player serta vektor 3 pada z yang lebih rendah 10 dari vektor 3 objek player 
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z -10);
    }
}
