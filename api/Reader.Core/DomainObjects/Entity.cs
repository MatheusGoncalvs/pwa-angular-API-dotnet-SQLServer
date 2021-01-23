using System;
using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation.Results;

namespace Reader.Core.DomainObjects
{
    public abstract class Entity
    {
        public int Id { get; set; }
        [NotMapped]
        public ValidationResult ValidationResult { get; set; }

        public virtual bool EhValido()
        {
            throw new NotImplementedException();
        }
    }
}