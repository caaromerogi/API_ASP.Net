namespace PruebaAPI.Error;

public class ErrorModelBuilder
{
    private string? _errorCode;
    private string? _message;

    public ErrorModelBuilder WithErrorCode(string errorCode){
        _errorCode = errorCode;
        return this;
    }

    public ErrorModelBuilder WithMessage(string message){
        _message = message;
        return this;
    }

    public ErrorModel Build(){
        return new ErrorModel{
            ErrorCode = _errorCode,
            Message = _message
        };
    }
}