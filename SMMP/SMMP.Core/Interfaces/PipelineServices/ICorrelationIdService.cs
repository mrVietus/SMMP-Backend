using System;

namespace SMMP.Core.Interfaces.PipelineServices
{
    public interface ICorrelationIdService
    {
        string GetCorrelationId();

        void SetCorrelationId(string correlationId);
    }
}
