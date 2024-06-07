namespace QuizApp.Core;

public class ResponseModel<T>
{
    public string? Message { get; set; }
    public T? Data { get; set; }

    public bool Succeeded { get; set; }

    public bool IsLastPage { get; set; }

    public ResponseModel()
    {
        Succeeded = true;
    }

    public ResponseModel(System.Exception exception)
    {
        var innerEx = exception;
        while (innerEx.InnerException != null)
        {
            innerEx = innerEx.InnerException;
        }

        Message = innerEx.Message;
        Succeeded = false;
    }

    public ResponseModel<T> SetMessage(string message)
    {
        Message = message;
        return this;
    }
}