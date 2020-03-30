# Microsoft Graph Raptor Design Spec

## Introduction 
The purpose for this document is to capture and convey the specs necessary for enabling service integration on current **Microsoft Graph Libraries**. Multiple iteration from the team is expected to narrow down the best approach in tackling the problem.

## Background
Current Microsoft Graph libraries don’t implement E2E function test that hit the Microsoft Graph API and the once that are implemented are all disabled. Please note some of the current unit tests are using mocks. 

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
- Enable all the skipped test within the existing client libraries.
- Setup **demo tenant** account
- **Data Integrity** - Maintain demo tenant data state – basically implement this continuously across SDKs without degrading the test data.
- **Authentication Flows** 
    - Implement multiple authentication flows with an intent to test both delegated and application permissions.

## Strategy One
**Enable all the current disabled functional tests and implement ability to hit the service using a demo tenant as opposed to using mocks**

### This approach will focus on;
- Enabling all the disabled integration test on the test projects on the existing libraries. 
- Hit the service with each workload and expecting.

### Drawbacks
- The whole process of enabling the test is manual.
- Introduction of new features within the API means adding the equivalent tests manually
- Breaking changes also means changing the test manually
- Service interruptions means the test will fail
- Data within the demo tenant will inevitably degrade over time.

## Strategy Two
**Implement test by compiling generated code snippets**

### The approach will focus on;
- Compiling generated code snippets as another way for testing contrary to the first approach.
- Use of a compiler to achieve this. Roslyn seem to be a good option
- Predetermine execution order given that some requests are dependent on a sequence and verify responses are as expected.
- Reporting failures - all 4XX 
- Reporting warnings – all 5XX with an exception of 501- Not Implemented

### Consideration
- Snippets Generator is dependent on docs and not the service
- MSAL breaking changes

### Drawbacks
- Roslyn only supports C# and VB. This means other languages (JavaScript, Objective C, Java) may use a different approach
- MSAL breaking changes

### Phases
- Create a Console App that compiles all generated .NET code snippets against specific versions of **Microsoft.Graph.Core** and **Microsoft.Graph** and reports errors. Allow pointing to folder of snippets.
- Provide command line switch to enable execution of all GET requests and report errors. Pass auth credentials via command line. (Username/password and/or client credentials). Report **4XX** as failures but **5XX** as warnings (except for 501 Not implemented).
- Build infrastructure to provision well known demo tenant and execute all requests against tenant.
- Verify responses are as expected. (We need a way of determining the test execution order)
