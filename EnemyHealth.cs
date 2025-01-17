using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    // Referensi ke Canvas untuk menampilkan UI kesehatan musuh
    public Canvas canvas;

    // Referensi ke objek musuh yang akan dipantau kesehatannya
    public GameObject enemy;

    // Referensi ke Image yang akan digunakan sebagai health bar
    public Image healthBarImage;

    // Warna health bar untuk status sehat, sedang, dan rendah
    public Color healthyColor = Color.green;
    public Color mediumColor = Color.yellow;
    public Color lowColor = Color.red;

    // Durasi transisi warna health bar
    public float transitionDuration = 1f;

    // Ambang batas kesehatan untuk transisi warna
    public float mediumHealthThreshold = 50f;
    public float lowHealthThreshold = 20f;

    // Kesehatan maksimum dan kesehatan saat ini
    public float maxHealth, currentHealth;

    // Warna target untuk transisi
    private Color targetColor;

    // Timer untuk durasi transisi warna
    private float transitionTimer;

    // Dipanggil saat objek pertama kali diinisialisasi
    void Start()
    {
        // Menetapkan warna target untuk health bar pada awalnya
        targetColor = healthyColor;
        healthBarImage.color = healthyColor;    

        // Menetapkan ukuran awal health bar berdasarkan maxHealth
        RectTransform startSize = healthBarImage.GetComponent<RectTransform>();
        startSize.sizeDelta = new Vector2(maxHealth, startSize.sizeDelta.y);
    }

    // Dipanggil setiap frame untuk memperbarui health bar
    void Update()
    {
        // Mengubah posisi health bar agar mengikuti posisi musuh di layar
        Vector2 enemyposition = Camera.main.WorldToViewportPoint(enemy.transform.position);
        Vector2 canvas = new Vector2(enemyposition.x + 3f, enemyposition.y);

        // Jika health bar ada dan transisi warna belum selesai, update warnanya
        if (healthBarImage != null && transitionTimer < transitionDuration)
        {
            healthBarImage.color = Color.Lerp(healthBarImage.color, targetColor, transitionTimer / transitionDuration);
            transitionTimer += Time.deltaTime;
        }        
    }

    // Fungsi untuk mengatur kesehatan musuh
    public void SetHealth(float health)
    {
        // Mengatur nilai kesehatan musuh dan memastikan berada dalam batas maxHealth
        currentHealth = Mathf.Clamp(health, 0, maxHealth);

        // Memperbarui jumlah kesehatan pada health bar
        healthBarImage.fillAmount = currentHealth / maxHealth;

        // Memperbarui warna health bar berdasarkan status kesehatan
        UpdateColor();

        // Mereset timer transisi warna
        transitionTimer = 0f;

        // Memperbarui ukuran health bar agar sesuai dengan nilai kesehatan
        RectTransform rectTransform = healthBarImage.GetComponent<RectTransform>();
        rectTransform.pivot = new Vector2(0f, 0f);
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x - 20f, rectTransform.sizeDelta.y);
    }

    // Fungsi untuk memperbarui warna health bar berdasarkan status kesehatan
    private void UpdateColor()
    {
        // Jika kesehatan musuh sangat rendah, ubah warna health bar menjadi merah
        if (currentHealth <= maxHealth * lowHealthThreshold)
        {
            targetColor = lowColor;
        }
        // Jika kesehatan musuh sedang, ubah warna health bar menjadi kuning
        else if (currentHealth <= maxHealth * mediumHealthThreshold)
        {
            targetColor = mediumColor;
        }
        // Jika kesehatan musuh masih penuh, ubah warna health bar menjadi hijau
        else
        {
            targetColor = healthyColor;
        }
    }
}
