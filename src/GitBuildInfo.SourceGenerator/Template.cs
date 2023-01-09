using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Kitsune.GitBuildInfo.SourceGenerator
{
    internal class Template
    {
        public string Headdesc { get; } = "";

        /// <summary>
        /// Gets the git commit hash as formatted by git rev-parse.
        /// </summary>
        /// <value>
        /// The git commit hash as formatted by git rev-parse.
        /// </value>
        public string Commit { get; }

        /// <summary>
        /// Gets the git branch name as formatted by git name-rev.
        /// </summary>
        /// <value>
        /// The git branch name as formatted by git name-rev.
        /// </value>
        public string Branchname { get; }

        /// <summary>
        /// Gets a value indicating whether the branch is dirty or
        /// clean based upon the string constructed by git describe.
        /// </summary>
        /// <value>
        /// A value indicating whether the branch is dirty or
        /// clean based upon the string constructed by git describe.
        /// </value>
        public bool IsDirty => this.Headdesc.EndsWith("-dirty", StringComparison.Ordinal);

        /// <summary>
        /// Gets a value indicating whether the branch is the master
        /// branch or not based upon the string constructed by
        /// git name-rev. This also returns true if the branch is main as well.
        /// </summary>
        /// <value>
        /// A value indicating whether the branch is the master
        /// branch or not based upon the string constructed by
        /// git name-rev. This also returns true if the branch is main as well.
        /// </value>
        [Obsolete("Use GitInformation.IsMain instead. This will be removed in a future release. This is because most people using git are abandoning the use of master as the default branch name for the name of main. To prevent breakage I suggest you rename your default branch from master to main today.")]
        [ExcludeFromCodeCoverage]
        public bool IsMaster => this.Branchname.Equals("master", StringComparison.Ordinal) || this.IsMain;

        /// <summary>
        /// Gets a value indicating whether the branch is the main
        /// branch or not based upon the string constructed by
        /// git name-rev.
        /// </summary>
        /// <value>
        /// A value indicating whether the branch is the main
        /// branch or not based upon the string constructed by
        /// git name-rev.
        /// </value>
        public bool IsMain => this.Branchname.Equals("main", StringComparison.Ordinal);

        /// <summary>
        /// Gets a value indicating whether refs point to a stable tag release.
        /// </summary>
        /// <value>
        /// A value indicating whether refs point to a stable tag release.
        /// </value>
        public bool IsTag => this.Headdesc.StartsWith("tags/", StringComparison.Ordinal);
    }
}
