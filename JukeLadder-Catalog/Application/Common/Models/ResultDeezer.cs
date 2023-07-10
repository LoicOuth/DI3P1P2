namespace Application.Common.Models;

public class ResultDeezer<T>
{
    public T Data { get; set; }

    public ResultDeezer(T data)
    {
        Data = data;
    }
}
