using System;
using GameLogic.ItemDispatcher;
using Services.GameEvents;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Services.UI
{
    public class AmmunitionCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _counterText;
        [SerializeField] private string _animationTrigger;
        
        private Animator Anim { get { return _anim ??= GetComponent<Animator>(); } }
        private Animator _anim;
        
        private AmmunitionController _ammunitionController;
        private IGameEvent _gameEvent;

        [Inject]
        private void Construct(AmmunitionController ammunitionController, IGameEvent gameEvent)
        {
            _ammunitionController = ammunitionController;
            _gameEvent = gameEvent;
        }

        private void SetCounterText(int count)
        {
            _counterText.text = count.ToString();
        }

        private void PlayAmmoNotEnoughtAnimation()
        {
            Anim.SetTrigger(_animationTrigger);
        }

        private void Start()
        {
            _ammunitionController.AmmunitionCountChanged += SetCounterText;
            _gameEvent.AddSub(GameEventType.AmmoNotEnought, PlayAmmoNotEnoughtAnimation);
        }

        private void OnDestroy()
        {
            _ammunitionController.AmmunitionCountChanged -= SetCounterText;
            _gameEvent.RemoveSub(GameEventType.AmmoNotEnought, PlayAmmoNotEnoughtAnimation);
        }
        // private void OnEnable()
        // {
        //     _ammunitionController.AmmunitionCountChanged += SetCounterText;
        //     _gameEvent.AddSub(GameEventType.AmmoNotEnought, PlayAmmoNotEnoughtAnimation);
        // }
        //
        // private void OnDisable()
        // {
        //     _ammunitionController.AmmunitionCountChanged -= SetCounterText;
        //     _gameEvent.RemoveSub(GameEventType.AmmoNotEnought, PlayAmmoNotEnoughtAnimation);
        // }
    }
}
