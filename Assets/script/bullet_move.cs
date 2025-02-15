using UnityEngine;

public class bullet_move : MonoBehaviour
{
    public GameObject target;
    [SerializeField] private float speed;
    void Start()
    {
        
    }
    void Update()
    {
        if(target)transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }
}
