using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{
    public GameObject PauseScreen;
    public float targetTime;
    public TextMeshProUGUI TimeText;
    public AudioSource beepSound; 

    private bool isBeeping = false; 
    private float beepInterval = 1.0f; 
    private float nextBeepTime; 
    private int beepCount = 0;

    private void Start()
    {
        Time.timeScale = 1.0f;
    }

    private void Update()
    {
        if (!PauseScreen.activeInHierarchy)
        {
            targetTime -= Time.deltaTime;
        }

        var timer = ToHourseMinutesSeconds(targetTime);

        if (targetTime <= 0)
        {
            targetTime = 0;
            ShowObjFail();
        }

        TimeText.text = timer;

        if (targetTime <= 10 && !isBeeping)
        {
            isBeeping = true; 
            nextBeepTime = Time.time + beepInterval; 
        }

        if (isBeeping && beepCount < 10 && Time.time >= nextBeepTime)
        {
            PlayBeep();
            nextBeepTime = Time.time + beepInterval; 
            beepCount++;
        }
    }

    private void PlayBeep()
    {
        if (beepSound != null)
        {
            beepSound.Play(); 
        }
    }

    public void ShowObjFail()
    {
        SceneManager.LoadScene("GameLoss");
        Time.timeScale = 0.0f;
    }

    private string ToHourseMinutesSeconds(float seconds)
    {
        var s = seconds % 60;
        var ms = s * 1000;

        ms = ms % 100;
        var m = (int)seconds / 60;
        var h = m / 60;

        return ToDualDigit(m) + ":" + ToDualDigit((int)s);
    }

    private string ToMinutesSeconds(float seconds)
    {
        var s = seconds % 60;
        var ms = s * 1000;

        ms = ms % 100;
        var m = (int)seconds / 60;

        if (!ToDualDigit((int)s).Equals("00"))
            return ToDualDigit(m) + "Min, " + ToDualDigit((int)s) + "Sec";
        return ToDualDigit(m) + "Minutes";
    }

    private string ToDualDigit(int value)
    {
        if (value < 10)
        {
            return "0" + value;
        }
        return "" + value;
    }
}
