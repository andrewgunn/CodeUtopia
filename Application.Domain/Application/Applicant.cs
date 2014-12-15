using System;
using CodeUtopia.Domain;

namespace Application.Domain.Application
{
    public abstract class Applicant : Entity
    {
        protected Applicant(Guid applicationId, IVersionNumberProvider versionNumberProvider, Guid entityId)
            : base(applicationId, versionNumberProvider, entityId)
        {
        }
    }
}