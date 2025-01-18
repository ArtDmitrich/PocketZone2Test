using System.Collections.Generic;
using Characters.Characters;
using UnityEngine;

namespace GameLogic.Characters
{
    public enum CharacterModelPartTypes
    {
        Head,
        Torso,
        ArmsElbow,
        ArmsWrist,
        Legs,
    }
    
    public class HumanoidCharacterModel : CharacterModel
    {
        [SerializeField] private CharacterModelPart _head;
        [SerializeField] private CharacterModelPart _torso;
        [SerializeField] private CharacterModelPart _leftArmElbow;
        [SerializeField] private CharacterModelPart _leftArmWrist;
        [SerializeField] private CharacterModelPart _rightArmWrist;
        [SerializeField] private CharacterModelPart _leftLeg;
        [SerializeField] private CharacterModelPart _rightLeg;

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

        public void TakeOffItemPart(CharacterModelPartTypes type)
        {
            var parts = GetParts(type);

            foreach (var part in parts)
            {
                part.TakeOffItem();
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
                case CharacterModelPartTypes.ArmsElbow:
                    result.Add(_leftArmElbow);
                    break;
                case CharacterModelPartTypes.ArmsWrist:
                    result.Add(_leftArmWrist);
                    result.Add(_rightArmWrist);
                    break;
                case CharacterModelPartTypes.Legs:
                    result.Add(_leftLeg);
                    result.Add(_rightLeg);
                    break;
            }
            
            return result;
        }
    }
}
