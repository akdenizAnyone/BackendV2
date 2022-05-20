namespace Application.Features.Follows.Dtos{
   public class SuggestionUserDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string PictureId { get; set; }
        public bool IsCertified { get; set; }
    } 
}