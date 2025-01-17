using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishGoal : MonoBehaviour
{
    // Menyimpan jumlah goal yang harus dicapai
    public int goals;

    // Referensi ke Text untuk menampilkan pesan saat goal tercapai
    public Text GoalsAchieved;

    // Menyimpan status apakah goal sudah tercapai
    public bool trigerred = false;

    // Mengambil komponen CircleCollider2D dan SpriteRenderer dari objek ini
    public new CircleCollider2D collider;
    public SpriteRenderer sprite;

    // Dipanggil saat permainan dimulai
    private void Start() 
    {
        // Menonaktifkan collider, sprite, dan tampilan teks goal pada awal permainan
        collider.enabled = false;
        sprite.enabled = false;
        GoalsAchieved.enabled = false;
    }

    // Dipanggil setiap frame untuk memeriksa apakah goal sudah tercapai
    private void Update() 
    {
        // Mengecek apakah jumlah point yang dikumpulkan sudah mencapai jumlah goal yang ditentukan
        if (GameManager.point == goals) 
        {
            // Mengaktifkan collider dan sprite, dan memulai proses pengecekan
            collider.enabled = true;
            sprite.enabled = true;
            StartCoroutine(check());
        }
    }

    // Dipanggil saat objek bersentuhan dengan objek lain
    private void OnCollisionEnter2D(Collision2D Collision) 
    {
        // Mengecek apakah objek yang bersentuhan memiliki tag "Player"
        if (Collision.gameObject.tag == "Player") 
        {
            // Menonaktifkan scene Level1 dan memuat scene FinishMenu
            SceneManager.UnloadSceneAsync("Level1");
            SceneManager.LoadScene("FinishMenu", LoadSceneMode.Single);
        }
    }

    // Coroutine untuk menampilkan pesan setelah goal tercapai
    public IEnumerator check()
    {
        // Mengecek jika pesan belum ditampilkan
        if (trigerred == false)
        {
            // Menampilkan pesan bahwa semua coin telah didapatkan
            GoalsAchieved.enabled = true;
            GoalsAchieved.text = "All Coin Obtained!\n\nGolden Coin Appeared!";
            
            // Menunggu selama 3 detik sebelum menyembunyikan pesan
            yield return new WaitForSecondsRealtime(3f);
            GoalsAchieved.enabled = false;

            // Menandakan bahwa pesan sudah ditampilkan
            trigerred = true;
        }
    }
}
