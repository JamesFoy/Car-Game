﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Scriptable Objects/Game Event")]
public class GameEvent : ScriptableObject
{
    bool eventHasBeenRaised;

    //When you create a game event create a new empty list of things listening to it
    public List<GameEventListener> listeners = new List<GameEventListener>();

    //trigger event and find every listener for the event and trigger it on them
    public void Raise()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            if (listeners[i] != null && !eventHasBeenRaised)
            {
                eventHasBeenRaised = true;
                listeners[i].OnEventRaised();
            }
        }

        eventHasBeenRaised = false;
    }

    //Tell the event "This is listening"
    public void RegisterListener(GameEventListener listener)
    {
        listeners.Add(listener);
    }

    public void UnregisterListener(GameEventListener listener)
    {
        listeners.Remove(listener);
    }
}
