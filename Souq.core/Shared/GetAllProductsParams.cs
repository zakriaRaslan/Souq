namespace Souq.core.Shared
{
    public class GetAllProductsParams
    {
        public string Sort {  get; set; }
        public int? CategoryId { get; set; }
        private int MaxPageSize { get; set; } = 6;
        private int _pageSize = 3;
       
        public int PageSize
        {
            get { return _pageSize; }
            set { if (value <= 0)
                {
                    _pageSize = 3;
                }else if(value > MaxPageSize)
                {
                     _pageSize = MaxPageSize;
                }
            }
        }
        private int _pageNumber;
        public int PageNumber 
        {
            get => _pageNumber;
            set { _pageNumber = value <=0 ? 1 :value; }
        }

        public string? SearchingWord { get; set; }
    }
}
