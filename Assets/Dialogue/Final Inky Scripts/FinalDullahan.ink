

EXTERNAL StartQuest(questID)
EXTERNAL AdvanceQuest(questID)
EXTERNAL FinishQuest(questID)

VAR currentSpeaker = "Dullahan"
// (quest id + "ID" for variable name)
// this should be the quest ID as in the "ID" field of the quest's scriptable object
VAR CollectHeadQuestID = "DullahnMultiStepQuest"

// quest states (quest id + "State" for variable name)
VAR CollectHeadQuestState = "REQUIREMENTS_NOT_MET"
-> Dialogue
=== Dialogue ===
{ CollectHeadQuestState :
    - "REQUIREMENTS_NOT_MET": -> requirementsNotMet
    - "CAN_START": -> canStart
    - "IN_PROGRESS": -> inProgress
    - "CAN_FINISH": -> canFinish
    - "FINISHED": -> finished
    - else: -> END
}

= requirementsNotMet
// here goes all the dialogue that should trigger ONLY if the player has yet to get the requirements for the current quest
~currentSpeaker = "Dullahan"
Can't you see I'm busy?! Stop staring and go away! #sprite:headlessAnger
-> END

= canStart
~currentSpeaker = "Dullahan"
// here goes all the dialogue that happens right before the player is prompted with dialogue
Come on…where is it?! #sprite:headlessShocked
~currentSpeaker = "Player"
Hello? Sir? Is something wrong? #speaker:gatherer #sprite:surprised
~currentSpeaker = "Dullahan"
Huh? Oh, it's just some kids. #sprite:headlessThinking
Go home, you can’t help me. #sprite:headless
~currentSpeaker = "Player"
You heard him Aloe, let's go. #speaker:warden #sprite:anger1
Slow down V. #speaker:gatherer #sprite:anger
Well, we can’t help if we don’t know what’s wrong. #speaker:warden #sprite:neutral
~currentSpeaker = "Dullahan"
It’s my head. If you couldn’t tell, it's missing. #sprite:headless
~currentSpeaker = "Player"
Oh…oh! Uhm, sorry, I really couldn’t tell. #speaker:gatherer #sprite:confused
~currentSpeaker = "Dullahan"
It’s fine. It’s not like I was expecting much from a bunch of strangers. #sprite:headlessAnger
~currentSpeaker = "Player"
That’s it, we’re leaving. We don’t need to help this guy Aloe. #speaker:Warden #sprite:anger2
He’s probably just upset about his head. We could find it, couldn't we? #speaker:gatherer #sprite:neutral
(Sighs) #speaker:warden #sprite:neutral
~currentSpeaker = "Dullahan"
Accept Quest? #sprite:headless
//NOTE calling external functions breaks the inky viewer lmao, but everything else still works!
* [Yes]
    ~ StartQuest("CollectHeadQuest")
    Swell, Witches! #sprite:headlessExcited
* [No]
- -> END

= inProgress
~currentSpeaker = "Dullahan"
Well, if you really want to help then you’ll need to keep your eye out for a pumpkin. #sprite:headless
~currentSpeaker = "Player"
A pumpkin? That’s awfully vague- #speaker:warden #sprite:confused
~currentSpeaker = "Dullahan"
Let me finish. It was the finest piece by the best artist in the land. #sprite:headless
Anyway, it was carved specifically for me. I swear nothing else feels the same. #sprite:headlessThinking
~currentSpeaker = "Player"
We understand, we’ll get it back for you! #speaker:gatherer #sprite:excited1
Was it, by any chance, stolen by a knight in blood red armor? #speaker:warden #sprite:confused
~currentSpeaker = "Dullahan"
Actually…it was. How did you know that?  #sprite:headlessThinking
~currentSpeaker = "Player"
You aren’t the first to have your things stolen by them. #speaker:warden  #sprite:anger2
~currentSpeaker = "Dullahan"
Hmmm... #sprite:headlessThinking
Well then, good luck. I suspect you’ll need it. #sprite:headless
~currentSpeaker = "Player"
How generous of you. (Sarcasm) #speaker:warden #sprite:anger1
We’ll be sure to get it back for you! #speaker:gatherer #sprite:excited2
~currentSpeaker = "Dullahan"
And be careful with it will you? The slightest dent or scratch and I’ll have your head instead. #sprite:headlessAnger
~currentSpeaker = "Player"
Lovely… #speaker:warden #sprite:anger1
-> END

= canFinish
~currentSpeaker = "Player"
Dullahan!! We found it! #speaker:gatherer #sprite:excited2
~currentSpeaker = "Dullahan"
Woah, woah! Be careful with that! #sprite:headlessExcited
~currentSpeaker = "Player"
You could thank us, you know. #speaker:warden #sprite:angry
~currentSpeaker = "Dullahan"
I was getting to that part. #sprite:headAnger
I didn’t believe you in the slightest. But, you did it. Thank you. #sprite:headExcited
~currentSpeaker = "Player"
Aww of course! We’re always happy to help. #speaker:gatherer #sprite:excited1
~currentSpeaker = "Dullahan"
This will be the only time I ask for your help. #sprite:head
Nevertheless, you have helped me greatly. #sprite:headExcited
I bestow upon you, tiny witch, this powerful and ancient relic... #sprite:head
The Soles of the Damned! #sprite:headShocked
~currentSpeaker = "Player"
Yippee!!! Thank you, Dullahan! #speaker:gatherer
How generous of you? #speaker:warden #sprite:neutral
~currentSpeaker = "Dullahan"
You're both welcome. #sprite:headExcited
Don't expect this kindness from me again, though. #sprite:head
~ FinishQuest("CollectHeadQuest")

-> END

= finished
// put dialogue here that will play after the player has finished the quest
~currentSpeaker = "Dullahan"
Didn't I already give you those booties? Why are you still here? #sprite:headThinking
-> END


-> END