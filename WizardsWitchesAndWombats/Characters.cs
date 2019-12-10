using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizardsWitchesAndWombats
{
    public class Template : Character
    {
        public Template() : base("Name", CharacterTypes.Wizard) { }

        public override Spell GetNextAction()
        {
            return Spell.Dark;
        }

        public override void Report(Outcome Outcome)
        {

        }
    }


    /*HPA Characters
    
    #region Submissions

    public class JasminKunfuPanda : Character
    {
        private int totalpoints = 0;
        private int love = 0;
        private int hate = 0;
        private int inflictedpain = 0;
        private int hurt = 0;

        private int totalPlayers = 5; //Reset after 5 rounds
        private int playerNo = 1;

        Spell nextAction;
        //Outcome currentOutcome;
        //List<Outcome> myOutcome ; 

        public JasminKunfuPanda() : base("KunfuPanda", CharacterTypes.Wombat) { }

        public override void Report(Outcome Outcome)
        {

            //Console.WriteLine(Outcome);
            switch (Outcome)
            {
                case Outcome.Supreme:
                    totalpoints = totalpoints + 2;
                    love++;
                    break;
                case Outcome.BlockedLight:
                    totalpoints = totalpoints + 1;
                    inflictedpain++;
                    break;
                case Outcome.LightBlocked:
                    totalpoints = totalpoints - 1;
                    hurt++;
                    break;
                case Outcome.Darkness:
                    totalpoints = totalpoints + 0;
                    hate++;
                    break;
                default:
                    break;
            }

            if (playerNo == totalPlayers)
            {
                //Resetting 
                love = 0;
                hate = 0;
                inflictedpain = 0;
                hurt = 0;
                playerNo = 1;
            }
            else
            {
                playerNo++;
            }
            //Console.WriteLine(totalpoints);
            //Console.WriteLine(hurt);
            //Console.WriteLine("");
        }

        public override Spell GetNextAction()
        {
            if (love >= hurt || inflictedpain > hate)
            {
                nextAction = Spell.Light;
            }
            else if (love < hurt || inflictedpain <= hate)
            {
                nextAction = Spell.Dark;
            }

            return nextAction;
        }

    }

    public class Ketan : Character
    {

        public Ketan() : base("KJ", CharacterTypes.Witch) { } 
        
        int iterationCount = 0;
        int currentScore = 0;
        public override Spell GetNextAction()
        {
            iterationCount++;

            if (iterationCount < currentScore && currentScore < (iterationCount * 2))
                return Spell.Light;
            else
                return Spell.Dark;
        }
        public override void Report(Outcome Outcome)
        {
            switch (Outcome)
            {
                case Outcome.Supreme:
                    currentScore += 2;
                    break;
                case Outcome.BlockedLight:
                    currentScore += 1;
                    break;
                case Outcome.LightBlocked:
                    currentScore -= 1;
                    break;
            }
        }
    }


    public class Nigel : Character
    {

        public Nigel() : base("Nigel", CharacterTypes.Wizard) { } 

        //unused
        public List<Spell> opponnentsLogicPattern = new List<Spell>();//need to figure out opponent`s logic pattern
        public List<Spell> opponnentsAction = new List<Spell>();
        //used
        public int NumberOfTimesOpponentSentHugInResponseToHug = 0;
        public int NumberOfTimesOpponentSentHugInResponseToPunch = 0;
        public int NumberOfTimesOpponentSentPunchInResponseToHug = 0;
        public int NumberOfTimesOpponentSentPunchInResponseToPunch = 0;
        public List<Spell> myAction = new List<Spell>();
        public int round = 1;
        public override Spell GetNextAction()
        {
            Spell actionToSendThisTime = Spell.Light;
            //if(round <= 100)
            //{
            //    actionToSendThisTime = Action.Hug;//learning opponents algo
            //}
            //else if (round > 100 && round <= 200)
            //{
            //    actionToSendThisTime = Action.Punch;//learning opponents algo
            //}
            //else 
            if (NumberOfTimesOpponentSentHugInResponseToHug >= NumberOfTimesOpponentSentHugInResponseToPunch &&
                 NumberOfTimesOpponentSentHugInResponseToHug >= NumberOfTimesOpponentSentPunchInResponseToHug &&
                 NumberOfTimesOpponentSentHugInResponseToHug >= NumberOfTimesOpponentSentPunchInResponseToPunch
            )
            {
                actionToSendThisTime = Spell.Light;
            }
            else if (NumberOfTimesOpponentSentHugInResponseToPunch >= NumberOfTimesOpponentSentHugInResponseToHug &&
                     NumberOfTimesOpponentSentHugInResponseToPunch >= NumberOfTimesOpponentSentPunchInResponseToHug &&
                     NumberOfTimesOpponentSentHugInResponseToPunch >= NumberOfTimesOpponentSentPunchInResponseToPunch
                )
            {
                actionToSendThisTime = Spell.Dark;
            }
            else if (NumberOfTimesOpponentSentPunchInResponseToHug >= NumberOfTimesOpponentSentHugInResponseToHug &&
                     NumberOfTimesOpponentSentPunchInResponseToHug >= NumberOfTimesOpponentSentHugInResponseToPunch &&
                     NumberOfTimesOpponentSentPunchInResponseToHug >= NumberOfTimesOpponentSentPunchInResponseToPunch
                )
            {
                actionToSendThisTime = Spell.Light;
            }
            else if (NumberOfTimesOpponentSentPunchInResponseToPunch >= NumberOfTimesOpponentSentHugInResponseToHug &&
                     NumberOfTimesOpponentSentPunchInResponseToPunch >= NumberOfTimesOpponentSentHugInResponseToPunch &&
                     NumberOfTimesOpponentSentPunchInResponseToPunch >= NumberOfTimesOpponentSentPunchInResponseToHug
                )
            {
                actionToSendThisTime = Spell.Dark;
            }
            round++;
            myAction.Add(actionToSendThisTime);
            return actionToSendThisTime;
        }
        public override void Report(Outcome Outcome)
        {
            if (Outcome == Outcome.Supreme && myAction[myAction.Count() - 1] == Spell.Light)
            {
                opponnentsAction.Add(Spell.Light);
                NumberOfTimesOpponentSentHugInResponseToHug++;
            }
            else if (Outcome == Outcome.BlockedLight && myAction[myAction.Count() - 1] == Spell.Dark)
            {
                opponnentsAction.Add(Spell.Light);
                NumberOfTimesOpponentSentHugInResponseToPunch++;
            }
            else if (Outcome == Outcome.LightBlocked && myAction[myAction.Count() - 1] == Spell.Light)
            {
                opponnentsAction.Add(Spell.Dark);
                NumberOfTimesOpponentSentPunchInResponseToHug++;
            }
            else if (Outcome == Outcome.Darkness && myAction[myAction.Count() - 1] == Spell.Dark)
            {
                opponnentsAction.Add(Spell.Dark);
                NumberOfTimesOpponentSentPunchInResponseToPunch++;
            }
        }
    }
    public class VincePerson : Character
    {
        public VincePerson() : base("Vince", CharacterTypes.Wizard) { }

        private int ActionCounter = 0;
        private int f0 = 0;
        private int f1 = 0;
        private int fn = 1;

        public List<Outcome> Outcomes = new List<Outcome>();
        public override Spell GetNextAction()
        {
            ActionCounter++;

            while (fn < ActionCounter)
            {
                f0 = f1;
                f1 = fn;
                fn = f1 + f0;
            }

            if (ActionCounter == fn)
            {
                return Spell.Light;
            }
            else if (ActionCounter < fn)
            {
                return Spell.Dark;
            }
            else
            {
                throw new Exception("Final F_n value was invalid.");
            }
        }
        public override void Report(Outcome Outcome)
        {
            Outcomes.Add(Outcome);
        }
    }


    public class RyansThisForThat : Character
    {
        private readonly Dictionary<Outcome, Spell> GiveMeThatIllGiveYouThis;
        private Spell NextAction = Spell.Light;
        public RyansThisForThat() : base("Ryan", CharacterTypes.Wizard)
        {
            GiveMeThatIllGiveYouThis = new Dictionary<Outcome, Spell>()
        {
            {Outcome.Supreme, Spell.Light},
            {Outcome.LightBlocked, Spell.Dark},
            {Outcome.BlockedLight, Spell.Light},
            {Outcome.Darkness, Spell.Dark},
        };
        }
        public override Spell GetNextAction()
        {
            return NextAction;
        }
        public override void Report(Outcome Outcome)
        {
            NextAction = GiveMeThatIllGiveYouThis[Outcome];
        }
    }

    public class HawyoodsSelfish : Character
    {
        
        //    Author: Haywood
        //  Date: 10/10/19


        public HawyoodsSelfish() : base("Selfish", CharacterTypes.Wizard)
        {
            _currentFeeling = Outcome.LightBlocked;      //Start out punching to ensure we don't lose against someone hateful (we'll tie with person who only punches)
            _Score = 0;
            _OpponentScore = 0;
        }
        private Outcome _currentFeeling;
        private int _Score;
        private int _OpponentScore;
        public override Spell GetNextAction()
        {
            //Hug only when the feeling is mutual and we know they like us more than we like them (3 point lead allows us to get hurt without falling behind)
            if ((_currentFeeling == Outcome.Supreme || _currentFeeling == Outcome.BlockedLight) && _Score - _OpponentScore > 2)
            {
                return Spell.Light;      //  :0)
            }
            else
            {
                return Spell.Dark;    //  >:(
            }
        }
        public override void Report(Outcome reportedOutcome)
        {
            _currentFeeling = reportedOutcome;

            switch (reportedOutcome)
            {
                case Outcome.Supreme:
                    _Score += 2;
                    _OpponentScore += 2;
                    break;

                case Outcome.LightBlocked:
                    _Score -= 1;
                    _OpponentScore += 1;
                    break;

                case Outcome.BlockedLight:
                    _Score += 1;
                    _OpponentScore -= 1;
                    break;

                default:    //Hate: No points to either person
                    break;
            }
        }
    }

    public class NicolesProbablityStrategyPerson : Character
    {
        public NicolesProbablityStrategyPerson() : base("Nicole", CharacterTypes.Witch) { }

        List<Outcome> outcomesSoFar = new List<Outcome>();
        List<Spell> actionHistory = new List<Spell>();
        int rounds = 0;
        public override Spell GetNextAction()
        {
            if (rounds > 3)
            {
                //If the other person is doing the same thing for the past 4 rounds, adjust my Action to get the most from theirs.
                if ((outcomesSoFar[rounds - 1] == Outcome.LightBlocked || outcomesSoFar[rounds - 1] == Outcome.Darkness) && (outcomesSoFar[rounds - 2] == Outcome.LightBlocked || outcomesSoFar[rounds - 2] == Outcome.Darkness) && (outcomesSoFar[rounds - 3] == Outcome.LightBlocked || outcomesSoFar[rounds - 3] == Outcome.Darkness) && (outcomesSoFar[rounds - 4] == Outcome.LightBlocked || outcomesSoFar[rounds - 4] == Outcome.Darkness))
                {
                    rounds++;
                    actionHistory.Add(Spell.Dark);
                    return Spell.Dark;
                }
                else if ((outcomesSoFar[rounds - 1] == Outcome.Supreme || outcomesSoFar[rounds - 1] == Outcome.BlockedLight) && (outcomesSoFar[rounds - 2] == Outcome.Supreme || outcomesSoFar[rounds - 2] == Outcome.BlockedLight) && (outcomesSoFar[rounds - 3] == Outcome.Supreme || outcomesSoFar[rounds - 3] == Outcome.BlockedLight) && (outcomesSoFar[rounds - 4] == Outcome.Supreme || outcomesSoFar[rounds - 4] == Outcome.BlockedLight))
                {
                    rounds++;
                    actionHistory.Add(Spell.Light);
                    return Spell.Light;
                }
            }
            if (rounds > 1)
            {
                Outcome previousRound = outcomesSoFar[rounds - 1];
                Outcome previousRoundBeforeThat = outcomesSoFar[rounds - 2];
                if (previousRoundBeforeThat == Outcome.Supreme || previousRoundBeforeThat == Outcome.BlockedLight)
                {
                    //Console.WriteLine("hug1");
                    //Other person Hugged <3
                    if (previousRound == Outcome.Supreme || previousRound == Outcome.BlockedLight)
                    {
                        //Console.WriteLine("hug2");
                        //Other person Hugged x2 in a row <3 <3
                        //Assuming that the next outcome will now be a punch (hug, hug, punch)
                        rounds++;
                        actionHistory.Add(Spell.Dark);
                        return Spell.Dark;
                    }
                    else
                    {
                        //Console.WriteLine("punch2");
                        //Other person Punched.. 
                        //Mix, Assuming they will hug next (hug, punch, hug)
                        rounds++;
                        actionHistory.Add(Spell.Light);
                        return Spell.Light;
                    }
                }
                else
                {
                    //Console.WriteLine("punch1");
                    //Other person Punched..
                    if (previousRound == Outcome.Supreme || previousRound == Outcome.BlockedLight)
                    {
                        //Console.WriteLine("hug2");
                        //Other person Hugged <3
                        //Mix, Assuming they will punch next (punch, hug, punch)
                        rounds++;
                        actionHistory.Add(Spell.Dark);
                        return Spell.Dark;
                    }
                    else
                    {
                        //Console.WriteLine("punch2");
                        //Other person Punched.. x2 in a row 
                        //Assuming they will hug next (punch, punch, hug)
                        rounds++;
                        actionHistory.Add(Spell.Light);
                        return Spell.Light;
                    }
                }
            }
            rounds++;
            actionHistory.Add(Spell.Dark);
            return Spell.Dark;
        }
        public override void Report(Outcome Outcome)
        {
            outcomesSoFar.Add(Outcome);
        }
    }

    public class AlisFred : Character
    {
        public AlisFred() : base("Fred", CharacterTypes.Wombat) { }

        private int counter { get; set; }
        int i = 0;
        public override Spell GetNextAction()
        {
            counter += 1;
            if (counter > 300)
            {
                if (counter > 301)
                {
                    counter = 0;
                }
                // showing some love even if it makes me lose.
                return Spell.Light;

            }
            return Spell.Dark;
        }
        public override void Report(Outcome Outcome)
        {
            //Console.WriteLine("Outcome is: {0}",outcome.ToString());
        }
    }

    #endregion
    //Implementations...
    */

    /*Standard Characters
    #region Standard
    public class Chaos : Character
    {

        public Chaos() : base("Chaos", CharacterTypes.Wizard) { }

        private Random _Random = new Random();

        public override Spell GetNextAction()
        {

            return _Random.Next(0, 2) == 1 ? Spell.Dark : Spell.Light;
        }

        public override void Report(Outcome Outcome)
        {
            //who cares	
        }
    }

    public class HuggerUntilHit : Character
    {
        public HuggerUntilHit() : base("HuggerUntilHit", CharacterTypes.Wombat) { }


        private bool _WasHit = false;

        public override Spell GetNextAction()
        {
            return _WasHit ? Spell.Dark : Spell.Light;
        }

        public override void Report(Outcome Outcome)
        {
            if (Outcome != Outcome.Supreme)
            {
                _WasHit = true;
            }
        }
    }

    public class PuncherUntilHugged : Character
    {
        public PuncherUntilHugged() : base("PuncherUntilHugged", CharacterTypes.Witch) { }
        private bool _WasHugged = false;

        public override Spell GetNextAction()
        {
            return _WasHugged ? Spell.Light : Spell.Dark;
        }

        public override void Report(Outcome Outcome)
        {
            if (Outcome != Outcome.Darkness)
            {
                _WasHugged = true;
            }
        }
    }

    public class Tit4RecentTat : Character
    {

        public Tit4RecentTat() : base("Tit4RecentTat", CharacterTypes.Witch) { }

        private int _RecentCount = 0;
        private bool _WasHugged = true;

        public override Spell GetNextAction()
        {
            return _WasHugged || _RecentCount > 0 ? Spell.Light : Spell.Dark;
        }

        public override void Report(Outcome Outcome)
        {
            _WasHugged = (Outcome == Outcome.Supreme || Outcome == Outcome.BlockedLight);
            _RecentCount += _WasHugged ? 1 : -1;
        }
    }

    public class Tit4Tat : Character
    {
        public Tit4Tat() : base("Tit4Tat", CharacterTypes.Wizard) { }

        private bool _WasHugged = true;

        public override Spell GetNextAction()
        {
            return _WasHugged ? Spell.Light : Spell.Dark;
        }

        public override void Report(Outcome Outcome)
        {
            _WasHugged = (Outcome == Outcome.Supreme || Outcome == Outcome.BlockedLight);
        }
    }

    public class Tit4TatPlusKindness : Character
    {

        public Tit4TatPlusKindness() : base("Tit4TatPlusKindness", CharacterTypes.Witch) { }

        private bool _WasHugged = true;
        private Random _Random = new Random();

        public override Spell GetNextAction()
        {
            if (_WasHugged)
            {
                return Spell.Light;
            }
            return _Random.Next(0, 2) == 1 ? Spell.Light : Spell.Dark;
        }

        public override void Report(Outcome Outcome)
        {
            _WasHugged = (Outcome == Outcome.Supreme || Outcome == Outcome.BlockedLight);
        }
    }

    public class Tit4TatRedHaired : Character
    {
        public Tit4TatRedHaired() : base("Tit4TatPlusKindness", CharacterTypes.Witch) { }

        private bool _WasHugged = true;
        private Random _Random = new Random();

        public override Spell GetNextAction()
        {
            if (_WasHugged)
            {
                return _Random.Next(0, 8) == 1 ? Spell.Dark : Spell.Light;
            }
            return Spell.Dark;
        }

        public override void Report(Outcome Outcome)
        {
            _WasHugged = (Outcome == Outcome.Supreme || Outcome == Outcome.BlockedLight);
        }
    }

    public class Tit4TatPlusASmidgeOfKindness : Character
    {
        public Tit4TatPlusASmidgeOfKindness() : base("Tit4TatPlusASmidgeOfKindness", CharacterTypes.Wizard) { }

        private bool _WasHugged = true;
        private Random _Random = new Random();

        public override Spell GetNextAction()
        {
            if (_WasHugged)
            {
                return Spell.Light;
            }
            return _Random.Next(0, 8) == 1 ? Spell.Light : Spell.Dark;
        }

        public override void Report(Outcome Outcome)
        {
            _WasHugged = (Outcome == Outcome.Supreme || Outcome == Outcome.BlockedLight);
        }
    }

    public class PunchAlways : Character
    {
        public PunchAlways() : base("Tit4TatPlusASmidgeOfKindness", CharacterTypes.Wombat) { }

        public override Spell GetNextAction()
        {
            return Spell.Dark;
        }

        public override void Report(Outcome Outcome)
        {

        }
    }

    
    #endregion


    */

}
