﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Bubble.io.Entities.DTOs
{
    public class DTOProfileData
    {
        public string id { get; set; }
        public string firstname { get; set; } = string.Empty;
        public string lastname { get; set; } = string.Empty;
        public string bio { get; set; } = string.Empty;
        public string email {  get; set; } = string.Empty;
        public string imageUrl {  get; set; } = string.Empty;
        public string imageData { get; set; }
    }
}
