using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<Level> levels;

    private int startIndex = 0;

    private int currentIndex;

    public GameObject vfxLevelUp;

    public TextMeshProUGUI text;

    float startingTime = 3f;

    float currentTime = 0f;

    public bool startTimer = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        currentIndex = startIndex;

        currentTime = startingTime;

        text.SetText(currentTime.ToString("0"));

        levels[currentIndex].gameObject.SetActive(true);
    }

    private void Update()
    {
        if (startTimer)
        {
            currentTime -= 1 * Time.deltaTime;

            text.SetText(currentTime.ToString("0"));

            if (currentTime <= 0)
            {
                Pause();
                LevelUp();
            }
        }
    }

    public void Pause()
    {
        startTimer = false;
        currentTime = startingTime;
        text.SetText(currentTime.ToString());
        levels[GetCurrentIndex()].Stop();
    }

    public int GetCurrentIndex()
    {
        return currentIndex;
    }

    public void LevelUp()
    {
        currentIndex += 1;

        GameObject vfx = Instantiate(vfxLevelUp, transform.position, Quaternion.identity);
        Destroy(vfx, 2f);

        StartCoroutine(ChangeScene());
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(2);

        levels[currentIndex-1].gameObject.SetActive(false);

        if (currentIndex >= levels.Count)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            currentIndex = 0;
        }

        levels[currentIndex].gameObject.SetActive(true);
    }

    public void ReSetCurrentLevel()
    {
        //levels[currentIndex].AddComponents();
    }
}