using System;
using System.Collections.Generic;
using Exerussus._1EasyEcs.Scripts.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace ECS.Modules.Exerussus.Contact2D
{
    public static class Contact2DData
    {
        public struct ContactDetector : IEcsComponent
        {
            public MonoBehaviours.Contact2DDetector Value;
        }
    }
    public static class ReadOnlyContact2DData
    {
        public struct CollisionHandler : IEcsComponent
        {
            public Dictionary<Collider2D, MonoBehaviours.Contact2DDetector> Detectors;
            public IndexCounter CollisionIdCounter;
            public IndexCounter DetectorIdCounter;
            public Queue<CollisionProcess> Processes;
            public HashSet<int> ExistingProcessesHash;
        }
    }
    
    [Serializable]
    public struct CollisionProcess
    {
        public int HashCode;
        
        public EntityInfo First;
        public EntityInfo Second;
    }

    [Serializable]
    public struct EntityInfo
    {
        public bool HasEntity;
        public EcsPackedEntity Entity;
        public Collider2D Collider2D;
    }
    
    [Serializable]
    public class IndexCounter
    {
        private int _index;
        
        public int FreeId => _index++;
    }
}