The following document, is a short text by my, the author of the code. I will shortly describe places, where i would have liked to improve the code, but dident.
None of the text below is in ANY way necessary in order to use the program, as it supplies the requested functionality, the concerns listed are only implementation-wise

- Seperation of code tasks:
	I wanted to improve the seperation of logic as much as posible, so that the commandParser, will almost only call other methods, acting as a mediator for the rest of the logic
	another example of this, would be the UserList, which is a static in the userclass instead of being an attribute in a class file, so it would belong to a singular "system". This could have been changed, if i had realised it earlier

- More readable method calls:
	I often found my self debating on whether i should convert a value in or before a method call, unfortunatly this, in my own opinion, was not implemented to a satisfactory degree

- Dictionaries
	In the third part of the assignment, i wanted implement the request of dictionaries, however due to lack of either insight or focus i was not able to, and instead obted for a switch case, 
	which naturally results in a linear runtime, instead of constant, through the .net hashtable it would be based on

- Segmented UI output
	I spend a long time trying to get an extension for UI to work, but failed to do so at an acceptable level, for this reason i instead hardcoded the console menu, with no flexibility, besides the privates feedbackline and warningline, which
	in of itself is also something i would have liked to implement in another way, possible by having my menu being based on an array or another collection, so that each segment, has its own index in the collection, this would also improve
	runtimes, as once a segment had been formatted, and if no changes was made to it, it would not need to load a number of computations and calls. 

This list in no is meant to be a cover for all of the lacking areas in my assignment, however, these are the ones i myself, felt.. dissapointed in not doing
Thank you for reading this, looking forward to discussing the pros and cons of my implementation, have a good day Thomas!
