﻿namespace Final.Dto
{
    public class UpdatePostDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public int CreatorId { get; set; }
    }
}