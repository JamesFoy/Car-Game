using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceRacers.TutorialState
{
    public class TutorialState4 : TutorialState
    {
        [SerializeField] GameObject pickupHolder;
        private void Start()
        {
            AssignPlayerCarInfo();
            pickupHolder.SetActive(true);
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