namespace FCG.Helper
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public int StatusCode { get; set; } = 200;
        public List<string> Errors { get; set; } = new List<string>();

        private ServiceResponse(int statusCode, List<string> errors)
        {
            StatusCode = statusCode;
            Errors     = errors;
        }

        private ServiceResponse(Exception ex)
        {
            Errors = new List<string> { ex.Message.ToString() };
        }

        private ServiceResponse(int statusCode, T data)
        {
            StatusCode = statusCode;
            Data       = data;
        }

        public bool Success
        {
            get
            {
                return Errors == null || Errors.Count == 0;
            }
        }

        public bool NoSuccess
        {
            get
            {
                return Errors != null || Errors!.Count != 0;
            }
        }

        /// <summary>
        /// 200 OK
        /// </summary>
        /// <returns></returns>
        public static ServiceResponse<T> ReturnSuccess()
        {
            return new ServiceResponse<T>(200, null!);
        }

        /// <summary>
        /// 200 OK
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ServiceResponse<T> ReturnResultWith200(T data)
        {
            return new ServiceResponse<T>(200, data);
        }

        /// <summary>
        /// 201 Created
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ServiceResponse<T> ReturnResultWith201(T data)
        {
            return new ServiceResponse<T>(201, data);
        }

        /// <summary>
        /// 204 No Content
        /// </summary>
        /// <returns></returns>
        public static ServiceResponse<T> ReturnResultWith204()
        {
            return new ServiceResponse<T>(204, null!);
        }

        /// <summary>
        /// 404 Not Found
        /// </summary>
        /// <returns></returns>
        public static ServiceResponse<T> Return404()
        {
            return new ServiceResponse<T>(404, new List<string> { "Não encontrado" });
        }

        /// <summary>
        /// 404 Not Found
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ServiceResponse<T> Return404(string message)
        {
            return new ServiceResponse<T>(404, new List<string> { message });
        }

        /// <summary>
        /// 409 Conflict
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ServiceResponse<T> Return409(string message)
        {
            return new ServiceResponse<T>(409, new List<string> { message });
        }

        /// <summary>
        /// 422 Unprocessable Content
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ServiceResponse<T> Return422(string message)
        {
            return new ServiceResponse<T>(422, new List<string> { message });
        }

        /// <summary>
        /// 500 Internal Server Error
        /// </summary>
        /// <returns></returns>
        public static ServiceResponse<T> Return500()
        {
            return new ServiceResponse<T>(500, new List<string> { "Ocorreu uma falha inesperada. Tente novamente mais tarde." });
        }

        public static ServiceResponse<T> ReturnException(Exception ex)
        {
            return new ServiceResponse<T>(ex);
        }

        public static ServiceResponse<T> ReturnFailed(int statusCode, List<string> errors)
        {
            return new ServiceResponse<T>(statusCode, errors);
        }

        public static ServiceResponse<T> ReturnFailed(int statusCode, string errorMessage)
        {
            return new ServiceResponse<T>(statusCode, new List<string> { errorMessage });
        }
    }
}
