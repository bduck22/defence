using System.Collections;
using UnityEngine;

public class GamePlay : MonoBehaviour
{
    public GameObject FighterJet;

    public GameObject HpBar;
    void Start()
    {
        GameManager.instance.StartGame();
        StartCoroutine(Delay());
    }
    void Update()
    {
        //0-1-2-3-4-7������1
        //0-1-6-5-4-7������2

        //0-1-2-3-4-5-6-2-3-4-7��ݱ�1
        //0-1-6-5-4-3-2-6-5-4-7��ݱ�2

        //1-7���۱�

        //0-1-2-6-2-3-4-7���ݱ�1
        //0-1-6-2-6-5-4-7���ݱ�2
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2);
        Summon();
        StartCoroutine(Delay());
    }
    void Summon()
    {
        GameManager.instance.EnemyCount++;
        GameObject mob = Instantiate(FighterJet, transform.position, transform.rotation);
        mob.GetComponent<Enemy>().MovePattern = new int[] {0, 1, 2, 3, 4, 7};
        Instantiate(HpBar, mob.transform);
    }
}
