using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.Entities
{
    public abstract class BaseEntity<T>
    {
        protected BaseEntity() { }

        public BaseEntity(T id)
        {
            if (default(T)!.Equals(id))
                throw new Exception("Invalid id");
            Id = id;
        }
        public T Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDeleted { get; set; }
        private static readonly List<object> _events = new List<object>();

        protected void Apply(object @event)
        {
            When(@event);
            _events.Add(@event);
        }

        protected static void AddEvents(object @event)
        {
            _events.Add(@event);
        }
        public IEnumerable<object> GetChanges() => _events.AsEnumerable();
        public void ClearChanges() => _events.Clear();
        protected abstract void When(object @event);
    }
}
