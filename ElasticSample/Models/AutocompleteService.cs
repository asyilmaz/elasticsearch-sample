using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSample.Models
{
    public class AutocompleteService : IAutocompleteService
    {
        readonly ElasticClient _elasticClient;

        public AutocompleteService(ConnectionSettings connectionSettings)
        {
            _elasticClient = new ElasticClient(connectionSettings);
        }
        public async Task<bool> CreateIndexAsync(string indexName)
        {
            CreateIndexDescriptor indexDescriptor = new CreateIndexDescriptor(indexName)
                    .Settings(s => s.NumberOfReplicas(0).NumberOfShards(1))
                                .Mappings(mappings => mappings
                                    .Map<Employee>(m => m.AutoMap().Properties(p => p.Completion(c => c.Name(su => su.Name)))));

            if (_elasticClient.IndexExists(indexName.ToLowerInvariant()).Exists)
            {
                _elasticClient.DeleteIndex(indexName.ToLowerInvariant());
            }

            ICreateIndexResponse createIndexResponse = await _elasticClient.CreateIndexAsync(indexDescriptor);

            return createIndexResponse.IsValid;


        }

        public async Task IndexAsync(string indexName, List<Employee> employees)
        {
            await _elasticClient.IndexManyAsync(employees, indexName);
        }

        public async Task<EmployeeSuggestResponse> SuggestAsync(string indexName, string keyword)
        {
            ISearchResponse<Employee> searchResponse = await _elasticClient.SearchAsync<Employee>(s => s.Index(indexName).Suggest(su => su.Completion("suggestions", c => c
            .Field(f => f.Suggest).Prefix(keyword).Fuzzy(f => f.Fuzziness(Fuzziness.Auto)).Size(5))));

            var suggests = from suggest in searchResponse.Suggest["suggestions"]
                           from option in suggest.Options
                           select new EmployeeSuggest
                           {
                               Id = option.Source.Id,
                               Name = option.Source.Name,
                               LastName = option.Source.LastName,
                               SuggestedName = option.Text
                           };

            return new EmployeeSuggestResponse
            {
                Suggests = suggests
            };
        }
    }
}
