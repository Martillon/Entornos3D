using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTPSController : MonoBehaviour
{
    public Camera cam;
        private InputData input;
        private CharacterAnimBasedMovement characterMovement;

        public bool blockInput { get; set; }
        public bool onInteractionZone { get; set; }

        public static event Action OnInteractionInput;
        
        void Start()
        {
            characterMovement=GetComponent<CharacterAnimBasedMovement>();
    
        }
    
        // Update is called once per frame
        void Update()
        {
            if(blockInput) input.ResetInput();
            else input.getInput();

            if (onInteractionZone && input.jump) OnInteractionInput?.Invoke();
            else characterMovement.MoveCharacter(input.hMovement, input.vMovement, cam, input.jump, input.dash, input.dance);
            
        }

}
