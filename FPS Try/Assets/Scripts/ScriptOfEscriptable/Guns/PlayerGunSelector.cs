using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]

public class PlayerGunSelector : MonoBehaviour
{
    [SerializeField] private GunType Gun;
    [SerializeField] private Transform GunParent;
    [SerializeField] private List<GunScriptableObject> Guns;
    //[SerializeField] private PlayerIK InverseKinematics;

    [Space]
    [Header("Runtime Filled")]
    public GunScriptableObject ActiveGun;

    private void Start()
    {
        GunScriptableObject gun = Guns.Find(gun => gun.Type == Gun);

        if (gun == null)
        {
            Debug.LogError($"No GunScriptableObject found for GunType: {gun}");
        }
        
        ActiveGun = gun;
        gun.Spawn(GunParent, this);

        //Transform[] allChildren = GunParent.GetComponentInChildren<Transform>();
        //InverseKinematics.LeftElbowIKTarget = allChildren.FirstOrDefault(child => child.name == "LeftElbow");
        //InverseKinematics.RightElbowIKTarget = allChildren.FirstOrDefault(child => child.name == "RightElbow");
        //InverseKinematics.LeftHandElbowIKTarget = allChildren.FirstOrDefault(child => child.name == "LeftHand");
        //InverseKinematics.RightHandElbowIKTarget = allChildren.FirstOrDefault(child => child.name == "Right Hand");
    }
}
