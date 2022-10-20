using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace XAMLEngine
{
    public static class Score
    {
        private static TextBox scoreBox;
        public static int score = 0;

        public static void Initialize()
        {
            scoreBox = Manager.t_score;
        }

        private static void UpdateScore()
        {
            scoreBox.Text = $@"Score: {score}";
        }

        public static int IncreaseScore(int value = 1)
        {
            score += value;
            Manager.Invoke(UpdateScore);
            return score;
        }

        public static int DecreaseScore(int value = 1)
        {
            score -= value;
            Manager.Invoke(UpdateScore);
            return score;
        }
    }
}
