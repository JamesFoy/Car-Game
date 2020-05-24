using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceRacers.TutorialState
{
    public class TutorialState7 : TutorialState
    {
        private void Start()
        {
            AssignPlayerCarInfo();
        }
        private void Update()
        {
        }
        public void TutorialStateSatisfied()
        {
            tutorialConditionSatisfied = true;
        }
    }
}