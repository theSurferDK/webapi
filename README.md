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

* GetUserBooks and CreateUserBook:

I have moved these methods to BookController with the paths /api/books/GetUserBooks?userId= and /api/books/CreateUserBook?userId= which makes more sense.
There is no problem is calling them using books.

The GetMoreUserBooks method does not makes a whole lot of sens to I changed the approach.

I have disabled GetMoreUserBooks (returns null) to make the output look as expected: a flat JSON array of books in the format "[ { book 1 }, { book 2 }, { book 3 } ]".

Earlier it returned an output in the format: "[ { book 1 [ { book 2, book 3} ] }, { book 2 [ { book 1, book 3} ] }, { book 3 [ { book 1, book 2 } ] } ] "


Paths vs. QueryStrings:

We could look into using URLs as "/api/books/GetBookById/B1E83BD4-6F22-4137-9033-6843F9E68DD8" instead of using querystrings.


Book vs. BookExtended:

I have created a new class in Models called "BookBase" which contains the base elements used by both Book and BookExtended.


Domain-models vs. DTOs:

I have made an example of using DTO to hide data from the outbound JSON.

