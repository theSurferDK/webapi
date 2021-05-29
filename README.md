# If you have any problems with running app with Visual Studio, please install npm package Microsoft.Net.Compilers (more details there: https://improveandrepeat.com/2020/03/how-to-fix-could-not-find-binroslyncsc-exe/)

# Web API prototype project
This repository stores a REST API project. It is a small prototype that is similar to a real project, that will be relevant for the back-end position.

Below is a few tasks that we have prepared for you. We only expect you to spend around 3 hours on this – not days. The most important is for us to get insight into your understanding/thinking. We ask you to do the following:

1. Fork this repo to your own GitHub account and clone your fork to your machine. Run the application and get an overview over how it is working.
2. Review the code base and think about how it could be improved – especially the general structure and code patterns.
3. Choose to do some relevant changes, accompanied by inline comments explaining the change and why.
4. More overall thoughts/suggestions/ideas for the code or architecture should be added below in this README. This also gives you a chance to mention changes that you did not have time to do in the short timeframe.
5. Push and make a pull request to this repository with your changes.

----

#### Add general thoughts/suggestions/ideas here:

There isn't description of the service itself so assumptions are made.
The solution is divided into 5 projects which probably should only be 2: BookApp which is the frontend (presentation) and BookAPI which is the backend (logic and data).


Testing:
Altough not absolutly necessary to have testing of any kind it is recommended but does not exist.

Error codes and messages:
I added error code enums under Helper to get rid of magic numbers found in BookController and UserController and probably other places.
A good reason for this is found in BookController where the error messages "Error updating book" and "Error deleting book" both are erroneously assigned the error code "3" (possibly just testing the developer).
In generel error codes and messages could benefit from a central point to make them more managable and should have unique identifiers - BookController and UserController both used the value "4" resulting in two seperate enums to avoid collisions.

Error handling:
Often it is expected that everything goes well which is not necessary the case - changes should be validated.
For some reason APIException is giving the HttpStatusCode.BadRequest as ErrorCode which happens to also be the HttpStatus value.
This also benefits from the use of enums otherwise is might as well be set as the default value that can be overridden if need be.
It might have been done to maintain brevity in this test.

Book vs. User:
Methods like CreateUserBook and GetUserBooks should be owned and managed by the BookController and not the UserController.
Another example is GetMoreUserBooks which is a part of the IUserRepository.
There seems to be a bit confusion about defining sections - I suspect this may have done intentionally solely for this test.

Book vs. BookExtended:
The names and content suggest that they could simply inherit default properties and extend these as needed.

Var vs. types:
I see that variable declaration is often done using "var" instead of using the actual type of the object or return type.
This decreases readability since you then need to scan the rest of the line to see the type - you may even do a (simple) lookup to see the return type of a call.

Public vs. Private variables:
Although I have not checked all files the private variables seem to start with a upper case letter instead of the expected lower case letter which indicates a private variable.
