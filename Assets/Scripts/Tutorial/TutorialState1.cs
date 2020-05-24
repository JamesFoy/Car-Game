using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceRacers.TutorialState
{
    public class TutorialState1 : TutorialState
    {
        private void Start()
        {
            AssignPlayerCarInfo();
        }
        private void Update()
        {
            if (playerCarInfo.carStats.speed > 20)
            {
                tutorialConditionSatisfied = true;
            }
        }
    }
}