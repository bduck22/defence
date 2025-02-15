using UnityEngine;
using UnityEngine.UI;
public enum Mob_Type
{
    FighterJet,
    Interceptor,
    TransportAircraft,
    Bomber
}
public class Enemy : MonoBehaviour
{
    public int Max_Hp;
    public int Hp;
    public Mob_Type Type;
    public float Speed;
    public float DownedSpeed;
    public int[] MovePattern;
    [SerializeField] private int NextSpot;
    public GameObject Hpbar;
    GameObject Spot;

    void Start()
    {
        DownedSpeed = 0;
        NextSpot= 0;
        Spot = GameObject.FindWithTag("Spot");
        Hp = Max_Hp;
        Hpbar = transform.GetChild(1).gameObject;
        Hpbar.transform.SetParent(GameObject.FindWithTag("Canvas").transform);
    }
    void Update()
    {
        if (Hp <= 0)
        {
            Destroy(Hpbar);
            GameManager.instance.EnemyCount--;
            GameManager.instance.Gold += 3 + ((int)Type + ((int)Type == 0 ? 0 : 1)) + (int)Type + ((int)Type > 1 ? (int)Type - 1 : 0);
            Destroy(gameObject);
        }
        Hpbar.transform.position = Camera.main.WorldToScreenPoint(transform.position-new Vector3(0,0.65f,0));
        Hpbar.transform.GetChild(0).GetComponent<Image>().fillAmount = (float)Hp / Max_Hp;
        Move(MovePattern[NextSpot]);
    }
    void Dead()
    {
        //3+((int)Type+((int)Type==0?0:1))+(int)Type+((int)Type>1?(int)Type-1:0)
    }
    void Move(int Spot)
    {
        Vector3 stopcheck = transform.position;
        transform.position = Vector2.MoveTowards(transform.position, this.Spot.transform.GetChild(Spot).position, (Speed-DownedSpeed/100)*Time.deltaTime);
        if (stopcheck == transform.position) if (NextSpot < MovePattern.Length - 1)
            {
                NextSpot++;
                transform.eulerAngles = new Vector3(0, 0, Mathf.Round((Quaternion.FromToRotation(Vector2.up, transform.position - this.Spot.transform.GetChild(MovePattern[NextSpot]).position).eulerAngles.z+180) / 90) * 90);
            }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("End")) {
            GameManager.instance.EnemyCount--;
            GameManager.instance.Hp--;
            Destroy(Hpbar);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Attack"))
        {
            Hp -= (int)collision.transform.localScale.z;
        }
    }
}
