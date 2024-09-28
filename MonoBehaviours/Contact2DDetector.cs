
using System.Collections.Generic;
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Extensions.SignalSystem;
using Leopotam.EcsLite;
using UnityEngine;

namespace ECS.Modules.Exerussus.Contact2D.MonoBehaviours
{
    public class Contact2DDetector : MonoBehaviour
    {
        [SerializeField] private bool canInvokeSignals;
        [SerializeField] private bool canTouchNotEntity;
        [SerializeField] private Collider2D objectCollider2D;
        private Signal _signal;
        private Contact2DPooler _pooler;
        
        public EcsPackedEntity EcsPackedEntity { get; private set; }
        public Collider2D Collider2D => objectCollider2D;
        public Dictionary<Collider2D,Contact2DDetector> Detectors { get; private set; }
        public bool HasOtherDetector { get; private set; }
        public bool IsInitialized { get; private set; }
        public int ID { get; private set; }
        
        public void Initialize(
            EcsPackedEntity ecsPackedEntity, 
            Signal signal, 
            Contact2DPooler contactPooler)
        {
            EcsPackedEntity = ecsPackedEntity;
            _signal = signal;
            _pooler = contactPooler;

            if (_pooler.HandlerFilter.TryGetFirstEntity(out var handlerEntity))
            {
                ref var handlerData = ref _pooler.CollisionHandler.Get(handlerEntity);
                Detectors = handlerData.Detectors;
                Detectors[objectCollider2D] = this;
                ID = handlerData.DetectorIdCounter.FreeId;
                IsInitialized = true;
            }
        }

        public void Deinitialize()
        {
            if (IsInitialized && _pooler != null && Detectors.ContainsKey(objectCollider2D))
            {
                Detectors.Remove(objectCollider2D);
                IsInitialized = false;
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!IsInitialized) return;
            HasOtherDetector = false;
            if (!canInvokeSignals) return;
            
            if (Detectors.TryGetValue(other.collider, out var otherDetector)) _pooler.AddCollisionProcess(this, otherDetector);
            else if (canTouchNotEntity) _pooler.AddCollisionProcess(this, other.collider);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!IsInitialized) return;
            HasOtherDetector = false;
            if (!canInvokeSignals) return;
            
            if (Detectors.TryGetValue(other, out var otherDetector)) _pooler.AddCollisionProcess(this, otherDetector);
            else if (canTouchNotEntity) _pooler.AddCollisionProcess(this, other);
        }

        private void OnDestroy()
        {
            Deinitialize();
        }
    }
}