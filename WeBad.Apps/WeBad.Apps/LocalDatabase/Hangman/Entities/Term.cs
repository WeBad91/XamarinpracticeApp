using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace WeBad.Apps.LocalDatabase.Hangman.Entities
{
    public class Term : RealmObject
    {        
        [PrimaryKey]
        public long Id { get; set; }

        public string Value { get; set; }

        public int NoOfOccurrence { get; set; }

        public TermCategory Category { get; set; }
    }
}
