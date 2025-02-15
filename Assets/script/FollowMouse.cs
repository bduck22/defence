using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public bool IsDestroy;
    void Start()
    {
        
    }
    void Update()
    {

        if(GameManager.instance.HoverObject != null)
        {
            transform.position = GameManager.instance.HoverObject.transform.position;
            if (Input.GetMouseButtonDown(0))
            {
                if (!IsDestroy)
                {
                    if(GameManager.instance.CanUseGold(((int)GameManager.instance.ST > 1 ? 1 : 0) * 10 + 10))
                    {
                        GetComponent<Tower>().Type = (int)GameManager.instance.ST;
                        transform.GetChild(1).gameObject.SetActive(true);
                        transform.GetChild(0).gameObject.SetActive(true);
                        transform.GetChild(2).gameObject.SetActive(true);
                        transform.GetChild(5).gameObject.SetActive(true);
                        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                        GetComponent<CircleCollider2D>().enabled = true;
                        GetComponent<Tower>().enabled = true;
                        GameManager.instance.HoverObject = null;
                        GameManager.instance.ST = SeletedTower.Wait;

                        Destroy(GetComponent<FollowMouse>());
                    }
                }
                else
                {
                    Destroy(GameManager.instance.HoverObject);
                }
            }
        }
        else transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
