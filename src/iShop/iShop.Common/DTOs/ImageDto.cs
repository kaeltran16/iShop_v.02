using System;
using System.ComponentModel.DataAnnotations;

namespace iShop.Common.DTOs
{
    public class ImageDto
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
    }
}
