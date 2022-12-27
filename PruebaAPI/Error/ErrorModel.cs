
namespace PruebaAPI.Error;

public class ErrorModel
{
    public string ErrorCode { get; set; }
    public string Message { get; set; }
    public Dictionary<string,string> AdditionalInf{ get; set;} 

}