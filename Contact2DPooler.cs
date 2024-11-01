
using System.Collections.Generic;
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1EasyEcs.Scripts.Custom;
using Leopotam.EcsLite;

namespace ECS.Modules.Exerussus.Contact2D
{
    public class Contact2DPooler : IGroupPooler
    {
        public virtual void Initialize(EcsWorld world)
        {
            ContactDetector = new PoolerModule<Contact2DData.ContactDetector>(world);
            ReadOnlyCollisionHandler = new PoolerModule<ReadOnlyContact2DData.CollisionHandler>(world);
            HandlerFilter = world.Filter<ReadOnlyContact2DData.CollisionHandler>().End();
        }

        public bool IsDebug;
        public List<Collision2DProcess> ProcessesDebug;
        public EcsFilter HandlerFilter;
        public PoolerModule<Contact2DData.ContactDetector> ContactDetector;
        public PoolerModule<ReadOnlyContact2DData.CollisionHandler> ReadOnlyCollisionHandler;
    }
}