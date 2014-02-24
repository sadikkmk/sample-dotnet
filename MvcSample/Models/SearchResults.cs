using Nest;

namespace MvcSample.Models
{
    public class SearchResults
    {
        public SearchResults(IQueryResponse<Post> queryResponse, string query, string category)
        {
            QueryResponse = queryResponse;
            Query = query;
            Category = category;
        }

        public IQueryResponse<Post> QueryResponse { get; set; }
        public string Query { get; set; }
        public string Category { get; set; }
    }
}