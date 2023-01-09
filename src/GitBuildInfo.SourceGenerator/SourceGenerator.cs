using System.Diagnostics;

namespace GitBuildInfo.SourceGenerator;

/// <summary>
/// Source Generator for dumping git build information into a assembly level attribute on the compilation.
/// </summary>
[Generator]
public class SourceGenerator : ISourceGenerator
{
    /// <inheritdoc/>
    public void Initialize(GeneratorInitializationContext context)
    {
        // Source Generators do not need to fill this in.
#if DEBUG
            if (!Debugger.IsAttached)
            {
                Debugger.Launch();
            }
#endif
    }

    /// <inheritdoc/>
    public void Execute(GeneratorExecutionContext context)
    {
        if (context.Compilation is not CSharpCompilation compilation)
        {
            return;
        }

        _ = context.AnalyzerConfigOptions.GlobalOptions.TryGetValue("build_property.RootNamespace", out var rootNamespace);
        _ = context.AnalyzerConfigOptions.GlobalOptions.TryGetValue("build_property.GitBuildInfoClassName", out var className);
        //_ = context.AnalyzerConfigOptions.GlobalOptions.TryGetValue("build_property.GitBuildInfoIsGeneric", out var isGeneric);
        var gitHead = context.AdditionalFiles.First(text => text.Path.EndsWith("git_head.txt")).GetText()?.ToString();
        var commitHash = context.AdditionalFiles.First(text => text.Path.EndsWith("git_commit_hash.txt")).GetText()?.ToString();
        var gitBranch = context.AdditionalFiles.First(text => text.Path.EndsWith("git_branch.txt")).GetText()?.ToString();
        var gitDiff = context.AdditionalFiles.First(text => text.Path.EndsWith("git_dirty.txt")).GetText()?.ToString();

        var code = Generator.CreateAndGenerateCode(
            new GeneratorOptions
            {
                RootNamespace = rootNamespace,
                ClassName = className,
                //IsGeneric = Convert.ToBoolean(isGeneric)
            },
            new GitInfo
            {
                GitHead = gitHead!.Trim(Environment.NewLine.ToCharArray()),
                CommitHash = commitHash!.Trim(Environment.NewLine.ToCharArray()),
                GitBranch = gitBranch!.Trim(Environment.NewLine.ToCharArray()),
                GitDiff = gitDiff!.Trim(Environment.NewLine.ToCharArray()).Split(Environment.NewLine.ToCharArray()).Where(x=>!string.IsNullOrEmpty(x)).ToArray(),
            },
            context);

        context.AddSource(
            "GitAssemblyInfo.g.cs",
            SourceText.From(
                code,
                Encoding.UTF8));
    }
}
