using CSV.helpers;
using CSV.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSV
{
    class CalcSurvey
    {
        public void Run()
        {
            var helper = new CSVHelper();
            Console.WriteLine("Please enter Survey file path:");
            var surveyPath = Console.ReadLine();

            Console.WriteLine("Please enter Survey Response file path:");
            var surveyResponsePath = Console.ReadLine();

            if (!System.IO.File.Exists(surveyPath) || !System.IO.File.Exists(surveyResponsePath))
            {
                Console.WriteLine("one or both pathes are not reachable exit and run application again please");
                return;

            }

            var survey = helper.Read<Survey>(surveyPath);
            var response = helper.Read<SurveyResponse>(surveyResponsePath);

            var count = System.Convert.ToDouble(response.Where(x => (x.TimeStamp ?? string.Empty) != string.Empty).Count());
            Console.WriteLine($"Total:{count.ToString()}");

            var percentage = response.Where(x => (x.TimeStamp ?? string.Empty) != string.Empty).Count() / count;
            Console.WriteLine($"Percentage:{percentage}");
            Console.WriteLine("***********************");
            Console.WriteLine("Average For Rating Qs:");
            Console.WriteLine("-----------------------");

            int a;
            var data = response.Select
                (x => x.Answers.Sum(y =>
                    !int.TryParse(y, out a) // data is int
                    || (x.TimeStamp ?? string.Empty) == string.Empty // TimeStamp found
                        ? 0
                        : System.Convert.ToInt32(y))).ToList();

            int i = 0;
            survey.ForEach(x =>
            {
                if (x.Type == "ratingquestion")
                    Console.WriteLine($"{x.Text}:{data[i] / count}");
                i++;
            });
        }
    }
}
