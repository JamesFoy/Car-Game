using UnityEngine;

//This scriptable object is the base object for all other ability scriptable objects
public abstract class Ability : ScriptableObject
{
    public string Name = "New Ability"; //Variable for the name
    public Sprite Sprite; //Variable for a sprite
    public AudioClip Sound; //Variable for a sound

    public abstract void Initialize(GameObject obj); //Method that will be overriden for Initializing an ability to a specific weapon holders script (this is based on which ability needs which script)
    public abstract void TriggerAbility(AbilityDeployModes.DeployStyle style); //Method that will be overriden for Triggering each ability based on a deploy style
}
