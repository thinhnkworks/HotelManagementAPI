namespace HotelManagementAPI.DTO.Result
{
    public class Result
    {
        public bool Success { get; set; }
        public List<string>? Errors { get; set; }
        public Object? Data { get; set; }
    }
}
