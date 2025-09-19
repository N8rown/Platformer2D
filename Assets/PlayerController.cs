using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Assertions;

public class PlayerController : PhysicsObject
{

    AudioSource jumpsound;

    [Header("Movement")]
    public float speed;
    public float jumpHeight;
    bool onMovingPlatform = false;
    float xAdjustment = 0f;

    [Header("Lives & UI")]
    public int lives = 3;
    [SerializeField] private GameObject gameOverPanel; 
    [SerializeField] private Button defaultSelectedButton; 
    public Text livesText;
    public Text pointsText;

    [Header("Progress")]
    int rounds_completed = 0;
    int points = 0;

    [Header("Spawn")]
    public Vector3 starting_position;


    void Start()
    {
        jumpsound = GetComponent<AudioSource>();
        jumpsound.playOnAwake = false;

        starting_position = new Vector3(-7, 21, 0);
        transform.position = starting_position;

        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        UpdateLivesUI();
        UpdatePointsUI();

        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetAxis("Horizontal") > 0)
            desiredx = speed;
        else if (Input.GetAxis("Horizontal") < 0)
            desiredx = -1 * speed;
        else
            desiredx = 0;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = jumpHeight;
            jumpsound.Play();
        }

        if (onMovingPlatform)
            desiredx += 1f * xAdjustment;
    }


    public override void CollideWith(Collider2D other)
    {
        if (other.CompareTag("Respawn"))
        {
            transform.position = starting_position;
            ChangeLives(-1);
        }

        if (other.CompareTag("Points"))
        {
            points += 1;
            UpdatePointsUI();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Victory"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            rounds_completed += 1;
            if (rounds_completed == 3)
            {
                Debug.Log("You Win!");
            }
        }
    }

    public override void CollideWithVertical(Collider2D other)
    {
        if (other.CompareTag("moving"))
        {
            xAdjustment = other.GetComponent<MovingPlatform>().desiredx;
            onMovingPlatform = true;
        }
        else
        {
            onMovingPlatform = false;
            xAdjustment = 0f;
        }
    }

   
    private void ChangeLives(int delta)
    {
    
        if (IsGameOver()) return;

        lives += delta;
        if (lives < 0) lives = 0;

        UpdateLivesUI();

        if (lives == 0)
        {
            TriggerGameOver();
        }
    }

    private bool IsGameOver() => gameOverPanel != null && gameOverPanel.activeSelf;

    private void TriggerGameOver()
    {
        this.enabled = false;

        Time.timeScale = 0f;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);

            if (defaultSelectedButton != null && EventSystem.current != null)
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(defaultSelectedButton.gameObject);
            }
        }

        Debug.Log("Game Over");
    }

    private void UpdateLivesUI()
    {
        if (livesText != null)
            livesText.text = lives.ToString();
    }

    private void UpdatePointsUI()
    {
        if (pointsText != null)
            pointsText.text = points.ToString();
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;

        rounds_completed = 0;
        points = 0;

        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;

        rounds_completed = 0;
        points = 0;

        SceneManager.LoadScene("StartMenu"); 
    }
}

