using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameManager gameManager;
    public HealthBar healthBar;
    public bool isDead = false;
    public Animator animator;

    public new SpriteRenderer renderer;

    public Rigidbody2D rb;
    public float jumpHeigth = 5f;
    public bool isGround = true;

    private float movement;
    public float moveSpeed = 5f;
    public static bool facingRight;
    public float knock;

    public BoxCollider2D boxCollider;

    public GameObject fireball;

    // Update is called once per frame
    void Update(){
        movement = Input.GetAxis("Horizontal");

        if(movement < 0f && facingRight == true){
            transform.eulerAngles = new Vector3(0f,-180f,0f);
            facingRight = false;
            // renderer.flipX = true;
        }
        else if(movement > 0f && facingRight == false){
            facingRight = true;
            transform.eulerAngles = new Vector3(0f,0f,0f);
            // renderer.flipX = false;
        }

        if (Input.GetKey(KeyCode.Space) && isGround){
            Jump();
            isGround = false;
            animator.SetBool("jump", true);
        }

        if (Mathf.Abs(movement) > .1f){
            animator.SetFloat("run", 1f);
        }
        else if (movement < .1f){
            animator.SetFloat("run", 0f);
        }

        if (Input.GetButtonDown("Fire1") && Time.timeScale != 0)
        {
            Instantiate(fireball, transform.position, transform.rotation);
        }

        
    }

    private void FixedUpdate() {
        transform.position += new Vector3(movement, 0f, 0f) * Time.fixedDeltaTime * moveSpeed;
    }

    void Jump(){
        rb.AddForce(new Vector2(0f,jumpHeigth), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D Collision){
        if (Collision.gameObject.tag == "Ground"){
            isGround = true;
            animator.SetBool("jump", false);
            
        }
        if (Collision.gameObject.tag == "Enemy")
        {
            StartCoroutine(Enemyhit());
            
        }
        if (Collision.gameObject.tag == "DeathBarrier")
        {
            isDead = true;
        }
    }


    public IEnumerator Enemyhit(){
        healthBar.SetHealth(healthBar.currentHealth - 50f);
        if (healthBar.currentHealth <= 0f)
         {
            isDead = true;
            gameManager.GameOver();
        }
        rb.AddForce(new Vector2(0f , knock), ForceMode2D.Impulse);

        boxCollider = GetComponent<BoxCollider2D>();
        
        boxCollider.enabled = false;
        
        renderer.enabled = false;

        yield return new WaitForSecondsRealtime(0.1f);

        renderer.enabled = true;

        yield return new WaitForSecondsRealtime(0.2f);

        boxCollider.enabled = true;

    }
    
    
}