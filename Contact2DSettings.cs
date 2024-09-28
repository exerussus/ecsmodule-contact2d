using System.Collections.Generic;
using Exerussus._1EasyEcs.Scripts.Core;

namespace ECS.Modules.Exerussus.Contact2D
{
    public class Contact2DSettings
    {
        public UpdateType Update = UpdateType.FixedUpdate;
        public bool IsDebug = false;
        public List<CollisionProcess> ProcessesDebug;
    }
}