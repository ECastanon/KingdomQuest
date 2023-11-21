using UnityEngine;

public class DayAndNightCycle : MonoBehaviour
{
  Vector3 rot = Vector3.zero;
  float degreespersec = 6;

    // Update is called once per frame
    void Update()
    {
        rot.x = degreespersec * Time.deltaTime;
        transform.Rotate(rot, Space.World);
    }
}
