using CSV.helpers;
using CSV.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSV
{
    public class CalcSurvey
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

            //read survey data to an object array
            var survey = helper.Read<Survey>(surveyPath);

            //read survey-responses data to an object array
            var response = helper.Read<SurveyResponse>(surveyResponsePath);

            //total count of responses
            var count = response.Count();
            
            // coun of responses with submit_date
            var answerdCount = System.Convert.ToDouble(response.Where(x => (x.TimeStamp ?? string.Empty) != string.Empty).Count());

            Console.WriteLine($"Total:{count.ToString()}");

            var percentage = count == 0 //avoid devide by zero
                             ? count : 
                             (float)answerdCount / count * 100;

            Console.WriteLine($"Answerd People Percentage:{percentage}%");
            Console.WriteLine("***********************");
            Console.WriteLine("Average For Rating Qs:");
            Console.WriteLine("-----------------------");

            int a;// temp variable to test if data is not number
            var data = response.Select
                (x => x.Answers.Sum(y =>
                    !int.TryParse(y, out a) // check if data is int
                    || (x.TimeStamp ?? string.Empty) == string.Empty // TimeStamp found
                        ? 0// if data is not number or does not have time_stmp return 0
                        : System.Convert.ToDouble(y))).ToList();

            //print averages 
            int i = 0;
            survey.ForEach(x =>
            {
                if (x.Type == "ratingquestion")
                    Console.WriteLine($"{x.Text}:{data[i] / answerdCount}");
                i++;
            });
        }
    }
}
