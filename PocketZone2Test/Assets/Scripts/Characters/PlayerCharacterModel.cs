using System;
using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
    public enum CharacterModelPartTypes
    {
        Head,
        Torso,
        Arms,
        Legs,
        Bag
    }
    
    public class PlayerCharacterModel : CharacterModel
    {
        [SerializeField] private CharacterModelPart _head;
        [SerializeField] private CharacterModelPart _torso;
        [SerializeField] private CharacterModelPart _leftArm;
        [SerializeField] private CharacterModelPart _rightArm;
        [SerializeField] private CharacterModelPart _leftLeg;
        [SerializeField] private CharacterModelPart _rightLeg;
        [SerializeField] private CharacterModelPart _bag;

        public void ChangeBodyPart(CharacterModelPartTypes type, Sprite sprite)
        {
            var parts = GetParts(type);

            foreach (var part in parts)
            {
                part.ChangeBody(sprite);
            }
        }
        
        public void ChangeItemPart(CharacterModelPartTypes type, Sprite sprite)
        {
            var parts = GetParts(type);

            foreach (var part in parts)
            {
                part.ChangeItem(sprite);
            }
        }
        
        private List<CharacterModelPart> GetParts(CharacterModelPartTypes type)
        {
            var result = new List<CharacterModelPart>();

            switch (type)
            {
                case CharacterModelPartTypes.Head:
                    result.Add(_head);
                    break;
                case CharacterModelPartTypes.Torso:
                    result.Add(_torso);
                    break;
                case CharacterModelPartTypes.Arms:
                    result.Add(_leftArm);
                    result.Add(_rightArm);
                    break;
                case CharacterModelPartTypes.Legs:
                    result.Add(_leftLeg);
                    result.Add(_rightLeg);
                    break;
                case CharacterModelPartTypes.Bag:
                    result.Add(_bag);
                    break;
            }
            
            return result;
        }
    }
}
