# Microsoft Graph Raptor Design Spec

## Introduction 
Current Microsoft Graph libraries don’t implement E2E functionS test that call the Microsoft Graph API.  The existing integration tests that are implemented are all disabled to enable testing to occur during CI process. 

## Problem Statement
Current service integration tests are not enabled and therefore we don’t have proper ways for customers to validate their features E2E.

## Objective
Enable E2E functional test for all Microsoft Graph libraries during validation.

## Consideration 
- **Security**
    - Protect the access token during CI from malicious tests.
    - Implement the calls in phases and the first phase will only enable non-destructive calls only.
- **Blockers** - Ensure Microsoft Graph Service issues will not be a blocker.

## Requirements
- Setup **demo tenant** account
- **Data Integrity** - Maintain demo tenant data state – basically implement this continuously across SDKs without degrading the test data.
- **Authentication Flows** 
    - Implement multiple authentication flows with an intent to test both delegated and application permissions.

## Design Approach
Implement test by compiling generated code snippets

### The approach will focus on;
- Compiling generated code snippets as another way for testing contrary to the first approach.
- Use of a compiler to achieve this. Roslyn seem to be a good option
- Predetermine execution order given that some requests are dependent on a sequence and verify responses are as expected.
- Reporting failures - all 4XX 
- Reporting warnings – all 5XX with an exception of 501- Not Implemented

### Consideration
- Snippets Generator is dependent on docs and not the service
- MSAL breaking changes
- Roslyn only supports C# and VB. This means other languages (JavaScript, Objective C, Java) may use a different approach

### Implementation Phases
- Create a Console App that compiles all generated .NET code snippets against specific versions of **Microsoft.Graph.Core** and **Microsoft.Graph** and reports errors. Allow pointing to folder of snippets.
- Provide command line switch to enable execution of all GET requests and report errors. Pass auth credentials via command line. (Username/password and/or client credentials). Report **4XX** as failures but **5XX** as warnings (except for 501 Not implemented).
- Build infrastructure to provision well known demo tenant and execute all requests against tenant.
- Verify responses are as expected. (We need a way of determining the test execution order)
