namespace WebApp.Models
{
    public class ApiResult
    {
        public ApiResult(string href, object data)
        {
            Href = href;
            Data = data;
        }
    
        public string Href { get; set; }
        public object Data { get; set; }
    }
}
