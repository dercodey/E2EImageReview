# .NET 8.0 Upgrade Plan

## Execution Steps

Execute steps below sequentially one by one in the order they are listed.

1. Validate that a .NET 8.0 SDK required for this upgrade is installed on the machine and if not, help to get it installed.
2. Ensure that the SDK version specified in global.json files is compatible with the .NET 8.0 upgrade.
3. Convert ImageWorkListAggregatorManager.csproj to SDK-style project
4. Convert ImageWorkListAggregatorManagerTests.csproj to SDK-style project
5. Convert ProxyManagerTests.csproj to SDK-style project
6. Convert MsqWorklistService.csproj to SDK-style project
7. Convert EmzImagingInteractionManager.csproj to SDK-style project
8. Convert ImageReviewManager.csproj to SDK-style project
9. Upgrade EndToEndImageReviewApp project to include net8.0-windows framework
10. Upgrade ImageWorkListAggregatorManager to .NET 8.0
11. Upgrade ImageWorkListAggregatorManagerTests to .NET 8.0
12. Upgrade ProxyManagerTests to .NET 8.0
13. Upgrade MsqWorklistService to .NET 8.0
14. Upgrade EmzImagingInteractionManager to .NET 8.0
15. Upgrade ImageReviewManager to .NET 8.0
16. Upgrade ImageWorklistModule to include net8.0-windows framework
17. Upgrade E2EImageReview.Package to .NET 8.0
18. Upgrade ImageWorklistModule.Test to .NET 8.0
19. Run unit tests to validate upgrade in the following projects:
    - ImageWorklistModule.Test
    - ImageWorkListAggregatorManagerTests
    - ProxyManagerTests

## Settings

This section contains settings and data used by execution steps.

### NuGet packages modifications across all projects

| Package Name               | Current Version | New Version | Description     |
|:------------------------------------------|:--------------:|:-----------:|:-----------------------------------------------|
| CoreWCF.ConfigurationManager   |                | 1.8.0      | Replacement for System.ServiceModel packages    |
| CoreWCF.Http       |           | 1.8.0    | Replacement for System.ServiceModel packages    |
| CoreWCF.NetTcp          |    | 1.8.0  | Replacement for System.ServiceModel packages    |
| CoreWCF.Primitives           |    | 1.8.0      | Replacement for System.ServiceModel packages    |
| CoreWCF.WebHttp      |          | 1.8.0      | Replacement for System.ServiceModel packages    |
| Microsoft.Windows.Compatibility            | 6.0.0         | 8.0.21     | Security vulnerability and recommended for .NET 8.0 |
| System.Collections.Immutable   | 6.0.0   | 8.0.0      | Recommended for .NET 8.0    |
| System.Reflection.Metadata    | 6.0.1      | 8.0.1      | Recommended for .NET 8.0|
| System.Runtime.CompilerServices.Unsafe     | 6.0.0         | 6.1.2      | Recommended for .NET 8.0  |
| System.ServiceModel.Http       | 4.9.0         |   | Replace with CoreWCF packages          |
| System.ServiceModel.Primitives         | 4.9.0         |            | Replace with CoreWCF packages           |
| System.Text.Encoding.CodePages     | 6.0.0      | 8.0.0      | Recommended for .NET 8.0          |

### Project upgrade details

#### EndToEndImageReviewApp modifications

Project properties changes:
  - Target frameworks should be changed from `netcoreapp3.1;net48` to `netcoreapp3.1;net48;net8.0-windows`

#### ImageWorkListAggregatorManager modifications

Project properties changes:
  - Project needs to be converted to SDK-style
  - Target framework should be changed from `.NETFramework,Version=v4.8` to `net8.0`

NuGet packages changes:
  - System.ServiceModel.Http should be removed and replaced with CoreWCF packages
  - System.ServiceModel.Primitives should be removed and replaced with CoreWCF packages
  - Remove System.IO (functionality included in framework)
  - Remove System.Runtime (functionality included in framework)
  - Remove System.Security.Cryptography.Algorithms (functionality included in framework)
  - Remove System.Security.Cryptography.Encoding (functionality included in framework)
  - Remove System.Security.Cryptography.Primitives (functionality included in framework)
  - Remove System.Security.Cryptography.X509Certificates (functionality included in framework)

Feature upgrades:
  - Migrate WCF services to CoreWCF

#### EmzImagingInteractionManager modifications

Project properties changes:
  - Project needs to be converted to SDK-style
  - Target framework should be changed from `.NETFramework,Version=v4.8` to `net8.0`

NuGet packages changes:
  - System.ServiceModel.Http should be removed and replaced with CoreWCF packages
  - System.ServiceModel.Primitives should be removed and replaced with CoreWCF packages
  - Remove System.IO (functionality included in framework)
  - Remove System.Runtime (functionality included in framework)
  - Remove System.Security.Cryptography.Algorithms (functionality included in framework)
  - Remove System.Security.Cryptography.Encoding (functionality included in framework)
  - Remove System.Security.Cryptography.Primitives (functionality included in framework)
  - Remove System.Security.Cryptography.X509Certificates (functionality included in framework)

