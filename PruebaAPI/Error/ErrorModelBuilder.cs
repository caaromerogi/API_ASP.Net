namespace PruebaAPI.Error;

public class ErrorModelBuilder
{
    private string? _errorCode;
    private string? _message;
    private Dictionary<string,string> _additionalInfo = new Dictionary<string, string>(){
        {"Info", "There is no additional info to show"}
    };

    public ErrorModelBuilder WithErrorCode(string errorCode){
        _errorCode = errorCode;
        return this;
    }

    public ErrorModelBuilder WithMessage(string message){
        _message = message;
        return this;
    }

    public ErrorModelBuilder WithAdditionalInf(Dictionary<string,string> additionalInf)
    {
        _additionalInfo = additionalInf;
        return this;
    }

    public ErrorModel Build(){
        return new ErrorModel{
            ErrorCode = _errorCode,
            Message = _message,
            AdditionalInf = _additionalInfo
        };
    }
}