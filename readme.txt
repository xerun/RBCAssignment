https://circleci.com/blog/continuous-integration-for-angular-applications/?utm_source=google&utm_medium=sem&utm_campaign=sem-google-dg--uscan-en-dsa-tROAS-auth-nb&utm_term=g_-_c__dsa_&utm_content=&gclid=CjwKCAjwrJ-hBhB7EiwAuyBVXeqoGcp9cRUajD22YC1-2AhbeOtaA93HqCnSKTcZjMbkOJ2D5JOygxoC4ZMQAvD_BwE


Angular Ngs Datatable 
yarn add @swimlane/ngx-datatable --save

https://swimlane.gitbook.io/ngx-datatable/readme/getting-started


xUnit, Shouldly (https://github.com/shouldly/shouldly), Moq (https://github.com/moq/moq4)



HttpClient is not designed to be mocked
HttpClient class doesn’t implement any interface you could use to create a fake substitute in your tests.
Ensure HttpClient is a singleton in your application.

We can create a tiny wrapper interface around the HttpClient and use it in your code instead of directly calling the HttpClient. This acts similar to how the Decorator pattern works.

Using EnsureSuccessStatusCode() instead of checking the status code with ShouldBe(), as it's more concise and already throws an exception if the status code is not successful
Using ShouldBeOfType() instead of ShouldBe(), to check the type of the response and the properties in a more readable way
Adding negative test scenarios, with a separate ApiUrls class to make it easier to change the URL in all