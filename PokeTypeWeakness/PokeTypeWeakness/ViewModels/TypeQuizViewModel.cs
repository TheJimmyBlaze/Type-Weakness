using PokeTypeWeakness.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PokeTypeWeakness.ViewModels
{
    public class TypeQuizViewModel: BaseViewModel
    {
        public PokeType QuizSubjectType { get; set; }
        public ObservableCollection<ElectablePokeType> PokeTypes { get; set; }

        private bool isWeaknessQuiz = true;
        public bool IsWeaknessQuiz
        {
            get { return isWeaknessQuiz; }
            set
            {
                isWeaknessQuiz = value;
                OnPropertyChanged(nameof(IsWeaknessQuiz));
                OnPropertyChanged(nameof(NumberOfElectablesText));
                OnPropertyChanged(nameof(CongratulationsMessage));
            }
        }

        private bool showCongratulations = false;
        public bool ShowCongratulations
        {
            get { return showCongratulations; }
            set
            {
                showCongratulations = value;
                OnPropertyChanged(nameof(ShowCongratulations));
            }
        }

        private ObservableCollection<PokeType> correctPokeTypes;
        public ObservableCollection<PokeType> CorrectPokeTypes
        {
            get
            {
                if (correctPokeTypes == null)
                    correctPokeTypes = new ObservableCollection<PokeType>();
                return correctPokeTypes;
            }
            set
            {
                correctPokeTypes = value;
                OnPropertyChanged(nameof(CorrectPokeTypes));
            }
        }

        public int NumCorrectTypes { get { return correctPokeTypes.Count; } }
        public int NumElections { get { return PokeTypes.Where(x => x.Elected).Count(); } }

        public string NumberOfElectablesText
        {
            get
            {
                return string.Format("Find {0} {1} {2}",
                                    NumCorrectTypes, 
                                    QuizSubjectType.DisplayName, 
                                    isWeaknessQuiz ? "Weaknesses" : "Strengths"); 
            } 
        }

        public string CongratulationsMessage
        {
            get { return string.Format("{0} types are {1}", QuizSubjectType.DisplayName, isWeaknessQuiz ? "weak against" : "strong against"); }
        }

        public TypeQuizViewModel(IEnumerable<PokeType> pokeTypes)
        {
            Title = "Trainer Challenge";

            PokeTypes = new ObservableCollection<ElectablePokeType>();

            foreach(PokeType pokeType in pokeTypes)
            {
                PokeTypes.Add(new ElectablePokeType(pokeType));
            }
            ElectQuizSubject();
        }

        public async void ToggleIsWeaknessQuiz()
        {
            IsWeaknessQuiz = !IsWeaknessQuiz;
            await LoadStrengthsOrWeaknesses();
        }

        async void ElectQuizSubject()
        {
            int subjectIndex = random.Next(0, PokeTypes.Count);
            QuizSubjectType = PokeTypes[subjectIndex];

            await LoadStrengthsOrWeaknesses();

            OnPropertyChanged(nameof(QuizSubjectType));
        }

        async Task LoadStrengthsOrWeaknesses()
        {
            if (isWeaknessQuiz)
            {
                await QuizSubjectType.LoadWeaknesses();
                CorrectPokeTypes = QuizSubjectType.Weaknesses;
            }
            else
            {
                await QuizSubjectType.LoadStrengths();
                CorrectPokeTypes = QuizSubjectType.Strengths;
            }

            OnPropertyChanged(nameof(NumberOfElectablesText));
            OnPropertyChanged(nameof(CongratulationsMessage));
        }

        public void ClearElections(bool keepCorrectElections = false)
        {
            foreach(ElectablePokeType pokeType in PokeTypes)
            {
                if (pokeType.Elected)
                {
                    if (IsElectionCorrect(pokeType.NaturalID) && keepCorrectElections)
                        continue;
                    pokeType.Elected = false;
                }
            }
        }

        public bool SubmitElections()
        {
            if (AreElectionsCorrect())
            {
                ShowCongratulations = true;
                return true;
            }

            ClearElections(true);
            return false;
        }

        bool AreElectionsCorrect()
        {
            IEnumerable<string> elections = PokeTypes.Where(x => x.Elected).Select(x => x.NaturalID);

            if (elections.Count() != correctPokeTypes.Count())
                return false;

            foreach (string election in elections)
            {
                if (!IsElectionCorrect(election))
                    return false;
            }

            return true;
        }

        bool IsElectionCorrect(string naturalID)
        {
            return correctPokeTypes.Select(x => x.NaturalID).Contains(naturalID);
        }

        public void DismissCongratulations()
        {
            ShowCongratulations = false;

            ElectQuizSubject();
            ClearElections();
        }
    }
}
