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
            /// <summary> Уникальный идентификатор столкновения </summary>
            public int CollisionId;
            /// <summary> Первая запакованная сущность </summary>
            public EcsPackedEntity First;
            public Collider2D FirstCollider;
            /// <summary> Вторая запакованная сущность </summary>
            public EcsPackedEntity Second;
            public Collider2D SecondCollider;
        }
        
        /// <summary>
        /// Срабатывает при контакте ContactDetector с объектом на сцене, который не обладает ContactDetector.
        /// </summary>
        public struct OnContactCollider
        {
            /// <summary> Уникальный идентификатор столкновения </summary>
            public int CollisionId;
            /// <summary> Первая запакованная сущность </summary>
            public EcsPackedEntity First;
            public Collider2D FirstCollider;
            public Collider2D SecondCollider;
        }
    }
}