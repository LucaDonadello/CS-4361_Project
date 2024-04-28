using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class HUDManager : MonoBehaviour
{   
    // manager for the HUD
    public static HUDManager Instance { get; set; }

    // UI elements
    [Header("Ammo")]
    public TextMeshProUGUI magazineAmmoUI;
    public TextMeshProUGUI totalAmmoUI;
    public Image ammoTypeUI;

    [Header("Weapon")]
    public Image activeWeaponUI;
    public Image unActiveWeaponUI;

    [Header("Throwables")]
    public Image lethalUI;
    public TextMeshProUGUI lethalAmountUI;

    public Image tactialUI;
    public TextMeshProUGUI tacticalAmountUi;

    public Sprite emptySlot;
    public GameObject middleDot;

    // start to get the instance
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    
    private void Update()
    {
        // update the HUD with the current weapon
        Weapon activeWeapon = WeaponManager.Instance.activeWeaponSlot.GetComponentInChildren<Weapon>();
        Weapon unActiveWeapon = GetUnActiveWeaponSlot().GetComponentInChildren<Weapon>();
        //check if we have a weapon
        if (activeWeapon)
        {
            magazineAmmoUI.text = $"{activeWeapon.bulletsLeft / activeWeapon.bulletsPerBurst}";
            totalAmmoUI.text = $"{WeaponManager.Instance.CheckAmmoLeftFor(activeWeapon.thisWeaponModel)}";

            Weapon.WeaponModel model = activeWeapon.thisWeaponModel;
            ammoTypeUI.sprite = GetAmmoSprite(model);

            activeWeaponUI.sprite = GetWeaponSprite(model);

            if (unActiveWeapon)
            {
                unActiveWeaponUI.sprite = GetWeaponSprite(unActiveWeapon.thisWeaponModel);
            }
        }
        else
        {
            magazineAmmoUI.text = "";
            totalAmmoUI.text = "";

            ammoTypeUI.sprite = emptySlot;

            activeWeaponUI.sprite = emptySlot;
            unActiveWeaponUI.sprite = emptySlot;
        }
    }

    private GameObject GetUnActiveWeaponSlot()
    {
        // get the unactive weapon slot
        foreach (GameObject weaponSlot in WeaponManager.Instance.weaponSlots)
        {
            if (weaponSlot != WeaponManager.Instance.activeWeaponSlot)
            {
                return weaponSlot;
            }
        }
        return null;
    }

    private Sprite GetWeaponSprite(Weapon.WeaponModel model)
    {
        // get the weapon sprite and switch it based on the weapon model
        switch (model)
        {
            case Weapon.WeaponModel.Pistol1911:
                return Resources.Load<Sprite>("Pistol1911_Weapon");

            case Weapon.WeaponModel.M4:
                return Resources.Load<Sprite>("M4_Weapon");

            default:
                return null;

        }
    }

    private Sprite GetAmmoSprite(Weapon.WeaponModel model)
    {
        // get the ammo sprite and switch it based on the weapon model
        switch (model)
        {
            case Weapon.WeaponModel.Pistol1911:
                return Resources.Load<Sprite>("Pistol_Ammo");

            case Weapon.WeaponModel.M4:
                return Resources.Load<Sprite>("Rifle_Ammo");

            default:
                return null;

        }
    }
    
}
