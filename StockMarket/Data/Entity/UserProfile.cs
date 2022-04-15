using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockMarket.Data.Entity
{
    public class UserProfile
    {
        public UserProfile(string email, string profilePictureURL, string coverPictureURL, string location, string aboutMeHeader, string aboutMeDescription, string phoneNumber, string ocupation, string[] education, string[] imagesURL)
        {
            Email = email;
            ProfilePictureURL = profilePictureURL;
            CoverPictureURL = coverPictureURL;
            Location = location;
            AboutMeHeader = aboutMeHeader;
            AboutMeDescription = aboutMeDescription;
            PhoneNumber = phoneNumber;
            Ocupation = ocupation;
            Education = education;
            ImagesURL = imagesURL;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [MaxLength(40)]
        public string Email { get; set; }
        public string ProfilePictureURL { get; set; }
        public string CoverPictureURL { get; set; }
        public string Location { get; set; }
        public string AboutMeHeader { get; set; }
        public string AboutMeDescription { get; set; }
        public string PhoneNumber { get; set; }
        public string Ocupation { get; set; }
        [Column(TypeName = "text[]")]
        public string[] Education { get; set; }
        [Column(TypeName = "text[]")]
        public string[] ImagesURL { get; set; }
    }
}
