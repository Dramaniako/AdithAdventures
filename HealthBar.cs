using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Mengambil gambar health bar
    public Image healthBarImage;

    // Warna untuk status kesehatan (sehat, sedang, rendah)
    public Color healthyColor = Color.green;
    public Color mediumColor = Color.yellow;
    public Color lowColor = Color.red;

    // Durasi transisi warna health bar
    public float transitionDuration = 1f;

    // Ambang batas untuk status kesehatan
    public float mediumHealthThreshold = 50f;
    public float lowHealthThreshold = 20f;

    // Variabel untuk menyimpan kesehatan maksimal dan kesehatan saat ini
    public float maxHealth, currentHealth;

    // Variabel untuk menyimpan warna target untuk transisi
    private Color targetColor;

    // Timer untuk transisi warna
    private float transitionTimer;

    // Dipanggil saat objek pertama kali diinisialisasi
    void Start()
    {
        // Menentukan warna target awal sebagai sehat
        targetColor = healthyColor;

        // Mengatur warna health bar sesuai warna target
        healthBarImage.color = healthyColor;

        // Mengatur ukuran health bar sesuai dengan kesehatan maksimal
        RectTransform startSize = healthBarImage.GetComponent<RectTransform>();
        startSize.sizeDelta = new Vector2(maxHealth, startSize.sizeDelta.y);
    }

    // Dipanggil setiap frame
    void Update()
    {
        // Jika health bar image ada dan timer transisi belum selesai
        if (healthBarImage != null && transitionTimer < transitionDuration)
        {
            // Mengubah warna health bar dengan transisi secara perlahan
            healthBarImage.color = Color.Lerp(healthBarImage.color, targetColor, transitionTimer / transitionDuration);
            transitionTimer += Time.deltaTime; // Menambah timer
        }        
    }

    // Fungsi untuk mengatur kesehatan dan mengubah ukuran serta warna health bar
    public void SetHealth(float health)
    {
        // Membatasi nilai kesehatan antara 0 dan nilai maksimal
        currentHealth = Mathf.Clamp(health, 0, maxHealth);

        // Mengubah persentase pengisian health bar sesuai dengan kesehatan saat ini
        healthBarImage.fillAmount = currentHealth / maxHealth;

        // Memperbarui warna health bar
        UpdateColor();

        // Mengatur ulang timer transisi warna
        transitionTimer = 0f;

        // Mengubah ukuran health bar untuk menyesuaikan perubahan kesehatan
        RectTransform rectTransform = healthBarImage.GetComponent<RectTransform>();
        rectTransform.pivot = new Vector2(1f, 0f); // Menempatkan pivot di sisi kanan
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x - 50f, rectTransform.sizeDelta.y); // Mengubah ukuran health bar
    }

    // Fungsi untuk memperbarui warna health bar berdasarkan tingkat kesehatan
    private void UpdateColor()
    {
        // Menentukan warna target berdasarkan level kesehatan
        if (currentHealth <= maxHealth * lowHealthThreshold)
        {
            targetColor = lowColor; // Warna merah jika kesehatan sangat rendah
        }
        else if (currentHealth <= maxHealth * mediumHealthThreshold)
        {
            targetColor = mediumColor; // Warna kuning jika kesehatan sedang
        }
        else
        {
            targetColor = healthyColor; // Warna hijau jika kesehatan baik
        }
    }
}
