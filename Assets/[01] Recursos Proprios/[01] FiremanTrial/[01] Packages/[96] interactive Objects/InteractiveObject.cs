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
      public Action OnViewEnterAction;
      public Action StartInteractionActions;
      public Action<bool> StartBoolInteractionActions;
      public Action EndInteractionAction;
      
      [SerializeField]private bool _externalLock;
      [SerializeField,ColorUsage(hdr:true,showAlpha:true)] protected Color highlightColor = Color.yellow; 

      private bool _onRange;
      private bool wasInRangeWhenLock;
      private bool wasInViewWhenLock;
      private bool _isInteracting;
      private List<MeshRenderer> _meshRenderers;
      
      private void Start()
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
         if (_externalLock) return;
         OnViewEnterAction?.Invoke();
      }
      public void StartInteraction()
      {
         StartInteractionActions?.Invoke();
         if(_onRange) MeshRendererModifier.RemoveEmissionHighlight(_meshRenderers);
      }
      public void StartInteraction(bool interactionValue)
      {
         StartBoolInteractionActions?.Invoke(interactionValue);
         if(_onRange) MeshRendererModifier.RemoveEmissionHighlight(_meshRenderers);
      }
      public void EndInteraction()
      {
         if(!_isInteracting) return;
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

         if (_isInteracting && locked)
         {
            EndInteraction();
            wasInViewWhenLock = true;
         }
         
         _externalLock = locked;
         if ((wasInRangeWhenLock || _onRange) && !locked) OnRange();
         if ((wasInViewWhenLock && _isInteracting) || locked) InteractionOnView();
      }
   }
}