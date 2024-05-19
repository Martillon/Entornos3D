using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Events;

public class TimeLineActivator : MonoBehaviour
{
    public PlayableDirector playableDirector;
    private string playerTAG = "Player";
    public Transform interactionLocation;
    public bool autoActivate = false;

    [Header("Activation Zone Event")] 
    
    public UnityEvent OnPlayerEnter;
    public UnityEvent OnPlayerExit;

    [Header("TimeLine Events")] 
    
    public UnityEvent OnTimeLineStart;
    public UnityEvent OnTimeLineEnd;

    private bool isPlaying;
    private bool playerInside;
    private Transform playerTransform;
    
    void Update()
    {
        if(playerInside && !isPlaying && autoActivate) PlayTimeLine();
    }

    private void PlayTimeLine()
    {
        if(playerTransform && interactionLocation)
            playerTransform.SetPositionAndRotation(interactionLocation.position, interactionLocation.rotation);

        if (autoActivate) playerInside = false;

        if (playableDirector)
        {
            playableDirector.Play();
            
            OnTimeLineStart.Invoke();
        }

        isPlaying = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals(playerTAG))
        {
            playerInside = true;
            playerTransform = other.transform;
            OnPlayerEnter.Invoke();
            PlayerTPSController.OnInteractionInput += PlayTimeLine;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals(playerTAG))
        {
            playerInside = false;
            playerTransform = null;
            OnPlayerExit.Invoke();
            PlayerTPSController.OnInteractionInput -= PlayTimeLine;
        }
    }

    void OnPlayableDirectorStopped(PlayableDirector playable)
    {
        OnTimeLineEnd.Invoke();
        isPlaying = false;
    }

    private void OnEnable()
    {
        playableDirector.stopped += OnPlayableDirectorStopped;
    }
    
    private void OnDisable()
    {
        playableDirector.stopped -= OnPlayableDirectorStopped;
    }
}
