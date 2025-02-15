using System.Collections;
using UnityEngine;

public class GamePlay : MonoBehaviour
{
    int[][][] Enemy;
    public GameObject FighterJet;

    public GameObject HpBar;
    void Start()
    {
        Enemy = new int[2][][];
        Enemy[0] = new int[10][];
        Enemy[0][0] = new int[] { 1, 2, 1, 2 , 1, 2, 5, 1, 2};
        Enemy[0][1] = new int[] { 1, 1, 2, 2, 1, 1, 2, 2, 1 };
        Enemy[0][2] = new int[] { 1, 1, 1, 2, 2, 2, 1, 2, 1 };
        Enemy[0][3] = new int[] { 2, 2, 1, 1, 1, 2, 2, 1, 2 };
        Enemy[0][4] = new int[] { 2, 1, 2, 2, 1, 2, 1, 1, 2 };
        Enemy[0][5] = new int[] { 1, 1, 2, 1, 2, 2, 2, 1, 5 };
        Enemy[0][6] = new int[] { 1, 1, 1, 2, 2, 2, 2, 1, 2 };
        Enemy[0][7] = new int[] { 1, 2, 1, 2, 3, 4, 3, 4, 5 };
        Enemy[0][8] = new int[] { 3, 1, 4, 2, 3, 2, 4, 1, 1 };
        Enemy[0][9] = new int[] { 3, 3, 1, 1, 4, 4, 2, 2, 1 };
        StartCoroutine(Summon());
    }
    int Wave=1;
    void Update()
    {
        if (Wave!=GameManager.instance.NowWave)
        {
            StartCoroutine(Summon());
        }
        Wave = GameManager.instance.NowWave;
        //0-1-2-3-4-7전투기1
        //0-1-6-5-4-7전투기2

        //0-1-2-3-4-5-6-2-3-4-7요격기1
        //0-1-6-5-4-3-2-6-5-4-7요격기2

        //1-7수송기

        //0-1-2-6-2-3-4-7폭격기1
        //0-1-6-2-6-5-4-7폭격기2
    }
    IEnumerator Summon()
    {
        yield return new WaitForSeconds(3);
        foreach (int Type in Enemy[GameManager.instance.Stage-1][GameManager.instance.NowWave-1])
        {
            if (Type < 3)
            {
                for (int i = 0; i < 2; i++)
                {
                    summon(Type);
                    yield return new WaitForSeconds(0.5f);
                }
            }
            summon(Type);
            yield return new WaitForSeconds(1.5f);
        }
    }
    int[] Jet1 = { 0, 1, 2, 3, 4, 7 };
    int[] Jet2 = { 0, 1, 6, 5, 4, 7 };
    int[] Ceptor1 = { 0, 1, 2, 3, 4, 5, 6, 2, 3, 4, 7 };
    int[] Ceptor2 = { 0, 1, 6, 5, 4, 3, 2, 6, 5, 4, 7 };
    int[] Transport = { 0, 1, 7 };
    int[] Bomber1 = { 0, 1, 2, 6, 2, 3, 4, 7 };
    int[] Bomber2 = { 0, 1, 6, 2, 6, 5, 4, 7 };
    void summon(int Type)
    {
        GameManager.instance.EnemyCount++;
        GameObject mob = Instantiate(FighterJet, transform.position, transform.rotation);
        int[] seletedPattern = new int[] { };
        switch (Type)
        {
            case 1: seletedPattern = Jet1;mob.GetComponent<Enemy>().Max_Hp = 5; mob.GetComponent<Enemy>().Type = Mob_Type.FighterJet; break;
            case 2: seletedPattern = Jet2; mob.GetComponent<Enemy>().Max_Hp = 5; mob.GetComponent<Enemy>().Type = Mob_Type.FighterJet; break;

            case 3: seletedPattern = Ceptor1; mob.GetComponent<Enemy>().Max_Hp = 15; mob.GetComponent<Enemy>().Type = Mob_Type.Interceptor; break;
            case 4: seletedPattern = Ceptor2; mob.GetComponent<Enemy>().Max_Hp = 15; mob.GetComponent<Enemy>().Type = Mob_Type.Interceptor; break;

            case 5: seletedPattern = Transport; mob.GetComponent<Enemy>().Max_Hp = 10; mob.GetComponent<Enemy>().Type = Mob_Type.TransportAircraft; break;

            case 6: seletedPattern = Bomber1; mob.GetComponent<Enemy>().Max_Hp = 15; mob.GetComponent<Enemy>().Type = Mob_Type.Bomber; break;
            case 7: seletedPattern = Bomber2; mob.GetComponent<Enemy>().Max_Hp = 15; mob.GetComponent<Enemy>().Type = Mob_Type.Bomber; break;
        }
        mob.GetComponent<Enemy>().MovePattern = seletedPattern;
        Instantiate(HpBar, mob.transform);
    }
}
