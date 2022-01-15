using CSV.Model;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSV.helpers
{
    class CSVHelper
    {
        public List<T> Read<T>(string Path)
        {

            using (var reader = new StreamReader(Path))
            using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                switch (typeof(T).Name)// TODO: we can do better using injection or generic type mapping in the future
                {
                    case "Survey":
                        csvReader.Context.RegisterClassMap<SurveyMap>();
                        break;
                    case "SurveyResponse":
                        csvReader.Context.RegisterClassMap<SurveyResponseMap>();
                        break;
                }

                var data = csvReader.GetRecords<T>();
                return data.ToList();
            }
        }
    }
}


