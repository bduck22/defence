using System.Collections;
using UnityEngine;

public class Tower : MonoBehaviour
{
    Queue FindMob = new Queue();
    public int AttackSpeed;
    public int Damage;
    public int Type;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject WideBullet;
    bool CorouTrigger;
    void Start()
    {
        CorouTrigger = true;
    }

    void Update()
    {
    }
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(AttackSpeed);
        if (FindMob.Count > 0)
        {
            switch (Type)
            {
                case 1:StartCoroutine(nomalattack()); break;
                case 2: break;
                case 3: break;
                case 4:break;
            }

        }
        if (FindMob.Count > 0)
        {
            StartCoroutine(Attack());
        }
        else
        {
            CorouTrigger = true;
        }
    }
    IEnumerator nomalattack()
    {
        GameObject target = (GameObject)FindMob.ToArray()[0];
        GameObject Bullet = Instantiate(bullet, transform.position, transform.rotation);
        Bullet.GetComponent<bullet_move>().target = target;
        yield return new WaitForSeconds(0.15f);
        if (target) target.GetComponent<Enemy>().Hp -= Damage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (FindMob.Count == 0)
        {
            if (CorouTrigger)
            {
                CorouTrigger = false;
                StartCoroutine(Attack());
            }
        }
        FindMob.Enqueue(collision.gameObject);
    }

    public void hoverobject()
    {
        GameManager.instance.HoverObject = gameObject;
    }
    public void exitobject()
    {
        GameManager.instance.HoverObject = null;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        FindMob.Dequeue();
    }
}
