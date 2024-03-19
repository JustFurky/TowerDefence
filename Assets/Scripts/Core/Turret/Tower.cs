using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private int health;

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Debug.Log("Game Over!");
        }
    }
}