Feature upgrades:
  - Migrate WCF services to CoreWCF

#### ImageReviewManager modifications

Project properties changes:
  - Project needs to be converted to SDK-style
  - Target framework should be changed from `.NETFramework,Version=v4.8` to `net8.0`

NuGet packages changes:
  - System.ServiceModel.Http should be removed and replaced with CoreWCF packages
  - System.ServiceModel.Primitives should be removed and replaced with CoreWCF packages
  - Remove System.IO (functionality included in framework)
  - Remove System.Runtime (functionality included in framework)
  - Remove System.Security.Cryptography.Algorithms (functionality included in framework)
  - Remove System.Security.Cryptography.Encoding (functionality included in framework)
  - Remove System.Security.Cryptography.Primitives (functionality included in framework)
  - Remove System.Security.Cryptography.X509Certificates (functionality included in framework)

Feature upgrades:
  - Migrate WCF services to CoreWCF

#### ImageWorklistModule modifications

Project properties changes:
  - Target frameworks should be changed from `netcoreapp3.1;net48` to `netcoreapp3.1;net48;net8.0-windows`

#### E2EImageReview.Package modifications

Project properties changes:
  - Target framework should be changed from `net451` to `net8.0`

#### ImageWorklistModule.Test modifications

Project properties changes:
  - Target framework should be changed from `netcoreapp3.1` to `net8.0`

#### ImageWorkListAggregatorManagerTests modifications

Project properties changes:
  - Project needs to be converted to SDK-style
- Target framework should be changed from `.NETFramework,Version=v4.8` to `net8.0`

#### ProxyManagerTests modifications

Project properties changes:
  - Project needs to be converted to SDK-style
  - Target framework should be changed from `.NETFramework,Version=v4.8` to `net8.0`

NuGet packages changes:
  - System.Collections.Immutable should be updated to 8.0.0
  - System.Reflection.Metadata should be updated to 8.0.1
  - System.Runtime.CompilerServices.Unsafe should be updated to 6.1.2
  - System.Text.Encoding.CodePages should be updated to 8.0.0
  - Remove System.AppContext (functionality included in framework)
  - Remove System.Buffers (functionality included in framework)
  - Remove System.Collections (functionality included in framework)
  - Remove System.Collections.Concurrent (functionality included in framework)
  - Remove System.Console (functionality included in framework)
  - Remove System.Diagnostics.Debug (functionality included in framework)
  - Remove System.Diagnostics.FileVersionInfo (functionality included in framework)
  - Remove System.Diagnostics.StackTrace (functionality included in framework)
- Remove System.Diagnostics.Tools (functionality included in framework)
  - Remove System.Dynamic.Runtime (functionality included in framework)
  - Remove System.Globalization (functionality included in framework)
  - Remove System.IO (functionality included in framework)
  - Remove System.IO.Compression (functionality included in framework)
  - Remove System.IO.FileSystem (functionality included in framework)
  - Remove System.IO.FileSystem.Primitives (functionality included in framework)
  - Remove System.Linq (functionality included in framework)
  - Remove System.Linq.Expressions (functionality included in framework)
  - Remove System.Memory (functionality included in framework)
  - Remove System.Numerics.Vectors (functionality included in framework)
  - Remove System.Reflection (functionality included in framework)
  - Remove System.Resources.ResourceManager (functionality included in framework)
  - Remove System.Runtime (functionality included in framework)
  - Remove System.Runtime.Extensions (functionality included in framework)
  - Remove System.Runtime.InteropServices (functionality included in framework)
  - Remove System.Runtime.Numerics (functionality included in framework)
  - Remove System.Security.Cryptography.Algorithms (functionality included in framework)
  - Remove System.Security.Cryptography.Encoding (functionality included in framework)
  - Remove System.Security.Cryptography.Primitives (functionality included in framework)
  - Remove System.Security.Cryptography.X509Certificates (functionality included in framework)
  - Remove System.Text.Encoding (functionality included in framework)
  - Remove System.Text.Encoding.Extensions (functionality included in framework)
  - Remove System.Threading (functionality included in framework)
  - Remove System.Threading.Tasks (functionality included in framework)
  - Remove System.Threading.Tasks.Extensions (functionality included in framework)
  - Remove System.Threading.Tasks.Parallel (functionality included in framework)
  - Remove System.Threading.Thread (functionality included in framework)
  - Remove System.ValueTuple (functionality included in framework)
  - Remove System.Xml.ReaderWriter (functionality included in framework)
  - Remove System.Xml.XDocument (functionality included in framework)
  - Remove System.Xml.XmlDocument (functionality included in framework)
  - Remove System.Xml.XPath (functionality included in framework)
  - Remove System.Xml.XPath.XDocument (functionality included in framework)

#### MsqWorklistService modifications

Project properties changes:
  - Project needs to be converted to SDK-style
  - Target framework should be changed from `.NETFramework,Version=v4.8` to `net8.0`

Feature upgrades:
  - Migrate WCF services to CoreWCF