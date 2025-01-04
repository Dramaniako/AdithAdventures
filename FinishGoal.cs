using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishGoal : MonoBehaviour
{
    public new CircleCollider2D collider; //mengambil unsur CircleCollider2D dari objek dan mengkategorikannya sebagai collider
    public SpriteRenderer sprite; //mengambil unsur SpriteRenderer dari objek dan mengkategorikannya sebagai sprite
    private void Start() { //pada awal game dimuat fungsi ini dijalankan
        collider.enabled = false; //unsur collider dinonaktifkan
        sprite.enabled = false; //unsur sprite dinonaktifkan
    }
    private void Update() { //fungsi ini dipanggil tiap frame
        if (GameManager.point == 3) //mengecek apakah unsur point pada script GameManager sudah mencapai 3
        {
            collider.enabled = true; //unsur collider diaktifkan
            sprite.enabled = true; //unsur sprite diaktifkan
        }
    }
    private void OnCollisionEnter2D(Collision2D Collision) //fungsi dipanggil saat objek menyentuh objek lainnya
    {
        if (Collision.gameObject.tag == "Player") //mengecek apakah objek yang disentuh memiliki klasifikasi Player
        {
            SceneManager.UnloadSceneAsync("Level1"); //scene bernama Level1 dinonaktifkan
            SceneManager.LoadScene("FinishMenu", LoadSceneMode.Single); //scene bernama FinishMenu diaktifkan dengan mode single
        }
    }

}
