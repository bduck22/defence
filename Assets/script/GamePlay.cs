using UnityEngine;

public class GamePlay : MonoBehaviour
{
    public GameObject FighterJet;

    public GameObject HpBar;
    void Start()
    {
        Summon();
    }
    void Update()
    {
        //0-1-2-3-4-7전투기1
        //0-1-6-5-4-7전투기2

        //0-1-2-3-4-5-6-2-3-4-7요격기1
        //0-1-6-5-4-3-2-6-5-4-7요격기2

        //1-7수송기

        //0-1-2-6-2-3-4-7폭격기1
        //0-1-6-2-6-5-4-7폭격기2
    }
    void Summon()
    {
        GameObject mob = Instantiate(FighterJet, transform.position, transform.rotation);
        mob.GetComponent<Enemy>().MovePattern = new int[] {0, 1, 2, 3};
        Instantiate(HpBar, mob.transform);
    }
}
