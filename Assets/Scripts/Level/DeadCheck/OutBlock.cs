using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutBlock : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SoundManager.Instance.PlaySFXSoundByClip(SoundManager.SoundList.OutCollisionSound);
            GameManager.Instance.GameOver();
        }
    }
}
