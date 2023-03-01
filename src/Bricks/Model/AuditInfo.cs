using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Bricks.Model
{
    [Owned]
    public class AuditInfo : ValueObject
    {
        public DateTime Created { get; set; }

        [MaxLength(64)]
        public string CreatedBy { get; set; }

        public DateTime Modified { get; set; }

        [MaxLength(64)]
        public string ModifiedBy { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Created;
            yield return CreatedBy;
            yield return Modified;
            yield return ModifiedBy;
        }
    }
}
