using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SfxManager : MonoBehaviour
{
    public AudioClip penSound;
    public AudioClip eraserSound;
    public AudioClip bucketSound;
    
    [SerializeField] private AudioSource audioSource;
    
    public void PlaySound(SoundType soundType)
    {
        switch (soundType)
        {
            case SoundType.Pen:
                PlaySingleSound(penSound);
                break;
            case SoundType.Eraser:
                PlaySingleSound(eraserSound);
                break;
            case SoundType.Bucket:
                PlaySingleSound(bucketSound);
                break;
        }
    }

    private void PlaySingleSound(AudioClip soundClip)
    {
        
            if (!audioSource.isPlaying)
            {
                audioSource.clip = soundClip;
                audioSource.Play();
            }
    }

}

public enum SoundType
{
    Pen,
    Eraser,
    Bucket,
    PaintballExplosion,
    Stamp,
    PaintGun
}

