using UnityEngine;

public class AttackBirdOutBlock : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AttackBird"))
        {
            GameManager.Instance.GameOver();
        }
    }
}
