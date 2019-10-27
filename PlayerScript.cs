//using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D rb2d;
    private Animator anim;
    private bool isGrounded;
    public float speed;
    public Text score;
    public Text gameEnd;
    public Text livesCount;
    private int scoreValue = 0;
    private int lives = 3;
    private int totalScore;
    private bool facingRight = true;
    public int jumpPower;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        score.text = "Points: " + scoreValue.ToString();
        livesCount.text = "Lives: " + lives.ToString();
        gameEnd.text = "";
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        rb2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));

        if (scoreValue == 4)
        {
            rb2d.position = new Vector2(47.16f, 1f);
            scoreValue = 0;
            score.text = "Points: " + scoreValue.ToString();
            lives = 3;
            livesCount.text = "Lives: " + lives.ToString();
        }

        else if (totalScore == 8)
        {
            gameEnd.text = "You Win! Game created by Steven Ulloa!";
        }

        else if(lives == 0)
        {
            Destroy(this);
            Destroy(rb2d);
            gameEnd.text = "You Lose!";
        }

        else if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }

        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.W) && isGrounded == true)
        {
            Jump();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            totalScore = totalScore + 1;
            score.text = "Points: " + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }

        else if(collision.collider.tag == "Enemy")
        {
            lives -= 1;
            livesCount.text = "Lives: " + lives.ToString();
            Destroy(collision.collider.gameObject);
        }

    }

    private void QuickExit()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    private void Jump()
    {
        if (isGrounded = true)
        {
             rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }

        else if (isGrounded = false)
        {
            return;
        }
    }

    void Update()
    {

        HandleJump();

        if (rb2d.velocity.y != 0)
        {
            anim.SetInteger("State", 2);
            isGrounded = false;
        }

        else if (rb2d.velocity.x != 0 && rb2d.velocity.y == 0)
        {
            anim.SetInteger("State", 1);
            isGrounded = true;
        }

        else if (rb2d.velocity.x == 0 && rb2d.velocity.y == 0)
        {
            anim.SetInteger("State", 0);
            isGrounded = true;
        }

        QuickExit();
    }

}
