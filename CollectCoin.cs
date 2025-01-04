using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    public GameObject finish; //mengambil objek dalam game yang kemudian dikategorikan sebagai finish
    private void OnCollisionEnter2D(Collision2D Collision) {
        if (Collision.gameObject.tag == "Player") //mengecek apakah objek bersentuhan dengan objek yang diberi klasifikasi Player 
        {
            GameManager.point += 1; //variabel point pada script gamemanager ditambah 1
            Destroy(gameObject); //objek dihancurkan
        }
        
        
    }

}
