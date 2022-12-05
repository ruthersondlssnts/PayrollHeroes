namespace TPS.Frontend.Infrastructure
{
    public class ApiResponse<T>
    {
        public StatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
    }
    public enum StatusCode
    {
        Success = 200,
        Created = 201,
        Bad_Request = 400,
        Conflict = 409,
        //Request Validation status
        DUPLICATE = 7000,
    }
}
