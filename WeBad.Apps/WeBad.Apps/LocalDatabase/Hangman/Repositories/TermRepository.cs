using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeBad.Apps.LocalDatabase.Hangman.Entities;
using WeBad.Apps.Models.Hangman;

namespace WeBad.Apps.LocalDatabase.Hangman.Repositories
{
    public class TermRepository
    {
        private IRealmRepository realmRepository;

        public TermRepository()
        {
            realmRepository = new RealmRepository();
        }

        public bool AddTerm(TermModel termModel)
        {
            bool retVal = false;

            try
            {
                TermCategory category = realmRepository.GetAll<TermCategory>().FirstOrDefault(x => x.Name.Equals(termModel.Category.Name));
                if (category == null)
                {
                    category = new TermCategory
                    {
                        Id = DateTime.Now.Ticks,
                        Name = termModel.Category.Name
                    };
                }

                Term term = new Term
                {
                    Id = DateTime.Now.Ticks,
                    Category = category,
                    NoOfOccurrence = 0,
                    Value = termModel.Value
                };

                realmRepository.Add(term);

                retVal = true;
            }
            catch(Exception ex)
            {
                retVal = false;
            }

            return retVal;
        }

        public IEnumerable<TermCategoryModel> GetAllCategories()
        {
            if (!realmRepository.GetAll<TermCategory>().Any())
            {
                InitData();
            }

            return realmRepository.GetAll<TermCategory>().ToList().Select(x => new TermCategoryModel
            {
                Id = x.Id,
                Name = x.Name
            });
        }

        public TermModel GetTermByCategory(long id)
        {            
            var category = realmRepository.Get<TermCategory>(id);

            var categoryModel = new TermCategoryModel
            {
                Id = category.Id,
                Name = category.Name
            };

            List<Term> terms = category.Terms.ToList()
                                             .GroupBy(x => x.NoOfOccurrence)
                                             .OrderBy(x => x.Key)
                                             .FirstOrDefault()                                             
                                             .ToList();
            Random random = new Random();
            int index = random.Next(0, terms.Count());
            Term selectedTerm = terms.ElementAt(index);

            realmRepository.Update(() =>
            {
                selectedTerm.NoOfOccurrence++;
            });

            return new TermModel
            {
                Category = categoryModel,
                ID = selectedTerm.Id,
                Value = selectedTerm.Value
            };
        }

        private void InitData()
        {
            
            TermCategoryModel categoryMovies = new TermCategoryModel { Name = "Filmovi" };
            TermCategoryModel categorySports = new TermCategoryModel { Name = "Sportovi" };
            TermCategoryModel categoryCities = new TermCategoryModel { Name = "Gradovi" };
            TermCategoryModel categoryCountries = new TermCategoryModel { Name = "Drzave" };

            TermModel moviesTerm1 = new TermModel { Category = categoryMovies, Value = "Ko to tamo peva" };
            TermModel moviesTerm2 = new TermModel { Category = categoryMovies, Value = "Balkanski špijun" };
            TermModel moviesTerm3 = new TermModel { Category = categoryMovies, Value = "Kad porastem biću kengur" };
            TermModel moviesTerm4 = new TermModel { Category = categoryMovies, Value = "Tesna koža" };
            TermModel moviesTerm5 = new TermModel { Category = categoryMovies, Value = "Munje" };


            TermModel sportsTerm1 = new TermModel { Category = categorySports, Value = "Fudbal" };
            TermModel sportsTerm2 = new TermModel { Category = categorySports, Value = "Vaterpolo" };
            TermModel sportsTerm3 = new TermModel { Category = categorySports, Value = "Karling" };
            TermModel sportsTerm4 = new TermModel { Category = categorySports, Value = "Boćanje" };
            TermModel sportsTerm5 = new TermModel { Category = categorySports, Value = "Stoni tenis" };

            TermModel citiesTerm1 = new TermModel { Category = categoryCities, Value = "Rio de žaneiro" };
            TermModel citiesTerm2 = new TermModel { Category = categoryCities, Value = "Madrid" };
            TermModel citiesTerm3 = new TermModel { Category = categoryCities, Value = "Venecija" };
            TermModel citiesTerm4 = new TermModel { Category = categoryCities, Value = "Smederevo" };
            TermModel citiesTerm5 = new TermModel { Category = categoryCities, Value = "Moskva" };

            TermModel countriesTerm1 = new TermModel { Category = categoryCountries, Value = "Srbija" };
            TermModel countriesTerm2 = new TermModel { Category = categoryCountries, Value = "Brazil" };
            TermModel countriesTerm3 = new TermModel { Category = categoryCountries, Value = "Češka" };
            TermModel countriesTerm4 = new TermModel { Category = categoryCountries, Value = "Meksiko" };
            TermModel countriesTerm5 = new TermModel { Category = categoryCountries, Value = "Nigerija" };


            AddTerm(moviesTerm1);
            AddTerm(moviesTerm2);
            AddTerm(moviesTerm3);
            AddTerm(moviesTerm4);
            AddTerm(moviesTerm5);

            AddTerm(sportsTerm1);
            AddTerm(sportsTerm2);
            AddTerm(sportsTerm3);
            AddTerm(sportsTerm4);
            AddTerm(sportsTerm5);

            AddTerm(citiesTerm1);
            AddTerm(citiesTerm2);
            AddTerm(citiesTerm3);
            AddTerm(citiesTerm4);
            AddTerm(citiesTerm5);

            AddTerm(countriesTerm1);
            AddTerm(countriesTerm2);
            AddTerm(countriesTerm3);
            AddTerm(countriesTerm4);
            AddTerm(countriesTerm5);


        }
    }
}
