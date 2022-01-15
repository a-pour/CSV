
using CSV.Model;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Thease classes help CSVReader to map data to model correctly
/// </summary>
namespace CSV.helpers
{
    public sealed class SurveyMap : ClassMap<Survey>
    {
        public SurveyMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.Text).Name("text");
            Map(m => m.Type).Name("type");
            Map(m => m.Theme).Name("theme");
        }
    }

    public sealed class SurveyResponseMap : ClassMap<SurveyResponse>
    {
        public SurveyResponseMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.Email).Index(0);
            Map(m => m.EmployeeId).Index(1);

            Map(m => m.TimeStamp).Index(2);
            Map(m => m.Answers).Index(3);// bkz type of Answers is Ilist, so CsvHelper reads all data from Index 3 to end as a string array 
        }
    }
}
