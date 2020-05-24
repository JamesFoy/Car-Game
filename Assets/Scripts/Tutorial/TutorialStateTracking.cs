using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceRacers.TutorialState
{
    public class TutorialStateTracking : MonoBehaviour
    {
        public List<TutorialState> tutorialStates = new List<TutorialState>();
        private void Update()
        {
            if (tutorialStates.Count > 0)
            {
                for (int i = tutorialStates.Count - 1; i >= 0; i--)
                {
                    if (tutorialStates[i].IsTutorialConditionSatisfied() && tutorialStates[i].gameObject.activeSelf)
                    {
                        tutorialStates[i].gameObject.SetActive(false);
                        if (i + 1 < tutorialStates.Count)
                        {
                            tutorialStates[i + 1].gameObject.SetActive(true);
                        }
                        tutorialStates.Remove(tutorialStates[i]);
                    }
                }
            }
        }
    }
}