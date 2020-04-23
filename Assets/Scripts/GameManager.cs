using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private float amountOfLifetoLoose = 0.001f;

    public float amountOfLifetoGain = 0.1f;
    public RectTransform lifebar;
    public Transform playerPosition;
    public Transform level1SpawnPoint;
    public Transform level2SpawnPoint;
    public Transform level3SpawnPoint;
    public Transform level4SpawnPoint;
    public Transform level5SpawnPoint;
    public Animator transitionAnim;
    public bool getThePill;

    private float timer = 0f;
    private readonly float timerDelay = 0.5f;
    private bool isDeath;
    private Vector3 lastRespawn;
    private int lastLevel = 1;

    public bool hasRespawn;
    void Start()
    {
        lastRespawn = level1SpawnPoint.position;
        SetCameraPosition(0.05f);
    }

    void Update()
    {
        hasRespawn = playerPosition.position == lastRespawn;
        if(Time.time > timer)
        {
            timer = Time.time + timerDelay;
        }
        if(isDeath)
        {
            YouDieScene();
        }
        LooseLife();
        InGameControls();
    }

    public void LooseLife()
    {
        float lifeToloose = (lifebar.localScale.x - amountOfLifetoLoose / 3f);
        isDeath = lifeToloose <= 0;
        lifebar.localScale = new Vector2(lifeToloose, lifebar.localScale.y);
    }

    public void GainLife()
    {
        lifebar.localScale = new Vector2((lifebar.localScale.x + amountOfLifetoGain), lifebar.localScale.y);
    }

    public void GetThePill()
    {
        lastRespawn = playerPosition.position;
        getThePill = true;
    }

    public void ResetScene()
    {
        transitionAnim.SetTrigger("makeTransition");
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void YouDieScene()
    {
        SceneManager.LoadScene("YouDieMenu");
    }

    public void RestartLevel()
    {
        hasRespawn = true;
        transitionAnim.SetTrigger("makeTransition");
        playerPosition.position = lastRespawn;
    }

    public void NextScene()
    {
        string scene = "level" + ++lastLevel;
        switch (scene)
        {
            case "level2":
                ContinueLevel(16.81f, level2SpawnPoint.position);
                break;
            case "level3":
                ContinueLevel(32.79f, level3SpawnPoint.position);
                break;
            case "level4":
                ContinueLevel(50.40f, level4SpawnPoint.position);
                break;
            case "level5":
                ContinueLevel(69.64f, level5SpawnPoint.position);
                break;
            case "level6":
                SceneManager.LoadScene("EndGame");
                break;
            default:
                break;
        }
    }

    private void ContinueLevel(float cameraPosition, Vector3 spawnPoint)
    {
        lastRespawn = spawnPoint;
        SetCameraPosition(cameraPosition);
        playerPosition.position = spawnPoint;
        getThePill = false;
    }

    private void SetCameraPosition(float cameraPosition)
    {
        Transform camPosition = Camera.main.transform;
        Camera.main.transform.localPosition = new Vector3(cameraPosition, camPosition.position.y, camPosition.position.z);
    }

    private void InGameControls()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            ResetScene();
        }
    }

}
