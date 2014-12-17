using System;
using CodeUtopia.Domain;

namespace Application.Backend.Domain.Application
{
    public abstract class Applicant : Entity
    {
        protected Applicant(Guid applicationId, IVersionNumberProvider versionNumberProvider, Guid entityId)
            : base(applicationId, versionNumberProvider, entityId)
        {
        }
    }
}