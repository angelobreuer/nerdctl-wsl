using System.Diagnostics;

var processStartInfo = new ProcessStartInfo("wsl.exe");

processStartInfo.ArgumentList.Add("/usr/bin/nerdctl");

foreach (var argument in args)
{
    var mappedArgument = argument;

    if (File.Exists(mappedArgument))
    {
        mappedArgument = Path.GetFullPath(mappedArgument);

        var originalFileRoot = Path.GetPathRoot(mappedArgument)!;
        var root = originalFileRoot.TrimEnd('\\').TrimEnd(':').ToLowerInvariant();
        var relativePath = mappedArgument[originalFileRoot.Length..].Trim('/').Trim('\\');

        mappedArgument = $"/mnt/{root}/{relativePath.Replace('\\', '/')}";
    }

    processStartInfo.ArgumentList.Add(mappedArgument);
}

Process.Start(processStartInfo)!.WaitForExit();