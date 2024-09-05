using System.Net;
namespace Library.Application.DTO;
public class ApiResponse
{
    public object Result { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public bool IsSuccess { get; set; }
    public List<string> Errors { get; set; }
    public ApiResponse()
    {
        Errors = new List<string>();
    }
}
