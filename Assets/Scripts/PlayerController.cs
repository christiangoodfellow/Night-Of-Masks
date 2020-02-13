using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float horizontalInput;
    public float verticalInput;
    public float speed = 10.0f;
    public float sneakSpeed = 10.0f;
    float timer;
    float timerMax = 3;
    float currentSpeed;
    bool sneak = false;
    bool canMove = true;
    public bool gameOver = false;
    bool paused = false;
    public GameObject gameOverPanel;
    public GameObject pauseMenu;
    public GameObject pauseButton;
    public BoxCollider boxCollider;
    public Transform spawnPoint;
    Animator animate;

    Rigidbody rb;
    Vector3 ySubtract;
    Vector3 normalHeight = new Vector3(1, 2, 1);
    Vector3 crouchHeight = new Vector3(1, 1, 1);
    RaycastHit hitRaycast;


    void Start()
    {
        animate = gameOverPanel.GetComponent<Animator>();
        currentSpeed = speed;
        rb = this.GetComponent<Rigidbody>();
        ySubtract = new Vector3(0, -0.4f, 0);

    }
    private void Awake()
    {
        Time.timeScale = 1f;
    }

    void FixedUpdate()
    {
        if (gameOver == true)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                animate.SetBool("GameOver", false);
                gameOverPanel.SetActive(false);
                currentSpeed = speed;
                currentSpeed = sneakSpeed;
                boxCollider.transform.position = new Vector3(boxCollider.transform.position.x, boxCollider.transform.position.y + 0.5f, boxCollider.transform.position.z);
                boxCollider.transform.localScale = normalHeight;
                transform.position = spawnPoint.position;
                gameOver = false;
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameOver();
        }

        ControlPlayer();
        if (pauseMenu.activeInHierarchy == false)
        {
            paused = false;
        }
    }

    void ControlPlayer()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (sneak == true)
            {
                sneak = false;
                currentSpeed = speed;
                currentSpeed = sneakSpeed;
                boxCollider.transform.position = new Vector3(boxCollider.transform.position.x, boxCollider.transform.position.y + 0.5f, boxCollider.transform.position.z);
                boxCollider.transform.localScale = normalHeight;
            }
            else
            {
                sneak = true;
                currentSpeed = sneakSpeed;
                boxCollider.transform.position = new Vector3(boxCollider.transform.position.x, boxCollider.transform.position.y - 0.5f, boxCollider.transform.position.z);
                boxCollider.transform.localScale = crouchHeight;

            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.activeInHierarchy == false)
            {
                pauseMenu.SetActive(true);
                paused = true;
                Time.timeScale = 0f;
                EventSystem es = EventSystem.current;
                es.SetSelectedGameObject(null);
                es.SetSelectedGameObject(pauseButton);
            }
            else
            {
                ButtonPress();
            }
        }

        Vector3 position = rb.transform.position;
        Vector3 movement = new Vector3(horizontalInput, 0.0f, verticalInput);

        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement.normalized), 0.2f);
        }
        if (Physics.Raycast(transform.position + ySubtract, transform.forward, out hitRaycast, 0.6f))
        {
            canMove = false;
        }
        else
        {
            canMove = true;
        }
        if (canMove)
        {
            position = position + movement * currentSpeed * Time.deltaTime;
            rb.transform.position = position;
        }



        //transform.Translate(movement * currentSpeed * Time.deltaTime, Space.World);
    }
    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        timer = timerMax;
        animate.SetBool("GameOver", true);
        gameOver = true;
        sneak = false;
    }
    public void ButtonPress()
    {
        pauseMenu.SetActive(false);
        paused = false;
        Time.timeScale = 1f;
    }

    public void GoMainMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}