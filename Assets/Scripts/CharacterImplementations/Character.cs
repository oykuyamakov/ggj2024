using System.Collections.Generic;
using UnityEngine;

namespace CharacterImplementations
{
    [CreateAssetMenu(fileName = "Character", menuName = "CharacterImplementations/Character", order = 1)]
    public class Character : ScriptableObject
    {
        [SerializeField]
        private List<Sprite> m_IdleAnimationSprites;
        [SerializeField]
        private List<Sprite> m_IdleBackAnimationSprites;
        [SerializeField]
        private List<Sprite> m_IdleLeftAnimationSprites;
        [SerializeField]
        private List<Sprite> m_WalkBackAnimationSprites;
        [SerializeField]
        private List<Sprite> m_WalkFrontAnimationSprites;    
        [SerializeField]
        private List<Sprite> m_WalkLeftAnimationSprites; 
        
        [SerializeField]
        private List<Sprite> m_EatingAnimationSprites;
        
        [SerializeField]
        private List<Sprite> m_PeeingAnimationSprites;
        
        [SerializeField]
        private List<Sprite> m_DeathAnimationSprites;
        
        [SerializeField]
        private List<Sprite> m_SelectedAnimationSprites;
        
        [SerializeField]
        private List<Sprite> m_SadAnimationSprites;
        
        [SerializeField]
        private List<Sprite> m_HappyAnimationSprites;
        
        [SerializeField]
        private List<Sprite> m_SittingAnimationSprites;
        
        [SerializeField]
        private List<Sprite> m_OpenMouseAnimationSprites;
        
        public List<Sprite> IdleAnimationSprites => m_IdleAnimationSprites;
        public List<Sprite> WalkBackAnimationSprites => m_WalkBackAnimationSprites;
        public List<Sprite> WalkFrontAnimationSprites => m_WalkFrontAnimationSprites;
        public List<Sprite> EatingAnimationSprites => m_EatingAnimationSprites;
        public List<Sprite> PeeingAnimationSprites => m_PeeingAnimationSprites;
        public List<Sprite> IdleBackAnimationSprites => m_IdleBackAnimationSprites;
        public List<Sprite> IdleLeftAnimationSprites => m_IdleLeftAnimationSprites;
        public List<Sprite> DeathAnimationSprites => m_DeathAnimationSprites;
        public List<Sprite> SelectedAnimationSprites => m_SelectedAnimationSprites;
        public List<Sprite> SadAnimationSprites => m_SadAnimationSprites;
        public List<Sprite> HappyAnimationSprites => m_HappyAnimationSprites;
        public List<Sprite> SittingAnimationSprites => m_SittingAnimationSprites;
        public List<Sprite> WalkLeftAnimationSprites => m_WalkLeftAnimationSprites;
        
        public List<Sprite> OpenMouseAnimationSprites => m_OpenMouseAnimationSprites;
        

    }
}
