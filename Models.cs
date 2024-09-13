using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatHistoryCaptureProcessor
{
    public class Models
    {
        public record CompletionsDiagnostics(int CompletionTokens, int PromptTokens, int TotalTokens, int AnswerDurationMilliseconds);
        public record Diagnostics(CompletionsDiagnostics AnswerDiagnostics, string ModelDeploymentName, long WorkflowDurationMilliseconds);
        public record SupportingContentRecord(string Title, string Content);
        public record ThoughtRecord(string Title, string Description);
        public record ResponseContext(string Profile, SupportingContentRecord[]? DataPoints, ThoughtRecord[] Thoughts, Guid MessageId, Guid ChatId, Diagnostics? Diagnostics);
    }
}