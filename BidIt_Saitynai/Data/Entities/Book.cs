﻿using BidIt_Saitynai.Auth.Model;
using FluentValidation;
using System.ComponentModel.DataAnnotations;
using static BidIt_Saitynai.Data.Entities.Book;
using static BidIt_Saitynai.Data.Entities.User;

namespace BidIt_Saitynai.Data.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Condition { get; set; }
        public int PageCount { get; set; }
        public double StartingPrice { get; set; }
        public User User { get; set; }

        [Required]
        public required string UserId { get; set; }
        public ForumRestUser AuthUser { get; set; }



    }
}
