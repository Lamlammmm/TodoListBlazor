namespace TodoList.Models
{
    public class BaseApiResult<T> : PageRequest
    {
        public List<T> Data { get; set; }
        public int HttpStatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public int TotalPage { get; set; }
        public int TotalItem { get; set; }
    }
}