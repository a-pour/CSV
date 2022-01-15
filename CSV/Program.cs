using CSV.helpers;
using CSV.Model;
using System;

namespace CSV
{
    class Program
    {
        static void Main(string[] args)
        {
            var helper = new CSVHelper();
            var survey=helper.Read<Survey>("survey.csv");
            var response = helper.Read<SurveyResponse>("survey-res.csv");
        }
    }
}
