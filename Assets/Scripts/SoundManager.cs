using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager sm;

    public AudioSource source;
    public AudioSource music;

    public AudioClip menuMusic;
    public AudioClip gameMusic;

    public AudioClip menuSound;
    public AudioClip potionSound;
    public AudioClip bookSound;
    public AudioClip scytheSound;
    public AudioClip chestSound;
    public AudioClip gemSound;
    public AudioClip hitSound;

    private bool canPlayGem = true;
    private bool canPlayHit = true;

    private void Awake()
    {
        if (sm == null)
        {
            sm = this;
        }
        else if (sm != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public AudioClip randomClip(AudioClip[] clips)
    {
        int i = Random.Range(0, clips.Length);
        return clips[i];
    }

    public void PlayMenuMusic()
    {
        if (music.isPlaying && music.clip == menuMusic) return;

        music.volume = 0.8f;
        music.clip = menuMusic;
        music.Play();
    }

    public void PlayGameMusic()
    {
        if (music.isPlaying && music.clip == gameMusic) return;

        music.volume = 0.1f;
        music.clip = gameMusic;
        music.Play();
    }

    public void PlayMenuSound()
    {
        source.PlayOneShot(menuSound);
    }

    public void PlayPotionSound()
    {
        source.PlayOneShot(potionSound);
    }

    public void PlayBookSound()
    {
        source.PlayOneShot(bookSound);
    }

    public void PlayScytheSound()
    {
        source.PlayOneShot(scytheSound);
    }

    public void PlayGemSound()
    {
        if (canPlayGem)
        {
            StartCoroutine(GemPickup());
        }
        
    }

    public void PlayChestSound()
    {
        source.PlayOneShot(chestSound,0.3f);
    }

    public void PlayHitSound()
    {
        if (canPlayHit)
        {
            StartCoroutine(PlayHit());
        }
    }

    IEnumerator GemPickup()
    {
        canPlayGem = false;
        source.PlayOneShot(gemSound,0.2f);
        yield return new WaitForSeconds(0.05f);
        canPlayGem = true;
    }

    IEnumerator PlayHit()
    {
        canPlayHit = false;
        source.PlayOneShot(hitSound,0.05f);
        yield return new WaitForSeconds(0.05f);
        canPlayHit = true;
    }



    //public void PlayMagicSound()
    //{
    //    if (!Data.sound) return;
    //    soundSource.PlayOneShot(magicSound);
    //}

    //public void PlayCriticalSound()
    //{
    //    if (!Data.sound) return;
    //    soundSource.PlayOneShot(randomClip(criticalSounds), 0.5f);
    //}
}
