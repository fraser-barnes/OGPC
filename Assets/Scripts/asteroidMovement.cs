using UnityEngine;

public class asteroidMovement : MonoBehaviour
{
    public Transform target;
    [SerializeField] public Ship ship;
    private int xTarget;


    void Update(){
        xTarget = ship.position.x + Random.Range(-30,31);
    }
}
