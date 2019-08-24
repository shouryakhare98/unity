using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
	private int count;
    private bool onGround;
    private bool keepJumping;
	public Text countText;
    public Text winText;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
		count = 0;
        onGround = true;
        keepJumping = false;
        SetCountText();
		winText.text = "";
    }

    private void Update()
    {

        if (Input.GetKeyDown("space"))
        {
            keepJumping = !keepJumping;
        }

        if (keepJumping && onGround)
        {
            Vector3 jump = new Vector3(0.0f, 100.0f, 0.0f);
            rb.AddForce(jump);
        }
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Pick Up"))
		{
			other.gameObject.SetActive(false);
			count += 1;
			SetCountText();
		}
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            onGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            onGround = false;
        }
    }

    void SetCountText()
	{
		countText.text = "Count: " + count.ToString();
		if (count >= 12)
		{
			winText.text = "You Win!";
		}
	}
}
