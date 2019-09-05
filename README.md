---
page_type: sample
products:
- office-project
- office-365
languages:
- csharp
extensions:
  contentType: samples
  createdDate: 9/15/2016 1:39:55 PM
---
# Project-CSOM-My-Task-Checklist

The Project CSOM My Task Checklist sample uses C# and Project CSOM to demonstrate how to assignments assigned to the current user and submit status indicating that the tasks are complete.

## Using the app

1.	Add the Project CSOM client package [here](https://www.nuget.org/packages/Microsoft.SharePointOnline.CSOM/).
2.	Update the PWA site and click connect.
3.	Update the login/password to your PWA site.
4.	Run the app.

## Prerequisites
To use this code sample, you need the following:

* PWA Site (Project Online, Project Server 2013 or Project Server 2016)
* Visual Studio 2013 or later 
* Project CSOM client library.  It is available as a Nuget Package from [here](https://www.nuget.org/packages/Microsoft.SharePointOnline.CSOM/)
* One or more project stored in the PWA instance that use ECFs.


## How the sample affects your tenant data
This sample runs CSOM methods that reads all assignments in the PWA instance for the specified user. Tenant data will be changed if assignments are checked/unchecked and submitted.

## Additional resources

* [Client-side object model (CSOM) for Project 2013](https://aka.ms/project-csom-docs)

* [SharePoint (Project) CSOM Client library](https://www.nuget.org/packages/Microsoft.SharePointOnline.CSOM/)

## Copyright

Copyright (c) 2016 Microsoft. All rights reserved.




This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/). For more information, see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.
