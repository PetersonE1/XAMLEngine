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
        public static TextBox scoreBox;
        public static int score = 0;

        private static void UpdateScore()
        {
            scoreBox.Text = $@"Score: {score}";
        }

        public static int IncreaseScore()
        {
            return IncreaseScore(1);
        }

        public static int IncreaseScore(int value)
        {
            score += value;
            Manager.Invoke(UpdateScore);
            return score;
        }

        public static int DecreaseScore()
        {
            return DecreaseScore(1);
        }

        public static int DecreaseScore(int value)
        {
            score -= value;
            Manager.Invoke(UpdateScore);
            return score;
        }
    }
}
