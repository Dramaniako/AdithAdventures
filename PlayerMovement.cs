using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameManager gameManager; // Referensi ke script GameManager
    public HealthBar healthBar; // Referensi ke script HealthBar
    public bool isDead = false; // Menyimpan status apakah pemain mati
    public Animator animator; // Referensi ke Animator untuk animasi karakter

    public new SpriteRenderer renderer; // Referensi ke SpriteRenderer untuk menggambar karakter
    public Rigidbody2D rb; // Referensi ke Rigidbody2D untuk fisika karakter
    public float jumpHeigth = 5f; // Tinggi lompatan karakter
    public bool isGround = true; // Menyimpan status apakah karakter berada di tanah

    private float movement; // Menyimpan input gerakan horizontal
    public float moveSpeed = 5f; // Kecepatan gerakan horizontal karakter
    public static bool facingRight; // Menyimpan status apakah karakter menghadap kanan
    public float knock; // Besar gaya dorongan saat terkena musuh

    public BoxCollider2D boxCollider; // Referensi ke BoxCollider2D karakter

    public GameObject fireball; // Referensi ke prefab fireball yang akan ditembakkan

    // Update dipanggil setiap frame
    void Update(){
        // Mendapatkan input horizontal dari pengguna
        movement = Input.GetAxis("Horizontal");

        // Mengatur arah karakter berdasarkan input
        if(movement < 0f && facingRight == true){
            transform.eulerAngles = new Vector3(0f,-180f,0f); // Membalik karakter
            facingRight = false;
        }
        else if(movement > 0f && facingRight == false){
            facingRight = true;
            transform.eulerAngles = new Vector3(0f,0f,0f); // Membalik karakter kembali ke posisi awal
        }

        // Mengatur aksi lompat saat tombol Space ditekan
        if (Input.GetKey(KeyCode.Space) && isGround){
            Jump(jumpHeigth); // Panggil fungsi lompat
            isGround = false; // Set status karakter tidak di tanah
            animator.SetBool("jump", true); // Mulai animasi lompat
        }

        // Mengatur animasi berlari saat bergerak
        if (Mathf.Abs(movement) > .1f){
            animator.SetFloat("run", 1f);
        }
        else if (movement < .1f){
            animator.SetFloat("run", 0f);
        }

        // Menembakkan fireball saat tombol Fire1 ditekan
        if (Input.GetButtonDown("Fire1") && Time.timeScale != 0)
        {
            Instantiate(fireball, transform.position, transform.rotation); // Membuat instansiasi fireball
        }
    }

    // FixedUpdate dipanggil pada interval tetap, cocok untuk pergerakan berbasis fisika
    private void FixedUpdate() {
        transform.position += new Vector3(movement, 0f, 0f) * Time.fixedDeltaTime * moveSpeed; // Memindahkan karakter berdasarkan input horizontal
    }

    // Fungsi untuk membuat karakter melompat
    void Jump(float x){
        rb.AddForce(new Vector2(0f,x), ForceMode2D.Impulse); // Menambahkan gaya untuk lompat
    }

    // Fungsi untuk menangani tabrakan dengan objek lain
    private void OnCollisionEnter2D(Collision2D Collision){
        if (Collision.gameObject.tag == "Ground"){
            isGround = true; // Set status karakter berada di tanah
            animator.SetBool("jump", false); // Berhenti animasi lompat
        }
        else if (Collision.gameObject.tag == "Enemy")
        {
            StartCoroutine(Enemyhit()); // Panggil coroutine saat terkena musuh
        }
        else if (Collision.gameObject.tag == "DeathBarrier")
        {
            isDead = true; // Set status karakter mati
        }
        else if (Collision.gameObject.tag == "Boost")
        {
            Jump(jumpHeigth*1.5f); // Dapatkan boost lompatan
        }
    }

    // Coroutine yang dijalankan saat karakter terkena musuh
    public IEnumerator Enemyhit(){
        healthBar.SetHealth(healthBar.currentHealth - 50f); // Mengurangi kesehatan saat terkena musuh
        if (healthBar.currentHealth <= 0f)
        {
            isDead = true; // Set status karakter mati jika kesehatan mencapai 0
            gameManager.GameOver(); // Panggil fungsi GameOver dari GameManager
        }
        rb.AddForce(new Vector2(0f , knock), ForceMode2D.Impulse); // Menambahkan gaya dorongan untuk membuat karakter mundur

        boxCollider = GetComponent<BoxCollider2D>(); // Mendapatkan komponen collider
        boxCollider.enabled = false; // Nonaktifkan collider untuk mencegah tabrakan sementara
        
        renderer.enabled = false; // Menyembunyikan renderer untuk efek hit

        yield return new WaitForSecondsRealtime(0.1f); // Tunggu sejenak

        renderer.enabled = true; // Tampilkan kembali renderer
        yield return new WaitForSecondsRealtime(0.2f); // Tunggu sejenak sebelum mengaktifkan collider

        boxCollider.enabled = true; // Aktifkan kembali collider
    }
}
