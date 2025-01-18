using System.Collections;
using UnityEngine;

public class Tower : MonoBehaviour
{
    Queue FindMob = new Queue();
    [SerializeField] private int AttackSpeed;
    [SerializeField] private int Damage;
    void Start()
    {

    }

    void Update()
    {

    }
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(AttackSpeed);
        if (FindMob.Count > 0)
        {

            GameObject target = (GameObject)FindMob.ToArray()[0];
            target.GetComponent<Enemy>().Hp -= Damage;
        }
        if (FindMob.Count > 0) yield return StartCoroutine(Attack());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (FindMob.Count == 0)
        {
            StartCoroutine(Attack());
        }
        FindMob.Enqueue(collision.gameObject);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        FindMob.Dequeue();
    }

}
