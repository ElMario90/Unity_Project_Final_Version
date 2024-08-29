using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Animation animationComponent;
    public Rigidbody rb;
    public AudioSource hitSound; 
    public TextMeshProUGUI healthText;

    public float jumpForce = 5f;
    public float runSpeed = 5f;
    public float walkSpeed = 2f;
    public int maxHealth = 5;

    private int currentHealth;
    private float currentSpeed;
    private bool isGrounded;



    /* Key Collecting */

    public GameObject[] keyPrefabs; 
    public TextMeshProUGUI keyCountText; 
    public GameObject door;
    public GameObject PopUiGameObject;
    private int totalKeys = 3;
    private int collectedKeys = 0;
    private GameObject currentKey; 
    public AudioSource audioSource;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
        rb.freezeRotation = true;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        for (int i = 0; i < keyPrefabs.Length; i++)
        {
            keyPrefabs[i].SetActive(i == 0); 
        }

        UpdateKeyCountText();
    }

    private void UpdateKeyCountText()
    {
        keyCountText.text = collectedKeys + "/" + totalKeys;
    }

    private void Update()
    {
        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");

        if (Mathf.Abs(vertical) > 0.5f)
            currentSpeed = runSpeed;
        else if (Mathf.Abs(vertical) > 0.1f)
            currentSpeed = walkSpeed;

        if (Mathf.Abs(vertical) > 0.1f)
        {
            var movement = transform.forward * vertical * currentSpeed * Time.deltaTime;
            rb.MovePosition(rb.position + movement);

            animationComponent.Play(currentSpeed == runSpeed ? "Run_F" : "Walk_F");
        }
        else
        {
            animationComponent.Play("Pose_Idle");
        }

        if (Mathf.Abs(horizontal) > 0.1f)
        {
            float rotationSpeed = 100f; 
            transform.Rotate(Vector3.up, horizontal * rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
       
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Obstacle"))
        {
            TakeDamage();
        }

        if (collider.gameObject.CompareTag("Died"))
        {
            SceneManager.LoadScene("GameLoss");
        }


        if (collider.CompareTag("Key"))
        {
            currentKey = collider.gameObject;
            CollectKey();
        }

        if (collider.CompareTag("Door"))
        {
            if (collectedKeys == totalKeys)
            {
                SceneManager.LoadScene("Level2"); 
            }
            else
            {
                PopUiGameObject.SetActive(true);
                Debug.Log("You need to collect all keys!");
            }
        }

        if (collider.CompareTag("DoorLevelComplete"))
        {
            SceneManager.LoadScene(collectedKeys == totalKeys ? "GameWin" : "GameLoss");
        }


        
    }
    private void CollectKey()
    {
        if (currentKey != null)
        {
            Destroy(currentKey); 
            collectedKeys++;
            UpdateKeyCountText();
            PlayKeyCollectSound();

            if (collectedKeys < totalKeys)
            {
                keyPrefabs[collectedKeys].SetActive(true);
            }
        }
    }

    private void PlayKeyCollectSound()
    {
        if (audioSource != null)
        {
            audioSource.Play(); 
        }
    }
    private void TakeDamage()
    {
        if (hitSound != null)
        {
            hitSound.Play();
        }
        currentHealth--;

        UpdateHealthUI();
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene("GameLoss");
        }
    }

    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = currentHealth + "/" + maxHealth;
        }
    }
}
