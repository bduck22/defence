using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public GameObject destroy;
    private void OnEnable()
    {
        destroy.SetActive(true);
    }
    void Update()
    {
        
    }
}
