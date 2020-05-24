using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceRacers.TutorialState
{
    public class TutorialState : MonoBehaviour
    {
        [SerializeField] protected bool tutorialConditionSatisfied = false;
        protected CarInfo playerCarInfo;

        protected void AssignPlayerCarInfo()
        {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<CarInfo>() != null)
            {
                playerCarInfo = GameObject.FindGameObjectWithTag("Player").GetComponent<CarInfo>();
            }
            else
            {
                Debug.Log("no player car detected for tutorial!");
                playerCarInfo = null;
            }
        }
        public bool IsTutorialConditionSatisfied()
        {
            return tutorialConditionSatisfied;
        }
    }
}