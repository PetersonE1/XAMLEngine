using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using XAMLEngine;

namespace XAMLEngine
{
    public class Entity : UIElement
    {
        private bool started = false;

        public string name;
        public Shape shape;
        public Rect hitbox;
        public List<Entity> collidingWith;

        public Entity()
        {
            if (!Manager.entities.Contains(this) && !Manager.instantiationQueue.Contains(this))
                Manager.instantiationQueue.Add(this);
            collidingWith = new List<Entity>();
            Awake();
        }

        public bool Move(Vector2D units)
        {
            bool stopped = false;
            Vector2D target = GetPosition() + units;
            Vector2D target2 = new Vector2D(target);
            if (Manager.borders)
            {
                target2 = new Vector2D(Math.Clamp(target.x, 0, Manager.canvas.ActualWidth - shape.ActualWidth),
                                      Math.Clamp(target.y, 0, Manager.canvas.ActualHeight - shape.ActualHeight));
                if (target != target2)
                    stopped = true;
            }
            Canvas.SetLeft(shape, target2.x);
            Canvas.SetTop(shape, target2.y);
            return stopped;
        }

        public virtual void Start() { started = true; }

        public virtual void Awake() { }

        public virtual void Update(double deltaTime) { }

        public virtual void LateUpdate() { }

        public virtual void OnEnable() { }

        public virtual void OnDisable() { }

        public virtual void OnCollision(Entity collider) { }

        public virtual void OnCollisionEnter(Entity collider) { }

        public virtual void OnCollisionExit(Entity collider) { }

        public void Destroy()
        {
            if (shape != null)
                Manager.canvas.Children.Remove(shape);
            Manager.deletionQueue.Add(this);
        }

        public void SetActive(bool active)
        {
            if (active == this.IsEnabled)
                return;
            this.IsEnabled = active;
            if (active)
            {
                if (!started)
                    Start();
                OnEnable();
                if (shape != null)
                {
                    shape.Visibility = Visibility.Visible;
                }
            }
            else
            {
                OnDisable();
                if (shape != null)
                {
                    shape.Visibility = Visibility.Collapsed;
                }
            }
        }

        public Vector2D GetPosition()
        {
            Vector2D pos = new Vector2D(Canvas.GetLeft(shape), Canvas.GetTop(shape));
            return pos;
        }

        public static T Instantiate<T>(string name, Shape shape, double x = 0, double y = 0) where T : Entity, new()
        {
            T entity = new T() { name = name, shape = shape };
            Canvas.SetLeft(shape, x);
            Canvas.SetTop(shape, y);
            Manager.canvas.Children.Add(shape);
            return entity;
        }

        public static T Instantiate<T>(string name, Shape shape, int z, double x = 0, double y = 0) where T : Entity, new()
        {
            T entity = new T() { name = name, shape = shape };
            Canvas.SetLeft(shape, x);
            Canvas.SetTop(shape, y);
            Canvas.SetZIndex(shape, z);
            Manager.canvas.Children.Add(shape);
            return entity;
        }

        public static T Instantiate<T>(string name) where T : Entity, new()
        {
            T entity = new T() { name = name };
            return entity;
        }

        public static TriggerBox InstantiateTrigger(string name, Shape shape, double x = 0, double y = 0, Action<Entity> enter = null, Action<Entity> leave = null, Action<Entity> stay = null)
        {
            TriggerBox trigger = new TriggerBox(enter, leave, stay) { name = name, shape = shape };
            Canvas.SetLeft(shape, x);
            Canvas.SetTop(shape, y);
            Manager.canvas.Children.Add(shape);
            return trigger;
        }
    }
}
