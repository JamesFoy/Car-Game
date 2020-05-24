using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceRacers.TutorialState
{
    public class TutorialState2 : TutorialState
    {
        float savedRot;
        private void Start()
        {
            AssignPlayerCarInfo();
            savedRot = AngleSigning(playerCarInfo.transform.localEulerAngles.y);
        }
        private void Update()
        {
            float currentRot = AngleSigning(playerCarInfo.transform.localEulerAngles.y);
            float rotDifference = Mathf.Abs(savedRot - currentRot);
            if (rotDifference > 30)
            {
                tutorialConditionSatisfied = true;
            }
        }
        float AngleSigning(float angleToCheck)
        {
            if (angleToCheck > 180)
            {
                return angleToCheck - 360;
            }
            else
            {
                return angleToCheck;
            }
        }
    }
}