using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceRacers.TutorialState
{
    public class TutorialState3 : TutorialState
    {
        private void Start()
        {
            AssignPlayerCarInfo();
        }
        private void Update()
        {
            if (playerCarInfo.carStats.speed < 3)
            {
                tutorialConditionSatisfied = true;
            }
        }
    }
}