using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Referensi ke objek dan elemen UI dalam game
    public GameObject startButton;       // Tombol start
    public PlayerMovement player;        // Pemain
    public Text gameOverCountdown;       // Teks untuk countdown Game Over
    public GameObject finishGoal;        // Tujuan finish game
    public Text Point;                   // Teks untuk menampilkan poin
    public Image healthBar;              // Gambar health bar
    public FinishGoal finish;            // Objek FinishGoal

    // Variabel untuk menyimpan poin
    public static int point = 0;

    // Timer untuk countdown saat game over
    public float countTimer = 3;

    // Dipanggil saat game dimulai
    void Start()
    {
        // Menonaktifkan game over countdown pada awal permainan
        gameOverCountdown.gameObject.SetActive(false);
        Time.timeScale = 1; // Mengatur waktu berjalan normal
        healthBar.gameObject.SetActive(false); // Menonaktifkan health bar
        Destroy(finishGoal); // Menghancurkan objek finish goal
    }

    // Dipanggil setiap frame
    private void Update()
    {
        // Jika pemain mati
        if( player.isDead )
        {
            // Menampilkan countdown Game Over
            gameOverCountdown.gameObject.SetActive(true);
            countTimer -= Time.unscaledDeltaTime; // Mengurangi timer
            Stop(); // Menghentikan waktu
        }

        // Menampilkan countdown Game Over
        gameOverCountdown.text = "Restarting " + (countTimer).ToString("0");

        // Jika countdown selesai, restart game
        if(countTimer < 0)
        {
            RestartGame();
        }

        // Menampilkan health bar jika waktu masih berjalan
        if (Time.timeScale != 0)
        {
            healthBar.gameObject.SetActive(true);
        }

        // Menampilkan jumlah poin yang telah dicapai
        Point.text = "Point : " + point + " / " + finish.goals;
    }

    // Fungsi untuk memulai game
    public void StartGame()
    {
        startButton.SetActive(false); // Menonaktifkan tombol start
        SceneManager.UnloadSceneAsync("StartMenu"); // Menghancurkan scene StartMenu
        SceneManager.LoadScene("Level1", LoadSceneMode.Single); // Memuat scene Level1
    }

    // Fungsi untuk menghentikan permainan
    public void GameOver()
    {
        Time.timeScale = 0; // Menghentikan waktu
    }

    // Fungsi untuk me-restart game
    public void RestartGame()
    {
        SceneManager.UnloadSceneAsync("Level1"); // Menghancurkan scene Level1
        SceneManager.LoadScene("Level1", LoadSceneMode.Single); // Memuat ulang scene Level1
    }

    // Fungsi untuk kembali ke menu utama setelah selesai
    public void Restart()
    {
        SceneManager.UnloadSceneAsync("FinishMenu"); // Menghancurkan scene FinishMenu
        SceneManager.LoadScene("StartMenu", LoadSceneMode.Single); // Memuat scene StartMenu
    }

    // Fungsi untuk menghentikan waktu (menggunakan Time.timeScale)
    private void Stop()
    {
        Time.timeScale = 0f; // Menghentikan waktu
    }
}
