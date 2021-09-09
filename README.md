# WordChallengeBP

## Gary Taylor

# Overview
A program to solve a word puzzle, in which the user chooses:
-	A start word i.e. same
-	An end word i.e. cost

...and the program finds a list of words that move from the start word to the end word, using the following rules:
-	Only one letter can change between any two words
-	Each intermediate step is a word from the dictionary file (supplied)

For example:

SAME > CAME > CASE > CAST > COST

# Launch / Usage Notes
-	Build the Solution in VisualStudio
-	Open Windows Command: Cmd.exe
- 	Navigate to the folder containing WordChallenge.exe
-	Type the command using the format: WordChallenge "startWord" "targetWord" "dictionaryFilePath" "outputFilePath"
-	The solution to the puzzle, written to the supplied "outputFilePath", will be a text file containing a comma-separated list of words
-	Errors will be notified to the console window, in which case the "outputFilePath" file will be empty
-	For serurity reasons, if "outputFilePath" exists when execution begins, it will not be overwritten

# Design Notes
-	It's interesting to note that the requirements don't specify that the solution should find the SHORTEST path between the words
-	In this case the IO is file-based, but the Interfaces are designed to be device-independent
-	A simple in-memory (non-persisted), class-based design is used as the word-cache. For larger data-sources, other solutions should be considered
-	For performance reasons, the dictionary is filtered based on the word-length

# ChallengeSolver Algorithm
-	Works from both ends of word-path simultaneously, in an attempt to narrow down the number of potential paths in each search
-	Start and Target words form a word-pair and each word-pair will attempt to populate the remaining word-pairs to complete the path between them
-	Searches are prioritised starting with word-pairs with the most matching characters
-	Searches are prioritised for Words that have not been used previously
-	Searches are prioritised for changes that involve characters that aren't matched already

# Assumptions
- 	The solution will run under Windows (the target architecture was not specified)
-	The user is familiar with Visual Studio and Windows CLI
-	Letter changes do not include character additions/subtractions
- 	The start and end words don’t need to exist in the supplied dictionary
-	Output files should not overwrite existing files
-	Output format will be CSV
- 	Filtered/Processed data is not persisted between invocations
-	The contents of the Dictionary are assumed to be valid, so are not checked on loading
-	Some sensible (or perhaps over-generous) limitations have been placed on the total number of word-path searches to attempt, and the maximum word-depth down any single path

# Out of Scope (Time/context related limitations)
- 	Usually, a production Solution would be split into multiple projects for Model, Services, Presentation etc. but the required functionality and integrations for this task are very limited, so I don’t feel it would add much benefit
-	Logging
-	Xml comments 
-	Checking input or output file permissions
-	Test coverage is very limited. Tests are there to demonstrate knowledge of testing tools and techniques, not cover the entire solution in this case
-	TDD for the reason above
-	All actions are synchronous. In a console app with no additional functionality, this makes perfect sense, in most real-word scenarios IO will usually be async
-	Error messages are magic strings. In a production scenario, these may be centralised or localised for multi-language support. This eases message management and helps make unit testing more robust
-	Instrumentation / Analytics / Performance metrics
-	A distribution package or Installer
-	Usage help to the user

# Potential Enhancements
-	Formal Caching (eg. Redis)
-	Feedback via the console 
-	The solver algorithm could be tweaked to ensure that the solution delivers the SHORTEST (or even the LONGEST) path between the start and end words

