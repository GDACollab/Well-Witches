

EXTERNAL StartQuest(questID)
EXTERNAL AdvanceQuest(questID)
EXTERNAL FinishQuest(questID)

VAR currentSpeaker = "Dullahan"
BUFFER
// (quest id + "ID" for variable name)
// this should be the quest ID as in the "ID" field of the quest's scriptable object
VAR MultiStepGarlicQuestID = "MultiStepGarlicQuest"

// quest states (quest id + "State" for variable name)
VAR MultiStepGarlicQuestState = "REQUIREMENTS_NOT_MET"

-> Dialogue
=== Dialogue ===
{ MultiStepGarlicQuestState :
    - "REQUIREMENTS_NOT_MET": -> requirementsNotMet
    - "CAN_START": -> canStart
    - "IN_PROGRESS": -> inProgress
    - "CAN_FINISH": -> canFinish
    - "FINISHED": -> finished
    - else: -> END
}

= requirementsNotMet
~currentSpeaker = "Dullahan"
blah blah
I have something for you to do, but it seems like you are already helping someone else! Come back to me when you're avaliable and ready!
// here goes all the dialogue that should trigger ONLY if the player has yet to get the requirements for the current quest
    
-> END

= canStart
~currentSpeaker = "Dullahan"
// here goes all the dialogue that happens right before the player is prompted with dialogue
blah blah blah
blah blah
blah
Anyways will you get me 15 garlic knots in two steps for some reason?
//NOTE calling external functions breaks the inky viewer lmao, but everything else still works!
* [Yes]
    ~ StartQuest("MultiStepGarlicQuest")
    Great! Thank you so much in advance #sprite:happy
* [No]
    Damn.
- -> END

= inProgress
~ currentSpeaker = "Dullahan"
Did you get me those garlic knots? #sprite:neutral
~currentSpeaker = "Player"
Ah, sorry not yet!#speaker:gatherer #sprite:happy
-> END

= canFinish
~ currentSpeaker = "Dullahan"
Thank you so much!! #sprite:happy
~ FinishQuest("MultiStepGarlicQuest")

-> END

= finished
// put dialogue here that will play after the player has finished the quest
~ currentSpeaker = "Dullahan"
Thank you for getting those for me!
Miku was initially released in August 2007 for the VOCALOID2 engine and was the first member of the Character Vocal Series. She was the seventh VOCALOID overall, as well as the second VOCALOID2 vocal released to be released for the engine. 
Her voice is provided by the Japanese voice actress Saki Fujita (Fujita Saki).
-> END


-> END