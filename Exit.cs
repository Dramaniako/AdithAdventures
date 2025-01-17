using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    // Fungsi untuk keluar dari permainan
    public void exitGame(){
        // Menghentikan aplikasi, keluar dari permainan
        Application.Quit();
    }
}
