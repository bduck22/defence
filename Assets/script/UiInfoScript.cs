using TMPro;
using UnityEngine;

public class UiInfoScript : MonoBehaviour
{
    enum UiType
    {
        Gold,
        Hp,
        Wave,
        Stage,
        EnemyCount,
        Time
    }
    [SerializeField] UiType type;
    TMP_Text text;
    void Start()
    {
        text = GetComponent<TMP_Text>();
    }
    void Update()
    {
        switch (type)
        {
            case UiType.Gold:text.text = "Money : " + GameManager.instance.Gold.ToString(",##0"); break;
            case UiType.Hp: text.text = "HP : " + GameManager.instance.Hp.ToString(",##0"); break;
            case UiType.Wave: text.text = "WAVE : " + GameManager.instance.NowWave.ToString(",##0")+"/"+ GameManager.instance.MaxWave.ToString(",##0"); break;
            case UiType.Stage: text.text = RomaNumber(GameManager.instance.Stage, "STAGE "); Destroy(GetComponent<UiInfoScript>()); break;
            case UiType.EnemyCount: text.text = "ENEMY : " + GameManager.instance.EnemyCount.ToString(",##0"); break;
            case UiType.Time: text.text = "NEXT WAVE " + (GameManager.instance.NextWaveTime/60).ToString("00 : ")+(GameManager.instance.NextWaveTime%60).ToString("00"); break;
        }
    }
    string RomaNumber(int number, string Number)
    {
        //1 5 10 50 100
        //93
        int[] Romanums = { 1, 5, 10, 50, 100, 500, 1000, 5000};
        string[] Romastrings = { "I", "V", "X", "L", "C", "D", "M", "F" };
        for (int i = 0; i < Romanums.Length; i++)
        {
            if(number == Romanums[i])
            {
                number-=Romanums[i];
                Number += Romastrings[i];
                break;
            }
            if(number < Romanums[i])//9
            {                
                if(i%2!=0&&number >= Romanums[i] - Romanums[i - 1])//43 == 40
                {
                    number -= Romanums[i] - Romanums[i - 1];
                    Number += Romastrings[i-1] + Romastrings[i];
                }
                else if(i % 2 == 0 && i >1&&number >= Romanums[i] - Romanums[i - 2])//93 == 90
                {
                    number -= Romanums[i] - Romanums[i - 2];
                    Number += Romastrings[i - 2] + Romastrings[i];
                }
                else//33
                {
                    for(int j = number; j >= Romanums[i - 1]; j -= Romanums[i - 1])
                    {
                        number -= Romanums[i-1];
                        Number += Romastrings[i-1];
                    }
                }
                break;
            }
        }
        if (number == 0)
        {
            return Number;
        }
        else return RomaNumber(number, Number);
    }
}
