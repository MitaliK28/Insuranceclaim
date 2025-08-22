using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace insuranceclaimproject.Dtos
{
    public class SubmitClaimDto
    {
        [Required]
        public int PolicyId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Claim amount must be greater than zero.")]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal ClaimAmount { get; set; }
    }
}