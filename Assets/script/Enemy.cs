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
    [SerializeField] private int Max_Hp;
    public int Hp;
    [SerializeField] private Mob_Type Type;
    [SerializeField] private float Speed;
    public int[] MovePattern;
    [SerializeField] private int NextSpot;
    public GameObject Hpbar;
    GameObject Spot;

    void Start()
    {
        NextSpot= 0;
        Spot = GameObject.FindWithTag("Spot");
        Hp = Max_Hp;
        Hpbar = transform.GetChild(0).gameObject;
        Hpbar.transform.SetParent(GameObject.FindWithTag("Canvas").transform);
    }
    void Update()
    {
        if (Hp <= 0)
        {
            Destroy(Hpbar);
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
        transform.position = Vector2.MoveTowards(transform.position, this.Spot.transform.GetChild(Spot).position, Speed*Time.deltaTime);
        if (stopcheck == transform.position) if(NextSpot<MovePattern.Length-1)NextSpot++;
    }
}
