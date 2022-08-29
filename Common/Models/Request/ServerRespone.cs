using System.Net;

namespace CommonClass.Models.Request
{
    /// <summary>
    /// Mẫu kết quả trả về từ phía API
    /// </summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 29/08/2022 created
    /// </Modified>
    public class ServerRespone
    {
        public bool IsSuccess { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
    }
}
