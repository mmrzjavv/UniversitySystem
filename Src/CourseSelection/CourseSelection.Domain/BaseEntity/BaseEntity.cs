using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSelection.Domain.BaseEntity
{
    public class BaseEntity
    {
        private Guid _id;

        public Guid Id
        {
            get => _id;
            set => _id = value == Guid.Empty ? Guid.NewGuid() : value;
        }

        [Column(TypeName = "datetime2(6)")]
        public DateTime CreateDate { get; set; } = DateTime.Now;

        [Column(TypeName = "datetime2(6)")]
        public DateTime? ModifyDate { get; set; }

        public BaseEntity()
        {
            _id = Guid.NewGuid();
        }
    }
}
