using Unity.VisualScripting;
using UnityEngine;

public class SlowBullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>())
        {
            collision.AddComponent<Slow>().SpeedValue=transform.parent.GetComponent<Tower>().SlowValue;
        }
    }
}
