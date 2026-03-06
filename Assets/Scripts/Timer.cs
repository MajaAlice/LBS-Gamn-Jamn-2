using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float seconds;
    public int minutes;
    public bool isCounting = true;
    public TMP_Text TimerText;
    void Start()
    {
        
    }

    void Update()
    {
        if (isCounting)
        {
            seconds += Time.deltaTime;
            if (seconds >= 60)
            {
                seconds -= 60;
                minutes++;  
            }
        }
        TimerText.text = minutes.ToString() + "m:" + seconds.ToString() + "s";
    }
}
