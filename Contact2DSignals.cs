using Leopotam.EcsLite;
using UnityEngine;

namespace ECS.Modules.Exerussus.Contact2D
{
    public static class Contact2DSignals
    {
        /// <summary>
        /// Срабатывает при контакте ContactDetector с другим ContactDetector
        /// </summary>
        public struct OnContactEntity
        {
            public int CollisionId;
            public EcsPackedEntity First;
            public Collider2D FirstCollider;
            public EcsPackedEntity Second;
            public Collider2D SecondCollider;
        }
        
        /// <summary>
        /// Срабатывает при контакте ContactDetector с объектом на сцене, который не обладает ContactDetector.
        /// </summary>
        public struct OnContactCollider
        {
            public int CollisionId;
            public EcsPackedEntity First;
            public Collider2D FirstCollider;
            public Collider2D SecondCollider;
        }
    }
}