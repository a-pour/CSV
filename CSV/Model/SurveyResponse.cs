using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSV.Model
{
    public class SurveyResponse
    {
        public string Email  { get; set; }
        public int EmployeeId  { get; set; }
        public string TimeStamp  { get; set; }
        public IList<string> Answers { get; set; }
    }
}
