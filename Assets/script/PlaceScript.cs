using UnityEngine;
using UnityEngine.EventSystems;

public class PlaceScript : MonoBehaviour
{
    bool one;
    void Start()
    {
        one = true;
    }
    void Update()
    {
        if (GameManager.instance.ST != SeletedTower.Wait && one)
        {
            one = false;
            foreach (EventTrigger child in transform.GetComponentsInChildren<EventTrigger>())
            {
                child.enabled = true;
            }
        }
        else if(GameManager.instance.ST == SeletedTower.Wait)
        {
            one = true;
            foreach (EventTrigger child in transform.GetComponentsInChildren<EventTrigger>())
            {
                child.enabled = false;
            }
        }
    }
    public void HoverObject(GameObject me)
    {
        GameManager.instance.HoverObject = me;
    }
    public void ExitObject()
    {
        GameManager.instance.HoverObject = null;
    }
}
