using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AbilityHolder : ScriptableObject
{
    public string abilityName;

    public List<PowerupAbility> powerupAbilities;
    public List<ProjectileAbility> projectileAbilities;
}
