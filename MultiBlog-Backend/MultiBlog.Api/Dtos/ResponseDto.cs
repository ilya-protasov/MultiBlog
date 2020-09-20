namespace MultiBlog.Api.Dtos
{
    public class ResponseDto<T>
    {
        public int HttpStatus { get; set; }
        public string ErrorInfo { get; set; }
        public T Result { get; set; }
    }
}
