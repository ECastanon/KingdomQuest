using UnityEngine;

public class DayAndNightCycle : MonoBehaviour
{
    Light myLight;
    private GameObject rm;
    void Start()
    {
        myLight = GetComponent<Light>();
        rm = GameObject.Find("ResourceManager");
    }

    void Update()
    {
        myLight.intensity = Mathf.PingPong(Time.time/50, 1.3f);
        if(myLight.intensity <= 0){rm.GetComponent<ResourceManager>().StartDay();}
    }
}
