using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeBad.Apps.LocalDatabase.Hangman.Entities
{
    public class TermCategory : RealmObject
    {
        [PrimaryKey]
        public long Id { get; set; }

        public string Name { get; set; }

        [Backlink(nameof(Term.Category))]
        public IQueryable<Term> Terms { get; }
    }
}
