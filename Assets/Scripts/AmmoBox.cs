using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    // amount of ammo in the box
    public int ammoAmmount = 200;
    public AmmoType ammoType;

    // types of ammo
    public enum AmmoType
    {
        RifleAmmo,
        PistolAmmo
    }
}
