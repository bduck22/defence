using System.Collections;
using UnityEngine;

public class Tower : MonoBehaviour
{
    Queue FindMob = new Queue();
    [SerializeField] private float AttackSpeed;
    [SerializeField] private int Damage;
    public int Type;
    [SerializeField] private int Scope;
    public int SlowValue;
    [SerializeField] private int MultiTargetCount;
    public int Level;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject WideBullet;
    [SerializeField] private GameObject SlowBullet;
    public int Price;
    bool CorouTrigger;
    void Start()
    {
        CorouTrigger = true;
        switch (Type)
        {
            case 1: Damage = 3; AttackSpeed = 2; Price = 20; break;
            case 2: Damage = 2; AttackSpeed = 2; Price = 25; Scope = 1; break;
            case 3: Damage = 3; AttackSpeed = 1; Price = 50; Scope = 3; SlowValue = 10; break;
            case 4: Damage = 2; AttackSpeed = 2; Price = 100; MultiTargetCount = 3; break;
        }
    }
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(AttackSpeed);
        if (FindMob.Count > 0)
        {
            switch (Type)
            {
                case 1: StartCoroutine(nomalattack()); break;
                case 2: StartCoroutine(wideattack()); break;
                case 3: StartCoroutine(slowattack()); break;
                case 4: StartCoroutine(multiattack()); break;
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
        yield return new WaitForSeconds(0.1f);
        if (target)
        {
            target.GetComponent<Enemy>().Hp -= Damage;
            Destroy(Bullet);
        }
    }
    IEnumerator wideattack()
    {
        GameObject target = (GameObject)FindMob.ToArray()[0];
        WideBullet.transform.position = target.transform.position;
        WideBullet.transform.localScale = new Vector3(Scope + 1, Scope + 1, Damage);
        WideBullet.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        WideBullet.SetActive(false);
    }
    IEnumerator slowattack()
    {
        GameObject target = (GameObject)FindMob.ToArray()[0];
        SlowBullet.transform.position = target.transform.position;
        SlowBullet.transform.localScale = new Vector3(Scope + 1, Scope + 1, Damage);
        SlowBullet.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        SlowBullet.SetActive(false);
    }
    IEnumerator multiattack()
    {
        GameObject[] target = new GameObject[MultiTargetCount];
        GameObject[] Bullet = new GameObject[MultiTargetCount];
        for (int i = 0; i < MultiTargetCount; i++)
        {
            target[i] = (GameObject)FindMob.ToArray()[i];
            Bullet[i] = Instantiate(bullet, transform.position, transform.rotation);
            Bullet[i].GetComponent<bullet_move>().target = target[i];
        }
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < MultiTargetCount; i++)
        {
            if (target[i]) target[i].GetComponent<Enemy>().Hp -= Damage;
            Destroy(Bullet[i]);
        }
    }
    public void Upgrade()
    {
        if(Level++ < 3){
            switch (Type)
            {
                case 1: Damage += 2; AttackSpeed -= 0.5f; Price += 10; break;
                case 2: Damage += 1; Price += 5; Scope += 1; if(Level==3) AttackSpeed -= 1; break;
                case 3: Price += 30; SlowValue *= 2; break;
                case 4: Damage += 1; Price += 80; MultiTargetCount += Level+2; break;
            }
        }
        if (Level == 3)
        {
            Price = 0;
        }
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
