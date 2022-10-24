using EngineBase;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using XAMLEngine;
using XAMLEngine.EngineClasses;

namespace XAMLEngine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Framerate (targeted frames per second)
        private readonly double FRAMERATE = 60d;

        // Timer handles Update loop
        private Timer mainLoop;
        private double _currentTime = 0;
        private bool Awoken = false;
        private bool running = false;


        public void ScreenLoaded(object sender, RoutedEventArgs e)
        {
            Manager.CallScreenEvent(this, "MainWindow");
            MainStart();
            mainLoop.Start();
        }

        public MainWindow()
        {
            InitializeComponent();

            // Globalizing xaml objects
            Manager.canvas = canvasArea;
            Manager.t_end = EndBox;
            Manager.t_high = HighScoreBox;
            Manager.b_end = EndButton;
            Manager.t_score = ScoreBox;

            Persistents.Initialize();
            Score.Initialize();
            Application.Current.Exit += Persistents.OnQuit;

            // Begin Game loop
            mainLoop = new Timer();
            mainLoop.Elapsed += new ElapsedEventHandler(Elapsed);
            mainLoop.Interval = 1000d / FRAMERATE;
            mainLoop.AutoReset = true;
            MainAwake();
        }

        private void Elapsed(object sender, ElapsedEventArgs e)
        {
            MainUpdate(System.DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond - _currentTime);
            _currentTime = System.DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }

        private void MainAwake()
        {
            // Initialize Fields
            Manager.entities = new List<Entity>();
            Manager.instantiationQueue = new List<Entity>();
            Manager.deletionQueue = new List<Entity>();
            Manager.colliders = new List<Entity>();
            Manager.random = new Random();
            Input.keysPressed = new List<Key>();
            Input.keysPressedThisFrame = new List<Key>();
            Input.keysReleasedThisFrame = new List<Key>();
            // Preloads time
            _currentTime = System.DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

            InitializeScripts();

            Manager.entities.AddRange(Manager.instantiationQueue);
            Manager.instantiationQueue.Clear();

            Awoken = true;
        }

        private void MainStart()
        {
            this.Dispatcher.Invoke(() =>
            {
                foreach (Entity entity in Manager.entities)
                {
                    if (entity.IsEnabled)
                        entity.Start();
                }
            });
        }

        private void MainUpdate(double deltaTime)
        {
            if (running)
            {
                Debug.WriteLine($@"Skipped frame at {Manager.runTime}");
                return;
            }
            running = true;
            Manager.entities.AddRange(Manager.instantiationQueue);
            Manager.instantiationQueue.Clear();
            foreach (Entity entity in Manager.deletionQueue)
                Manager.entities.Remove(entity);
            Manager.deletionQueue.Clear();
            Manager.runTime += deltaTime / 1000d;
            if (!Awoken)
                return;
            this.Dispatcher.Invoke(() =>
            {
                Manager.colliders.Clear();
                // Creates current hitboxes every frame
                foreach (Entity entity in Manager.entities)
                {
                    if (entity.IsEnabled)
                    {
                        entity.Update(deltaTime);
                        if (entity.shape != null)
                        {
                            entity.hitbox = new Rect(Canvas.GetLeft(entity.shape), Canvas.GetTop(entity.shape), entity.shape.Width, entity.shape.Height);
                            Manager.colliders.Add(entity);
                        }
                    }
                }
                // Checks all entity collision status
                foreach (Entity first in Manager.colliders)
                {
                    foreach (Entity second in Manager.colliders)
                    {
                        if (second != first)
                        {
                            if (first.hitbox.IntersectsWith(second.hitbox))
                            {
                                if (!first.collidingWith.Contains(second))
                                {
                                    first.collidingWith.Add(second);
                                    first.OnCollisionEnter(second);
                                }
                                first.OnCollision(second);
                            }
                            else if (first.collidingWith.Contains(second))
                            {
                                first.collidingWith.Remove(second);
                                first.OnCollisionExit(second);
                            }
                        }
                    }
                }
            });
            MainLateUpdate();
            running = false;
        }

        private void MainLateUpdate()
        {
            this.Dispatcher.Invoke(() =>
            {
                foreach (Entity entity in Manager.entities)
                {
                    if (entity.IsEnabled)
                    {
                        entity.LateUpdate();
                    }
                }
                Input.MouseClickedLeft = false;
                Input.MouseReleasedLeft = false;
                Input.MouseClickedRight = false;
                Input.MouseReleasedRight = false;
                Input.keysPressedThisFrame.Clear();
                Input.keysReleasedThisFrame.Clear();
            });
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                Input.MouseClickedLeft = true;
                Input.MouseHeldLeft = true;
            });
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                Input.MouseReleasedLeft = true;
                Input.MouseHeldLeft = false;
            });
        }

        private void OnMouseRightDown(object sender, MouseButtonEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                Input.MouseClickedRight = true;
                Input.MouseHeldRight = true;
            });
        }

        private void OnMouseRightUp(object sender, MouseButtonEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                Input.MouseReleasedRight = true;
                Input.MouseHeldRight = false;
            });
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (!Input.keysPressed.Contains(e.Key))
            {
                Input.keysPressedThisFrame.Add(e.Key);
                Input.keysPressed.Add(e.Key);
            }
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (Input.keysPressed.Contains(e.Key))
            {
                Input.keysPressed.Remove(e.Key);
                Input.keysReleasedThisFrame.Add(e.Key);
            }
        }

        private void ShutdownWindow(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void InitializeScripts()
        {
            
        }
    }
}
