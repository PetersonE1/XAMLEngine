using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace XAMLEngine
{
    public class TriggerBox : Entity
    {
        public Action<Entity> TriggerEnter;
        public Action<Entity> TriggerLeave;
        public Action<Entity> TriggerStay;

        public TriggerBox(Action<Entity> enter, Action<Entity> leave, Action<Entity> stay)
        {
            Debug.WriteLine("New Trigger");
            TriggerEnter = enter;
            TriggerLeave = leave;
            TriggerStay = stay;
        }

        public override void OnCollisionEnter(Entity collider)
        {
            TriggerEnter?.Invoke(collider);
        }

        public override void OnCollisionExit(Entity collider)
        {
            TriggerLeave?.Invoke(collider);
        }

        public override void OnCollision(Entity collider)
        {
            TriggerStay?.Invoke(collider);
        }
    }
}
