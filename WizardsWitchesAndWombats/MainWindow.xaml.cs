using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WizardsWitchesAndWombats
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            LeftCharacter.Visibility = System.Windows.Visibility.Collapsed;
            RightCharacter.Visibility = System.Windows.Visibility.Collapsed;
            
            DoubleGood.Visibility = System.Windows.Visibility.Collapsed;
            BadLeft.Visibility = System.Windows.Visibility.Collapsed;
            BadRight.Visibility = System.Windows.Visibility.Collapsed;
            DoubleBad.Visibility = System.Windows.Visibility.Collapsed;

            CharacterLeftLabel.Visibility = System.Windows.Visibility.Collapsed;
            CharacterRightLabel.Visibility = System.Windows.Visibility.Collapsed;
            CharacterLeftPointsLabel.Visibility = System.Windows.Visibility.Collapsed;
            CharacterRightPointsLabel.Visibility = System.Windows.Visibility.Collapsed;
            RoundLabel.Visibility = System.Windows.Visibility.Collapsed;
            RoundLabelCount.Visibility = System.Windows.Visibility.Collapsed;
            MatchLabel.Visibility = System.Windows.Visibility.Collapsed;
            MatchLabelVS.Visibility = System.Windows.Visibility.Collapsed;
            VersusLabel.Visibility = System.Windows.Visibility.Collapsed;

            CharacterScores.Visibility = System.Windows.Visibility.Collapsed;
            SpeedSelector.Visibility = System.Windows.Visibility.Collapsed;

            SpeedValue.Minimum = _ShortestInterval;
            SpeedValue.Maximum = _LongestInterval;
            _SpeedInterval = _ShortestInterval;
            SpeedValue.Value = _SpeedInterval;


            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(150);
            timer.Tick += new EventHandler(timerTick);
            timer.Start();

        }

        private void timerTick(object sender, EventArgs e)
        {
            Cloud1.Margin = new Thickness(Cloud1.Margin.Left + 0.75, Cloud1.Margin.Top, 0, 0);
            Cloud2.Margin = new Thickness(Cloud2.Margin.Left - 0.35, Cloud2.Margin.Top, 0, 0);
            Cloud3.Margin = new Thickness(Cloud3.Margin.Left + 0.5, Cloud3.Margin.Top, 0, 0);

            if (Cloud1.Margin.Left > 1950)
            {
                Cloud1.Margin = new Thickness(0-Cloud1.ActualWidth-150, Cloud1.Margin.Top, 0, 0);
            }

            if (Cloud2.Margin.Left < -1* Cloud2.ActualWidth)
            {
                Cloud2.Margin = new Thickness(1980, Cloud2.Margin.Top, 0, 0);
            }

            if (Cloud3.Margin.Left > 1950)
            {
                Cloud3.Margin = new Thickness(0 - Cloud3.ActualWidth - 150, Cloud3.Margin.Top, 0, 0);
            }
        }

        private int _ShortestInterval = 50;
        private int _LongestInterval = 2000;
        private int _SpeedInterval;

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Application.Current.Shutdown();
            }
        }

        private void BeginBtnClick(object sender, MouseButtonEventArgs e)
        {
            BeginBtn.Visibility = System.Windows.Visibility.Collapsed;
            Run();
        }

        private void Run()
        {
            SpeedSelector.Visibility = System.Windows.Visibility.Visible;

            CharacterLeftLabel.Text = string.Empty;
            CharacterRightLabel.Text = string.Empty;
            CharacterLeftPointsLabel.Text = string.Empty;
            CharacterRightPointsLabel.Text = string.Empty;
            CharacterScores.Text = string.Empty;
            
            System.ComponentModel.BackgroundWorker bw = new System.ComponentModel.BackgroundWorker();
            bw.DoWork += (o, s) =>
            {
                Dictionary<Type, int?> roundRobin = new Dictionary<Type, int?>();
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                foreach (Type type in assembly.GetTypes())
                {
                    if (!type.IsAbstract && typeof(Character).IsAssignableFrom(type))
                    {
                        roundRobin.Add(type, null);
                    }
                }
                
                int numRounds = 10;
                Random random = new Random();

                List<Type> shuffledRoundRobin = roundRobin.Select(x => x.Key).OrderBy(x => random.Next()).ToList();
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    if (shuffledRoundRobin.Count > 1)
                    {
                        VersusLabel.Visibility = System.Windows.Visibility.Visible;

                        Character character1 = (Character)Activator.CreateInstance(shuffledRoundRobin[0]);
                        Character character2 = (Character)Activator.CreateInstance(shuffledRoundRobin[1]);

                        VersusLabel.Text = string.Format("{0} vs. {1}", character1.Name, character2.Name);

                        MatchLabelVS.Visibility = System.Windows.Visibility.Visible;
                        MatchLabelVS.Text = string.Format("Match 1 of {0}", Convert.ToInt32(Math.Ceiling(shuffledRoundRobin.Count * (shuffledRoundRobin.Count - 1) / 2.0)));
                    }
                }));
                System.Threading.Thread.Sleep(_LongestInterval);
                int matchCount = 1;
                for (int i = 0; i < shuffledRoundRobin.Count; i++)
                {
                    for (int j = i + 1; j < shuffledRoundRobin.Count; j++)
                    {
                        Character character1 = (Character)Activator.CreateInstance(shuffledRoundRobin[i]);
                        Character character2 = (Character)Activator.CreateInstance(shuffledRoundRobin[j]);

                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            CharacterLeftLabel.Visibility = System.Windows.Visibility.Visible;
                            CharacterRightLabel.Visibility = System.Windows.Visibility.Visible;
                            CharacterLeftPointsLabel.Visibility = System.Windows.Visibility.Visible;
                            CharacterRightPointsLabel.Visibility = System.Windows.Visibility.Visible;

                            RoundLabel.Visibility = System.Windows.Visibility.Visible;
                            RoundLabelCount.Visibility = System.Windows.Visibility.Visible;
                            MatchLabel.Visibility = System.Windows.Visibility.Visible;
                            VersusLabel.Visibility = System.Windows.Visibility.Collapsed;

                            MatchLabelVS.Visibility = System.Windows.Visibility.Collapsed;
                            MatchLabel.Text = string.Format("Match {0} of {1}", matchCount, Convert.ToInt32(Math.Ceiling(shuffledRoundRobin.Count * (shuffledRoundRobin.Count-1)/2.0)));

                            CharacterScores.Visibility = System.Windows.Visibility.Visible;
                            LeftCharacter.CharacterType = character1.CharacterType;
                            RightCharacter.CharacterType = character2.CharacterType;
                            LeftCharacter.Visibility = System.Windows.Visibility.Visible;                            
                            RightCharacter.Visibility = System.Windows.Visibility.Visible;
            
                            CharacterLeftLabel.Text = character1.Name;
                            CharacterRightLabel.Text = character2.Name;
                        }));

                        int character1Score = 0;
                        int character2Score = 0;

                        for (int k = 0; k < numRounds; k++)
                        {
                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                RoundLabelCount.Text = (k + 1).ToString();

                                CharacterLeftPointsLabel.Text = string.Empty;
                                CharacterRightPointsLabel.Text = string.Empty;
                                DoubleGood.Visibility = System.Windows.Visibility.Collapsed;
                                BadLeft.Visibility = System.Windows.Visibility.Collapsed;
                                BadRight.Visibility = System.Windows.Visibility.Collapsed;
                                DoubleBad.Visibility = System.Windows.Visibility.Collapsed;
                            }));

                            System.Threading.Thread.Sleep((_LongestInterval+_ShortestInterval) - _SpeedInterval);

                            Spell action1 = character1.GetNextAction();
                            Spell action2 = character2.GetNextAction();

                            if (action1 == Spell.Light && action2 == Spell.Light)
                            {
                                character1Score += (int)Outcome.Supreme;
                                character2Score += (int)Outcome.Supreme;
                                character1.Report(Outcome.Supreme);
                                character2.Report(Outcome.Supreme);

                                this.Dispatcher.BeginInvoke(new Action(() =>
                                {

                                    CharacterLeftPointsLabel.Text = Outcome.Supreme == 0 ? "No points!" : (Outcome.Supreme < 0 ? ((int)Outcome.Supreme).ToString() : string.Format("+{0}", (int)Outcome.Supreme));
                                    CharacterRightPointsLabel.Text = Outcome.Supreme == 0 ? "No points!" : (Outcome.Supreme < 0 ? ((int)Outcome.Supreme).ToString() : string.Format("+{0}", (int)Outcome.Supreme));

                                    DoubleBad.Visibility = System.Windows.Visibility.Collapsed;
                                    BadLeft.Visibility = System.Windows.Visibility.Collapsed;
                                    BadRight.Visibility = System.Windows.Visibility.Collapsed;
                                    DoubleGood.Visibility = System.Windows.Visibility.Visible;
                                }));
                            }
                            else if (action1 == Spell.Light && action2 == Spell.Dark)
                            {
                                character1Score += (int)Outcome.LightBlocked;
                                character2Score += (int)Outcome.BlockedLight;

                                character1.Report(Outcome.LightBlocked);
                                character2.Report(Outcome.BlockedLight);

                                this.Dispatcher.BeginInvoke(new Action(() =>
                                {
                                    CharacterLeftPointsLabel.Text = Outcome.LightBlocked == 0 ? "No points!" : (Outcome.LightBlocked < 0 ? ((int)Outcome.LightBlocked).ToString() : string.Format("+{0}", (int)Outcome.LightBlocked));
                                    CharacterRightPointsLabel.Text = Outcome.BlockedLight == 0 ? "No points!" : (Outcome.BlockedLight < 0 ? ((int)Outcome.BlockedLight).ToString() : string.Format("+{0}", (int)Outcome.BlockedLight));

                                    DoubleGood.Visibility = System.Windows.Visibility.Collapsed;
                                    DoubleBad.Visibility = System.Windows.Visibility.Collapsed;
                                    BadLeft.Visibility = System.Windows.Visibility.Collapsed;
                                    BadRight.Visibility = System.Windows.Visibility.Visible;
                                }));
                            }
                            else if (action1 == Spell.Dark && action2 == Spell.Light)
                            {
                                character1Score += (int)Outcome.BlockedLight;
                                character2Score += (int)Outcome.LightBlocked;

                                character1.Report(Outcome.BlockedLight);
                                character2.Report(Outcome.LightBlocked);

                                this.Dispatcher.BeginInvoke(new Action(() =>
                                {
                                    CharacterLeftPointsLabel.Text = Outcome.BlockedLight == 0 ? "No points!" : (Outcome.BlockedLight<0? ((int)Outcome.BlockedLight).ToString() : string.Format("+{0}", (int)Outcome.BlockedLight));
                                    CharacterRightPointsLabel.Text = Outcome.LightBlocked == 0 ? "No points!" : (Outcome.LightBlocked < 0 ? ((int)Outcome.LightBlocked).ToString() : string.Format("+{0}", (int)Outcome.LightBlocked));

                                    DoubleGood.Visibility = System.Windows.Visibility.Collapsed;
                                    DoubleBad.Visibility = System.Windows.Visibility.Collapsed;
                                    BadRight.Visibility = System.Windows.Visibility.Collapsed;
                                    BadLeft.Visibility = System.Windows.Visibility.Visible;
                                }));
                            }
                            else if (action1 == Spell.Dark && action2 == Spell.Dark)
                            {
                                character1Score += (int)Outcome.Darkness;
                                character2Score += (int)Outcome.Darkness;

                                character1.Report(Outcome.Darkness);
                                character2.Report(Outcome.Darkness);

                                this.Dispatcher.BeginInvoke(new Action(() =>
                                {
                                    CharacterLeftPointsLabel.Text = Outcome.Darkness == 0 ? "No points!" : (Outcome.Darkness < 0 ? ((int)Outcome.Darkness).ToString() : string.Format("+{0}", (int)Outcome.Darkness));
                                    CharacterRightPointsLabel.Text = Outcome.Darkness == 0 ? "No points!" : (Outcome.Darkness < 0 ? ((int)Outcome.Darkness).ToString() : string.Format("+{0}", (int)Outcome.Darkness));

                                    DoubleGood.Visibility = System.Windows.Visibility.Collapsed;
                                    BadLeft.Visibility = System.Windows.Visibility.Collapsed;
                                    BadRight.Visibility = System.Windows.Visibility.Collapsed;
                                    DoubleBad.Visibility = System.Windows.Visibility.Visible;
                                }));
                            }

                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {

                                CharacterLeftLabel.Text = string.Format("{0}\r\n{1}{2}", character1.Name, character1Score <= 0 ? string.Empty : "+", character1Score);
                                CharacterRightLabel.Text = string.Format("{0}\r\n{1}{2}", character2.Name, character2Score <= 0 ? string.Empty : "+", character2Score);
                            }));

                            System.Threading.Thread.Sleep((_LongestInterval+_ShortestInterval) - _SpeedInterval);

                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                DoubleGood.Visibility = System.Windows.Visibility.Collapsed;
                                BadLeft.Visibility = System.Windows.Visibility.Collapsed;
                                BadRight.Visibility = System.Windows.Visibility.Collapsed;
                                DoubleBad.Visibility = System.Windows.Visibility.Collapsed;

                                CharacterLeftPointsLabel.Text = string.Empty;
                                CharacterRightPointsLabel.Text = string.Empty;
                            }));
                        }
                        
                        if (roundRobin[character1.GetType()].HasValue)
                        {
                            roundRobin[character1.GetType()] += character1Score;
                        }
                        else
                        {
                            roundRobin[character1.GetType()] = character1Score;
                        }
                        
                        if (roundRobin[character2.GetType()].HasValue)
                        {
                            roundRobin[character2.GetType()] += character2Score;
                        }
                        else
                        {
                            roundRobin[character2.GetType()] = character2Score;
                        }

                        var rankings = roundRobin.Where(w=>w.Value.HasValue).OrderByDescending(kvp => kvp.Value);
                        StringBuilder rankingsDisplay = new StringBuilder();
                        for (int r = 0; r < rankings.Count(); r++)
                        {
                            var kvp = rankings.ToArray()[r];

                            Character character = (Character)Activator.CreateInstance(kvp.Key);

                            rankingsDisplay.AppendLine((r + 1) + ". " + character.Name + " (" + kvp.Value + ")");
                        }

                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            LeftCharacter.Visibility = System.Windows.Visibility.Collapsed;
                            RightCharacter.Visibility = System.Windows.Visibility.Collapsed;
                            CharacterLeftLabel.Visibility = System.Windows.Visibility.Collapsed;
                            CharacterRightLabel.Visibility = System.Windows.Visibility.Collapsed;
                            CharacterLeftPointsLabel.Visibility = System.Windows.Visibility.Collapsed;
                            CharacterRightPointsLabel.Visibility = System.Windows.Visibility.Collapsed;
                            RoundLabel.Visibility = System.Windows.Visibility.Collapsed;
                            MatchLabel.Visibility = System.Windows.Visibility.Collapsed;
                            RoundLabelCount.Visibility = System.Windows.Visibility.Collapsed;
                            CharacterScores.Text = rankingsDisplay.ToString();

                            matchCount++;
                            if (i<shuffledRoundRobin.Count-2)
                            {
                                VersusLabel.Visibility = System.Windows.Visibility.Visible;

                                Character nextCharacter1;
                                Character nextCharacter2;

                                if (j < shuffledRoundRobin.Count - 1)
                                {
                                    nextCharacter1 = (Character)Activator.CreateInstance(shuffledRoundRobin[i]);
                                    nextCharacter2 = (Character)Activator.CreateInstance(shuffledRoundRobin[j + 1]);
                                }
                                else
                                {
                                    nextCharacter1 = (Character)Activator.CreateInstance(shuffledRoundRobin[i + 1]);
                                    nextCharacter2 = (Character)Activator.CreateInstance(shuffledRoundRobin[i + 2]);
                                }

                                VersusLabel.Text = string.Format("{0} vs. {1}", nextCharacter1.Name, nextCharacter2.Name);

                                MatchLabelVS.Visibility = System.Windows.Visibility.Visible;
                                MatchLabelVS.Text = string.Format("Match {0} of {1}", matchCount, Convert.ToInt32(Math.Ceiling(shuffledRoundRobin.Count * (shuffledRoundRobin.Count - 1) / 2.0)));
                            }
                        }));

                        if (i < shuffledRoundRobin.Count - 2)
                        {
                            System.Threading.Thread.Sleep(_LongestInterval);
                        }
                    }
                }

                var rankingsFinal = roundRobin.OrderByDescending(kvp => kvp.Value);
                StringBuilder rankingsDisplayFinal = new StringBuilder();
                for (int r = 0; r < rankingsFinal.Count(); r++)
                {
                    var kvp = rankingsFinal.ToArray()[r];
                    rankingsDisplayFinal.AppendLine((r + 1) + ". " + kvp.Key.Name + " (" + kvp.Value + ")");
                }

                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    CharacterScores.Text = rankingsDisplayFinal.ToString();
                    CharacterLeftLabel.Visibility = System.Windows.Visibility.Collapsed;
                    CharacterRightLabel.Visibility = System.Windows.Visibility.Collapsed;
                    CharacterLeftPointsLabel.Visibility = System.Windows.Visibility.Collapsed;
                    CharacterRightPointsLabel.Visibility = System.Windows.Visibility.Collapsed;
                    VersusLabel.Visibility = System.Windows.Visibility.Collapsed;
                    RoundLabel.Visibility = System.Windows.Visibility.Collapsed;
                    MatchLabel.Visibility = System.Windows.Visibility.Collapsed;
                    RoundLabelCount.Visibility = System.Windows.Visibility.Collapsed;

                    SpeedSelector.Visibility = System.Windows.Visibility.Collapsed;
                    CharacterLeftLabel.Text = string.Empty;
                    CharacterRightLabel.Text = string.Empty;
                    BeginBtn.Visibility = System.Windows.Visibility.Visible;
                }));
            };
            bw.RunWorkerAsync();
        }

        private void SpeedValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _SpeedInterval = Convert.ToInt32(SpeedValue.Value);
        }
    }

    public abstract class Character
    {
        public string Name { get; private set; }
        public CharacterTypes CharacterType { get; private set; }
        public abstract Spell GetNextAction();
        public abstract void Report(Outcome Outcome);

        public Character(string Name, CharacterTypes CharacterType)
        {
            this.Name = Name;
            this.CharacterType = CharacterType;
        }
    }

    public enum CharacterTypes { Wizard, Witch, Wombat }
    public enum Spell { Light, Dark }
    public enum Outcome { Supreme = 15, BlockedLight = 5, LightBlocked = -5, Darkness = 0 }
}