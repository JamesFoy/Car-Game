using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceRacers.TutorialState
{
    public class TutorialState2 : TutorialState
    {
        float savedRot;
        float initialRot;
        float deltaAngle = 30;
        private void Awake()
        {
            initialRot = GameObject.FindGameObjectWithTag("Player").transform.localEulerAngles.y;
        }
        private void Start()
        {
            AssignPlayerCarInfo();
            savedRot = AngleSigning(playerCarInfo.transform.localEulerAngles.y);
            if (Mathf.Abs(initialRot - savedRot) > deltaAngle)
            {
                tutorialConditionSatisfied = true;
            }
        }
        private void Update()
        {
            float currentRot = AngleSigning(playerCarInfo.transform.localEulerAngles.y);
            float rotDifference = Mathf.Abs(savedRot - currentRot);
            if (rotDifference > deltaAngle)
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