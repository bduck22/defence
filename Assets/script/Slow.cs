using System.Collections;
using UnityEngine;

public class Slow : MonoBehaviour
{
    public int SpeedValue;
    float OriginSpeed;
    void Start()
    {
        GetComponent<Enemy>().DownedSpeed += SpeedValue;
    }
    IEnumerator speedback()
    {
        yield return new WaitForSeconds(5);
        GetComponent<Enemy>().DownedSpeed -= SpeedValue;
        Destroy(this);
    }
}
