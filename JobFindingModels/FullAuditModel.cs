using JobFindingModels.Interfaces;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace JobFindingModels
{
    public class FullAuditModel : IIdentityModel, IActivableModel, ISoftDeletable
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }
        [Required]
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }
}
