namespace Bubble.io.Entities.DTOs
{
    public class DTOProfileBasicInfo
    {
        public string firstname {  get; set; }
        public string lastname { get; set; }
        public string bio {  get; set; }
        public IFormFile imageData { get; set; }
    }
}
