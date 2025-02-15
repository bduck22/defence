using System.Collections;
using UnityEngine;

public enum SeletedTower
{
    Wait,
    Nomal,
    Wide,
    Slow,
    Multi
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Update()
    {
        NextWaveTime-=Time.deltaTime;
        if (NextWaveTime < 0)
        {
            if (NowWave <= MaxWave)
            {
                NowWave++;
                NextWaveTime = 180;
            }
        }
    }

    public int Stage;

    public int Gold;
    public int Hp;

    public int MaxWave;
    public int NowWave;

    public float NextWaveTime;

    public int EnemyCount;

    public SeletedTower ST = SeletedTower.Wait;

    public GameObject HoverObject;

    public bool CanUseGold(int count)
    {
        if (Gold - count >= 0)
        {
            Gold -= count;
            return true;
        }
        else return false;
    }
}
