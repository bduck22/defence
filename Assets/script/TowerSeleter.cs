using UnityEngine;
using UnityEngine.UI;

public class TowerSeleter : MonoBehaviour
{
    [SerializeField] GameObject Tower;
    [SerializeField] bool Spawning;
    [SerializeField] GameObject tower;
    void Start()
    {
        Spawning = false;
    }
    void Update()
    {
        if (GameManager.instance.ST != SeletedTower.Wait)
        {
            if (!Spawning)
            {
                SpawnTower();
            }
        }
        else
        {
            if (tower)
            {
                Spawning=false;
                tower = null;
                TowerSelect(-1);
            }
        }
    }
    void SpawnTower()
    {
        Spawning = true;
        tower = Instantiate(Tower, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.rotation);
        tower.transform.GetChild(1).gameObject.SetActive(false);
        tower.transform.GetChild(0).gameObject.SetActive(false);
        tower.transform.GetChild(2).gameObject.SetActive(false);
        tower.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.8f);
    }
    public void TowerSelect(int select)
    {
        GameManager.instance.ST = (SeletedTower)(select + 1);
        for (int i = 0; i < 4; i++)
        {
            transform.GetChild(i).GetComponent<Image>().color = Color.white;
        }
        if (select >= 0)
        {
            transform.GetChild(select).GetComponent<Image>().color = Color.gray;
        }
        else
        {
            if (tower)
            {
                Destroy(tower);
                Spawning = false;
            }
            gameObject.SetActive(false);
        }
    }
}
