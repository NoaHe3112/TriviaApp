using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TriviaApp.Models
{
    public class Answer
    {
        public string text { get; set; }
        public Color color{ get; set; }

    }
    public class AmericanQuestion
    {
        public string QText { get; set; }
        public string CorrectAnswer { get; set; }
        public string[] OtherAnswers { get; set; }
        public string CreatorNickName { get; set; }
    }
}
