using UnityEngine;

namespace GameLogic.Characters
{
    public enum CharacterDirection
    {
        Right,
        Left
    }
    
    public static class CharacterRotator
    {
        public static void RotateTo(CharacterDirection targetDirection, Transform character)
        {
            var targetScale = new Vector3(Mathf.Abs(character.localScale.x), character.localScale.y, character.localScale.z);
            
            switch (targetDirection)
            {
                case CharacterDirection.Right:
                    character.localScale = targetScale;
                    break;
                case CharacterDirection.Left:
                    targetScale.x *= -1;
                    character.localScale = targetScale;
                    break;
            }
        }
    }
}
