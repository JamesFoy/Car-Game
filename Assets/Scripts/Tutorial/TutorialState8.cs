using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceRacers.TutorialState
{
    public class TutorialState8 : TutorialState
    {
        private void Start()
        {
            AssignPlayerCarInfo();
        }
        private void Update()
        {
            if (playerCarInfo.GetComponent<AbilityInitializer>().CanThisCarTriggerAbility() == false)
            {
                tutorialConditionSatisfied = true;
            }
        }
    }
}