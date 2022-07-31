using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutBlock : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.CameraShake(0.5f, 2f);
            SoundManager.Instance.PlaySFXSoundByClip(SoundManager.SoundList.OutCollisionSound);
            GameManager.Instance.GameOver();
        }
    }
}
