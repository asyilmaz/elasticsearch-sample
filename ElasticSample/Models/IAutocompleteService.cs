using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSample.Models
{
    interface IAutocompleteService
    {
        Task<bool> CreateIndexAsync(string indexName);
        Task IndexAsync(string indexName, List<Employee> products);
        Task<EmployeeSuggestResponse> SuggestAsync(string indexName, string keyword);
    }
}
