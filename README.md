# FinanceToolkitApi


## Process Notes

This is going to be a change in paradigm.  We are going to move away from the MVC pattern and go with minimal apis.
What does this mean?

- We are not going to use controllers.  We will use the Models.  But the concept of controllers is gone.
- We are not going to use try-catch-finally blocks for error handling.  
	- Instead of try-catch we are going to use the Serilog library to handle logging at a global level.
	- For the server-side calculators we will send all errors back to the users in the form of 
		- 400, 401, 500 and other appropiate errors.  The errors will be accompanied with a message that explains what actions the user can do to fix the problem.
- We will use the built in dependency injection for the services and repositories.

## Roadmap

The first phase of this will be the financial calculators.  These are basic services that provide financial calulations for simple, compound and other interest calculations and cash flows.

