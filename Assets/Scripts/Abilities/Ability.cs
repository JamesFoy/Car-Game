using UnityEngine;

//abstract allows functions with no implementation within it
//script setups up basic inforamtion about the ability
public abstract class Ability : ScriptableObject
{
    public string Name = "New Ability";
    public Sprite Sprite;
    public AudioClip Sound;

    public abstract void Initialize(GameObject obj);
    public abstract void TriggerAbility(AbilityDeployModes.DeployStyle style);
}
