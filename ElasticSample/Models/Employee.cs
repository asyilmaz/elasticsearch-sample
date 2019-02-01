using Nest;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSample.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        [NotMapped]
        public virtual CompletionField Suggest { get; set; }
    }

    public class EmployeeSuggest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string SuggestedName { get; set; }
    }

    public class EmployeeSuggestResponse
    {
        public IEnumerable<EmployeeSuggest> Suggests { get; set; }
    }
}
