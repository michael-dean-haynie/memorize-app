using System;

namespace memorize_app;

public class TermEntity : MemorizeAppEntity
{
    public DateTimeOffset CreatedTimestamp { get; set; }
    public DateTimeOffset NextPromptTimestamp { get; set; }
}