
using ECS.Modules.Exerussus.Contact2D.Systems;
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1EasyEcs.Scripts.Custom;
using Leopotam.EcsLite;

namespace ECS.Modules.Exerussus.Contact2D
{
    public class Contact2DGroup : EcsGroup<Contact2DPooler>
    {
        public Contact2DSettings Settings = new Contact2DSettings();

        protected override void OnBeforePoolInitializing(EcsWorld world, Contact2DPooler pooler)
        {
            if (Settings.IsDebug) Pooler.IsDebug = true;
        }

        protected override void SetFixedUpdateSystems(IEcsSystems fixedUpdateSystems)
        {
            if (Settings.Update == UpdateType.FixedUpdate || 
                (Settings.Update != UpdateType.Update &&
                 Settings.Update != UpdateType.LateUpdate &&
                 Settings.Update != UpdateType.TickUpdate)) fixedUpdateSystems.Add(new Contact2DSystem());
        }

        protected override void SetUpdateSystems(IEcsSystems updateSystems)
        {
            if (Settings.Update == UpdateType.Update) updateSystems.Add(new Contact2DSystem());
        }

        protected override void SetLateUpdateSystems(IEcsSystems lateUpdateSystems)
        {
            if (Settings.Update == UpdateType.LateUpdate) lateUpdateSystems.Add(new Contact2DSystem());
        }

        protected override void SetTickUpdateSystems(IEcsSystems tickUpdateSystems)
        {
            if (Settings.Update == UpdateType.TickUpdate) tickUpdateSystems.Add(new Contact2DSystem());
        }
        
        public Contact2DGroup SetUpdateType(UpdateType updateType)
        {
            Settings.Update = updateType;
            return this;
        }
        
        public Contact2DGroup SetDebugMode(bool isEnabled)
        {
            Settings.IsDebug = isEnabled;
            return this;
        }
    }
}