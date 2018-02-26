﻿using System;
using System.ComponentModel.DataAnnotations;
using iShop.Common.DataAnnotations;

namespace iShop.Common.DTOs
{
    public class OrderedItemDto
    {
        [GuidFormat(ErrorMessage = "The Product Id is missing or not in format.")]
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive value and greater than 1.")]
        public int Quantity { get; set; }
    }
}