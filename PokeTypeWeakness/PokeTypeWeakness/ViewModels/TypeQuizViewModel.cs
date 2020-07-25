using PokeTypeWeakness.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace PokeTypeWeakness.ViewModels
{
    public class TypeQuizViewModel: BaseViewModel
    {
        public PokeType QuizSubjectType { get; set; }
        public ObservableCollection<ElectablePokeType> PokeTypes { get; set; }

        public int NumWeaknesses { get { return QuizSubjectType.Weaknesses.Count; } }
        public int NumElections { get { return PokeTypes.Where(x => x.Elected).Count(); } }

        public string NumberOfWeaknessesText { get { return string.Format("Find {0} {1} Weaknesses", NumWeaknesses, QuizSubjectType.DisplayName); } }

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

        async void ElectQuizSubject()
        {
            int subjectIndex = random.Next(0, PokeTypes.Count);
            QuizSubjectType = PokeTypes[subjectIndex];
            await QuizSubjectType.LoadWeaknesses();

            OnPropertyChanged(nameof(QuizSubjectType));
            OnPropertyChanged(nameof(NumberOfWeaknessesText));
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
                ElectQuizSubject();
                ClearElections();
                return true;
            }

            ClearElections(true);
            return false;
        }

        bool AreElectionsCorrect()
        {
            IEnumerable<string> elections = PokeTypes.Where(x => x.Elected).Select(x => x.NaturalID);

            if (elections.Count() != QuizSubjectType.WeaknessNaturalIDs.Count())
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
            return QuizSubjectType.WeaknessNaturalIDs.Contains(naturalID);
        }
    }
}
