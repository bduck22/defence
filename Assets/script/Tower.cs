using System.Collections;
using UnityEngine;

public class Tower : MonoBehaviour
{
    Queue FindMob = new Queue();
    [SerializeField] private int AttackSpeed;
    [SerializeField] private int Damage;
    [SerializeField] private GameObject bullet;
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
            GameObject Bullet = Instantiate(bullet, transform.position, transform.rotation);
            Bullet.GetComponent<bullet_move>().target = target;
            yield return new WaitForSeconds(0.3f);
            if(target)target.GetComponent<Enemy>().Hp -= Damage;
        }
        if (FindMob.Count > 0)
        {
            StartCoroutine(Attack());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (FindMob.Count == 0)
        {
            StartCoroutine(Attack());
        }
        FindMob.Enqueue(collision.gameObject);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        FindMob.Dequeue();
    }
}
