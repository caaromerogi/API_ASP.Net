namespace PruebaAPI.Exceptions;

public class InvalidElementException<T> : Exception
{
   
    public T Data { get; set; }

     public InvalidElementException(string message, T data) : base(message)
    {
        Data = data;
    }
    
}
