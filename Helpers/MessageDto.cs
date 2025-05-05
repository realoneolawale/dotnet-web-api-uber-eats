namespace Ubereats.Helpers
{
    public class MessageDto
    {
        public bool Ok { get; set; }
        public string Message { get; set; } = string.Empty;
        public dynamic? Record { get; set; }

        public MessageDto() { }

        public MessageDto(bool ok, string message, dynamic? record = null)
        {
            Ok = ok;
            Message = message;
            Record = record;
        }
        public MessageDto(int result, string successMessage, string errorMessage)
        {
            Ok = result > 0;
            Message = Ok ? successMessage : errorMessage;
            Record = string.Empty;
        }

        public MessageDto(bool ok, string successMessage, string errorMessage, dynamic? record = null)
        {
            Ok = ok;
            Message = ok ? successMessage : errorMessage;
            Record = record;
        }

    }
}