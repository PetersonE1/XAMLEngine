using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using XAMLEngine;

namespace XAMLEngine
{
    public static class Manager
    {
        // Engine Settings
        public static readonly bool borders = true;

        // Static references to xaml objects
        public static Random? random;
        public static Canvas? canvas;
        public static TextBox t_end;
        public static TextBox t_score;
        public static TextBox t_high;
        public static Button b_end;

        // Global Variables
        public static double runTime = 0d;
        public static List<Entity>? entities;
        public static List<Entity>? instantiationQueue;
        public static List<Entity>? deletionQueue;
        public static List<Entity>? colliders;

        public static event ScreenLoadedHandler screenHandler;

        public static void CallScreenEvent(object sender, string name)
        {
            if (screenHandler != null)
                screenHandler(sender, new ScreenLoadedEventArgs(name));
        }

        public static void Invoke(Action action)
        {
            Application.Current.Dispatcher.Invoke(action);
        }

        public static void EndGame()
        {
            foreach (Entity entity in entities)
            {
                entity.SetActive(false);
            }
            t_end.IsEnabled = true;
            t_end.Visibility = Visibility.Visible;
            b_end.IsEnabled = true;
            b_end.Visibility = Visibility.Visible;

            int tempScore = Convert.ToInt32(Persistents.GetValue("highscore", "0"));
            if (Score.score > tempScore)
            {
                tempScore = Score.score;
                Persistents.SetValue("highscore", tempScore.ToString());
            }
            t_high.Text = $@"High Score: {tempScore}";
            t_high.IsEnabled = true;
            t_high.Visibility = Visibility.Visible;
        }
    }

    public delegate void ScreenLoadedHandler(object source, ScreenLoadedEventArgs e);

    public class ScreenLoadedEventArgs : EventArgs
    {
        private string ScreenName;
        public ScreenLoadedEventArgs(string name)
        {
            ScreenName = name;
        }
        public string GetInfo()
        {
            return ScreenName;
        }
    }
}
