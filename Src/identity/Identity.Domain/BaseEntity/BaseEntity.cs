using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.Domain.BaseEntity
{
    public abstract class BaseEntity
    {
        private Guid _id = Guid.NewGuid();

        public Guid Id
        {
            get => _id;
            set => _id = value == Guid.Empty ? Guid.NewGuid() : value;
        }

        [Column(TypeName = "datetime2(6)")]
        public DateTime CreateDate { get; private set; } = DateTime.Now;

        [Column(TypeName = "datetime2(6)")]
        public DateTime? ModifyDate { get; private set; }

        public void SetModifyDate()
        {
            ModifyDate = DateTime.Now;
        }
    }
}