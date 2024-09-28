using Leopotam.EcsLite;
using UnityEngine;

namespace ECS.Modules.Exerussus.Contact2D
{
    public static class Contact2DSignals
    {
        public struct OnContactEntity
        {
            public int CollisionId;
            public EcsPackedEntity First;
            public Collider2D FirstCollider;
            public EcsPackedEntity Second;
            public Collider2D SecondCollider;
        }
        
        public struct OnContactCollider
        {
            public int CollisionId;
            public EcsPackedEntity First;
            public Collider2D FirstCollider;
            public Collider2D SecondCollider;
        }
    }
}