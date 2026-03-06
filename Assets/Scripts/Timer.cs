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
    // This needs comments -Lud
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
        float FloorTime = Mathf.Floor(seconds * 1000);
        TimerText.text = minutes.ToString() + ":" + (FloorTime/1000).ToString() + "s";
    }
}
