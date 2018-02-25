using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace blog.Models {
    public class Post {
        private string _tags = "[]";
        [Key]
        public Guid Id { get; set; }
        public string Content { get; set; } = "";
        [NotMapped]
        public string[] Tags {
            get {
                return JsonConvert.DeserializeObject<string[]>(_tags ?? "[]");
            }
            set {
                _tags = JsonConvert.SerializeObject(value == null ? new string[0] : value);
            }
        }
        public string Title { get; set; } = "";
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}