using System;
using ECS.Modules.Exerussus.Contact2D.MonoBehaviours;
using Exerussus._1EasyEcs.Scripts.Core;
using UnityEngine;

namespace ECS.Modules.Exerussus.Contact2D
{
    public static class Contact2DExtensions
    {
        public static void AddCollisionProcess(this Contact2DPooler pooler, Contact2DDetector first, Contact2DDetector second)
        {
            if (pooler.HandlerFilter.TryGetFirstEntity(out var handlerEntity))
            {
                ref var handlerData = ref pooler.ReadOnlyCollisionHandler.Get(handlerEntity);

                var hashCode = GenerateHash(first.ID, second.ID);
                    
                if (handlerData.ExistingProcessesHash.Contains(hashCode)) return;

                var process = new CollisionProcess
                {
                    HashCode = hashCode,
                    First = new EntityInfo { Collider2D = first.Collider2D, Entity = first.EcsPackedEntity, HasEntity = true },
                    Second = new EntityInfo { Collider2D = second.Collider2D, Entity = second.EcsPackedEntity, HasEntity = true }
                };
                
                handlerData.Processes.Enqueue(process);
                handlerData.ExistingProcessesHash.Add(hashCode);
                if (pooler.IsDebug) pooler.ProcessesDebug.Add(process);
            }
        }
        public static void AddCollisionProcess(this Contact2DPooler pooler, Contact2DDetector first, Collider2D second)
        {
            if (pooler.HandlerFilter.TryGetFirstEntity(out var handlerEntity))
            {
                ref var handlerData = ref pooler.ReadOnlyCollisionHandler.Get(handlerEntity);

                var hashCode = GenerateHash(first.ID, second.GetHashCode());
                
                if (handlerData.ExistingProcessesHash.Contains(hashCode)) return;

                var process = new CollisionProcess
                {
                    HashCode = hashCode,
                    First = new EntityInfo { Collider2D = first.Collider2D, Entity = first.EcsPackedEntity, HasEntity = true },
                    Second = new EntityInfo { Collider2D = second, HasEntity = false }
                };
                
                handlerData.Processes.Enqueue(process);
                handlerData.ExistingProcessesHash.Add(hashCode);
                if (pooler.IsDebug) pooler.ProcessesDebug.Add(process);
            }
        }
        
        public static int GenerateHash(int firstId, int secondId)
        {
            var minId = Math.Min(firstId, secondId);
            var maxId = Math.Max(firstId, secondId);

            unchecked
            {
                var hash = 17;
                hash = hash * 31 + minId;
                hash = hash * 31 + maxId;
                return hash;
            }
        }
    }
}