# WordChallengeBP

Assumptions
•	Letter changes do not include character additions/subtractions
•	The start and end words don’t need to exist in the supplied dictionary
•	Output files should not overwrite existing files
•	Filtered/Processed data is not persisted between invocations

Time/context related limitations
•	Usually, a production Solution would be split into multiple projects for Model, Services, Presentation etc. but the required functionality and integrations for this task are very limited, so I don’t feel it would add much benefit
•	Logging
•	Xml comments 
•	Test coverage is very limited. Tests are there to demonstrate knowledge of testing tools and techniques, not cover the entire solution in this case
•	TDD for the reason above
•	All actions are synchronous. In a console app with no additional functionality, this makes perfect sense, in most real-word scenarios IO will usually be async
•	Error messages are magic strings. In a production scenario, these may be centralised or localised for multi-language support. This eases message management and helps make unit testing more robust

Potential Enhancements
•	Formal Caching
•	Feedback via the console 

