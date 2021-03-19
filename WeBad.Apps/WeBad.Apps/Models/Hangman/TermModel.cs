using System;
using System.Collections.Generic;
using System.Text;

namespace WeBad.Apps.Models.Hangman
{
    public class TermModel
    {
        public TermCategoryModel Category { get; set; }
        public string Value { get; set; }

        public long ID { get; set; }
    }

}
