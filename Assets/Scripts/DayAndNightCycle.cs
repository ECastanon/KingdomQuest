using UnityEngine;

public class DayAndNightCycle : MonoBehaviour
{
    Light myLight;
    private GameObject rm;
    public int dayLength = 50; //Lower dayLength = faster days/nights
    void Start()
    {
        myLight = GetComponent<Light>();
        rm = GameObject.Find("ResourceManager");
    }

    void Update()
    {
        myLight.intensity = Mathf.PingPong(Time.time/dayLength, 1.3f);
    }
}
