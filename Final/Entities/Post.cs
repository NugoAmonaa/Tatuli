using Final.Enum;

using Microsoft.Extensions.Configuration.UserSecrets;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Final.Entities
{
    public class Post
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set;}
        public string Name { get; set;}
        public string Content { get; set;}
        public EState State { get; set;}
        public EStatus Status { get; set;}
        public int CreatorId { get; set; }
        public User Creator { get; set; }
        public DateTime CreateDate { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
