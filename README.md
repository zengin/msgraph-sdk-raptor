# Microsoft Graph SDK Raptor

**Raptor** in it's most basic description is a **compile** and **E2E functional testing tool**. The tool has two primary objectives.

- Compile Microsoft Graph generated snippets (**CSharp**, **Java**, **JavaScript**, **Objective C**)
- Provide E2E functional tests – this approach is not the usual conventional approach through XUnit. For more information checkout the [Design Spec](msgraph-sdk-raptor-design-spec.md)

Feature| .Net | Java | JavaScript | Objective C |  
|--|--|--|--|--|
 Compile                           |✓| | | |
 GET requests execution            | | | | |
 Verify responses                  | | | | |

## Run Raptor Console App
To run **Raptor**, you need to setup the snippets directory path for both **v1.0** and **beta**. The settings for this can be found in the [msgraph-sdk-raptor-compiler-lib/appsettings.json](msgraph-sdk-raptor-compiler-lib/appsettings.json)

```
{
  "Azure": {
    "ClientId": "Enter Client ID",
    "Scopes": "Enter Microsoft Graph Scopes Here"
  },
  "SnippetsDirectory": {
    /* get snippets from
    https://github.com/microsoftgraph/microsoft-graph-docs/tree/master/api-reference/v1.0/includes/snippets/csharp
    */
    "CSharpPath-v1.0": "Enter Path Here",
    /* get snippets from 
    https://github.com/microsoftgraph/microsoft-graph-docs/tree/master/api-reference/beta/includes/snippets/csharp 
    */
    "CSharpPath-beta": "Enter Path Here",
    "JavaPath-v1.0": "Enter Path Here",
    "JavaPath-beta": "Enter Path Here",
    "JavaScriptPath-v1.0": "Enter Path Here",
    "JavaScriptPath-beta": "Enter Path Here",
    "ObjCPath-v1.0": "Enter Path Here",
    "ObjCPath-beta": "Enter Path Here"
  },
  "ConnectionStrings": {
    "Raptor": "Server=Server;Database=Raptor;User Id=user;Password=pwd;"
  }
}
```
## Setup Raptor Dashboard

The Raptor dashboard displays the results of a compilation cycle and is visible from the screenshot below. The information displayed include total snippets compiled, total errors found in a given compilation cycle and the total number of snippets that compiled without any errors.

![Dashboard Screenshot](msgraph-sdk-raptor-dashboard/wwwroot/dist/img/raptor-dashboard-screenshot.png)

To setup the dashboard on your local PC using visual studio;

- Install SQL Server.
- Use the [raptor-scripts.sql](raptor-scripts.sql) to create the tables and views required by the dashboard.
- Update the [appsettings.json](msgraph-sdk-raptor-dashboard/appsettings.json) file with your SQL Server user credentials.

```
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "Raptor": "Server=ServerName;Database=Raptor;User Id=user;Password=pwd;"
  }
}
```

## Copyright and license

Copyright (c) Microsoft Corporation. All Rights Reserved. Licensed under the MIT [license](LICENSE).

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/). For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.