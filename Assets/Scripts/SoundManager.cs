using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Weapon;

public class SoundManager : MonoBehaviour
{
    // manager for the sound managed in the editor
    public static SoundManager Instance { get; set; }

    public AudioSource ShootingChannel;
    
    public AudioClip P1911Shot;
    public AudioClip M4Shot;

    public AudioSource reloadingSoundM4;
    public AudioSource reloadingSound1911;

    public AudioSource emptyMagazineSound1911;

    public AudioClip zombieWalking;
    public AudioClip zombieChase;
    public AudioClip zombieAttack;
    public AudioClip zombieHurt;
    public AudioClip zombieDeath;

    public AudioSource zombieChannel;
    public AudioSource zombieChannel2;

    public AudioSource playerChannel;
    public AudioClip playerHurt;
    public AudioClip playerDie;

    public AudioClip gameOverMusic;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // sound for the shooting
    public void PlayShootingSound(WeaponModel weapon)
    {
        switch(weapon)
        {
            case WeaponModel.Pistol1911:
                ShootingChannel.PlayOneShot(P1911Shot);
                break;
            case WeaponModel.M4:
                ShootingChannel.PlayOneShot(M4Shot);
                break;
            
        }
        
    }

    // sound for the reloading
    public void PlayReloadSound(WeaponModel weapon)
    {
        switch(weapon)
        {
            case WeaponModel.Pistol1911:
                reloadingSound1911.Play();
                break;
            case WeaponModel.M4:
                reloadingSoundM4.Play();
                break;
            
        }

    }
}
