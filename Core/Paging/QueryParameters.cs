namespace Core.Entities
{
    public class QueryParameters
    {
        private const int MaxPageCount = 50;
        public int Page { get; set; } = 1;
        private int _pageCount = MaxPageCount;
        public int PageCount
        {
            get { return _pageCount; }
            set { _pageCount = (value > MaxPageCount) ? MaxPageCount : value; }
        }
        private string _query;
        public string Query
        {
            get => _query;
            set => _query = value.ToLower();
        }
    }
}