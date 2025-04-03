using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    private Rigidbody rb;
    private float movementX;
    private float movementY;

    private int count;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    public float jumpForce = 5f;
    private int jumpCount = 0;
    private int maxJumps = 2;
    private bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        setCountText();
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movmentValue)
    {
        Vector2 movmentVector = movmentValue.Get<Vector2>();

        movementX = movmentVector.x;
        movementY = movmentVector.y;
        

    }

    void setCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 6)
        {
            winTextObject.SetActive(true);
            Destroy(GameObject.FindWithTag("Enemy"));
        }
    }

    void FixedUpdate()
    {
        Vector3 movemnt = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movemnt * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count +  1;
            setCountText();
        }

         if (other.gameObject.CompareTag("Enemy"))
        {
           Destroy(gameObject);
           winTextObject.SetActive(true);
           winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";

        }

    }

    void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.CompareTag("Ground"))
    {
        isGrounded = true;
        jumpCount = 0;
    }
}


    void OnJump()
{
    if (jumpCount < maxJumps)
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        jumpCount++;
        isGrounded = false;
    }
}

    

}
