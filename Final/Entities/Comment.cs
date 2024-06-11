﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Final.Entities
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PostID { get; set; }
        public string Content { get; set; }
        public Post Post { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}