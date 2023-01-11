using FluentValidation.Results;

namespace MyDelivery.Application.Services;

public class ResultService
{
    public bool Sucess { get; set; }
    public string Message { get; set; }
    public ICollection<ErrorValidation> Errors { get; set; }

    public static ResultService RequestError(string message, ValidationResult validationResult)
    {
        return new ResultService
        {
            Sucess = false,
            Message = message,
            Errors = validationResult.Errors.Select(x => new ErrorValidation { Property = x.PropertyName, Message = x.ErrorMessage }).ToList()
        };
    }

    public static ResultService<T> RequestError<T>(string message, ValidationResult validationResult)
    {
        return new ResultService<T>
        {
            Sucess = false,
            Message = message,
            Errors = validationResult.Errors.Select(x => new ErrorValidation { Property = x.PropertyName, Message = x.ErrorMessage }).ToList()
        };
    }

    public static ResultService Fail(string message) => new ResultService { Sucess = false, Message = message };
    public static ResultService<T> Fail<T>(string message) => new ResultService<T> { Sucess = false, Message = message };

    public static ResultService Ok(string message) => new ResultService { Sucess= true, Message = message };
    public static ResultService<T> Ok<T>(T data) => new ResultService<T> { Sucess= true, Data = data };

}

public class ResultService<T> : ResultService
{
    public T Data { get; set; }
}
