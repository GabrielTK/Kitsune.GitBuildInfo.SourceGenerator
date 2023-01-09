namespace GitBuildInfo.SourceGenerator;

public class GeneratorOptions
{
    public static readonly DiagnosticDescriptor ValidationWarning = new(
        "GITINFO000",
        "GitBuildInfoSourceGeneratorConfigurationValidationWarning",
        "{0} should not be an empty string",
        "Functionality",
        DiagnosticSeverity.Warning,
        true);

    public string RootNamespace { get; set; }

    public string ClassName { get; set; }

    //public bool IsGeneric { get; set; }



    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Validate(GeneratorExecutionContext context)
    {
        if (string.IsNullOrEmpty(this.ClassName))
        {
            context.ReportDiagnostic(
                Diagnostic.Create(
                    ValidationWarning,
                    null,
                    nameof(this.ClassName)));
            throw new InvalidOperationException(
                string.Format(
                    ValidationWarning.MessageFormat.ToString(),
                    nameof(this.ClassName)));
        }
        if (string.IsNullOrEmpty(this.RootNamespace))
        {
            context.ReportDiagnostic(
                Diagnostic.Create(
                    ValidationWarning,
                    null,
                    nameof(this.ClassName)));
            throw new InvalidOperationException(
                string.Format(
                    ValidationWarning.MessageFormat.ToString(),
                    nameof(this.ClassName)));
        }
    }
}
