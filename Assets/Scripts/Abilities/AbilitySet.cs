using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AbilitySet : ScriptableObject
{
    public string abilityName;

    public List<PowerupAbility> powerupAbilities;
    public List<ProjectileAbility> projectileAbilities;
    //public List<TeleportAbility> teleportAbilities;
}
