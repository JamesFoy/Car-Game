using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceRacers.TutorialState
{
    public class TutorialState5 : TutorialState
    {
        private void Start()
        {
            AssignPlayerCarInfo();
            StartCoroutine(WaitForSeconds(5));
        }
        private void Update()
        {
        }
        IEnumerator WaitForSeconds(int seconds)
        {
            yield return new WaitForSeconds(seconds);
            tutorialConditionSatisfied = true;
        }
    }
}