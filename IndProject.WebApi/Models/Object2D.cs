﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace IndProject.WebApi.Models
{
    public class Object2D
    {
        public Guid Id { get; set; }
        [Required]
        [JsonPropertyName("EnviromentId")]
        public Guid EnviromentId { get; set; }
        [Required]
        [JsonPropertyName("PrefabId")]
        public int PrefabId { get; set; }
        [Required]
        [JsonPropertyName("PositionX")]
        public float PositionX { get; set; }
        [Required]
        [JsonPropertyName("PositionY")]
        public float PositionY { get; set; }
        [Required]
        [JsonPropertyName("ScaleX")]
        public float ScaleX { get; set; }
        [Required]
        [JsonPropertyName("ScaleY")]
        public float ScaleY { get; set; }
        [Required]
        [JsonPropertyName("RotationZ")]
        public float RotationZ { get; set; }
        [Required]
        [JsonPropertyName("SortingLayer")]
        public string SortingLayer { get; set; }
    }

}
