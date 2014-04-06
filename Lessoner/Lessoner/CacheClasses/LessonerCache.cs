using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lessoner
{
    public class LessonerCache
    {
        public struct Selecter
        {
            public int ID;
            public string Name;
            public int Week;
        }
        public int WeekIndex = 0;
        public List<DateTime> WeekBegins = new List<DateTime>();
        public Selecter SelectedTimeTable;
        public List<Lesson> Lessons = new List<Lesson>();
        public bool TeacherLessons = false;
    }
}