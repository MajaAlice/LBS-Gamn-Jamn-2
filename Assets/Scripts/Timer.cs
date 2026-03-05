using UnityEngine;

public class Timer : MonoBehaviour
{
    public float seconds;
    public int minutes;
    public bool isCounting = true;
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
    }
}
