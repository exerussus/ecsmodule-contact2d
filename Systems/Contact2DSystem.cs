using System.Collections.Generic;
using ECS.Modules.Exerussus.Contact2D.MonoBehaviours;
using Exerussus._1EasyEcs.Scripts.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace ECS.Modules.Exerussus.Contact2D.Systems
{
    public class Contact2DSystem : EasySystem<Contact2DPooler>
    {
        private EcsFilter _handlerFilter;
        private List<Collision2DProcess> _processes;
        
        protected override void Initialize()
        {
            _handlerFilter = World.Filter<ReadOnlyContact2DData.CollisionHandler>().End();
            
            var entity = World.NewEntity();
            ref var handlerData = ref Pooler.ReadOnlyCollisionHandler.Add(entity);
            handlerData.Detectors = new Dictionary<Collider2D, Contact2DDetector>(64);
            handlerData.DetectorIdCounter = new IndexCounter();
            handlerData.CollisionIdCounter = new IndexCounter();
            handlerData.Processes = new Queue<Collision2DProcess>(64);
            handlerData.ExistingProcessesHash = new HashSet<int>(64);
        }

        protected override void Update()
        {
            foreach (var handlerEntity in _handlerFilter)
            {
                ref var handlerData = ref Pooler.ReadOnlyCollisionHandler.Get(handlerEntity);

                if (Pooler.IsDebug) Pooler.ProcessesDebug.Clear();
                handlerData.ExistingProcessesHash.Clear();
                
                for (int i = handlerData.Processes.Count - 1; i >= 0; i--)
                {
                    var collisionProcess = handlerData.Processes.Dequeue();

                    if (collisionProcess.Second.HasEntity)
                    {
                        Signal.RegistryRaise(new Contact2DSignals.OnContactEntity
                        {
                            CollisionId = handlerData.CollisionIdCounter.FreeId,
                            First = collisionProcess.First.Entity,
                            FirstCollider = collisionProcess.First.Collider2D,
                            Second = collisionProcess.Second.Entity,
                            SecondCollider = collisionProcess.Second.Collider2D
                        });
                    }
                    else
                    {
                        Signal.RegistryRaise(new Contact2DSignals.OnContactCollider
                        {
                            CollisionId = handlerData.CollisionIdCounter.FreeId,
                            First = collisionProcess.First.Entity,
                            FirstCollider = collisionProcess.First.Collider2D,
                            SecondCollider = collisionProcess.Second.Collider2D
                        });
                    }
                }
            }
        }
    }
    
}