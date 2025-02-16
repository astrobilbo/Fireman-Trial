using System;
using System.Collections.Generic;
using FiremanTrial.PhysicsInteraction;
using UnityEngine;

namespace FiremanTrial.InteraciveObjects
{
   public class InteractiveObject : MonoBehaviour, IRayCastInteractalble, ISphereInteractable
   {
      public Action OnRangeAction;
      public Action OutRangeAction;
      public Action StartInteractionActions;
      public Action<bool> StartBoolInteractionActions;
      public Action EndInteractionAction;
      
      [SerializeField]private bool _externalLock;
      [SerializeField,ColorUsage(hdr:true,showAlpha:true)] protected Color highlightColor = Color.yellow; 

      private bool _onRange;
      private bool wasInRangeWhenLock;
      private bool _isInteracting;
      private List<MeshRenderer> _meshRenderers;
      
      private void Awake()
      {
         _meshRenderers = MeshRendererModifier.GetAllMeshRenderers(gameObject);
      }

      public void OnRange()
      {
         _onRange = true;  
         if (_externalLock) return;
         OnRangeAction?.Invoke();
         MeshRendererModifier.ApplyEmissionHighlight(_meshRenderers, highlightColor);
      }

      public void OutRange()
      {
         _onRange = false;
         wasInRangeWhenLock = false;
         OutRangeAction?.Invoke();
         MeshRendererModifier.RemoveEmissionHighlight(_meshRenderers);
      }

      public string Name() => name;

      public void InteractionOnView()
      {
         if (_isInteracting) return;
         _isInteracting = true;
      }
      public void StartInteraction()
      {
         StartInteractionActions?.Invoke();
         Debug.Log("Start Interaction");
      }
      public void StartInteraction(bool interactionValue)
      {
         Debug.Log("Start Interaction with bool");
         StartBoolInteractionActions?.Invoke(interactionValue);
      }
      public void EndInteraction()
      {
         Debug.Log(" is interacting" + _isInteracting);
         _isInteracting = false;
         EndInteractionAction?.Invoke();
      }

      public bool CanInteract() => _isInteracting && !_externalLock && _onRange;

      public void ExternalInteractionLock(bool locked)
      {
         if (_onRange && locked)
         {
            OutRange();
            wasInRangeWhenLock = true;
         }
         _externalLock = locked;
         if ((!wasInRangeWhenLock && !_onRange) || locked) return;
         OnRange();
      }
   }
}