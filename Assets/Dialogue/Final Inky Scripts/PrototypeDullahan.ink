

EXTERNAL StartQuest(questID)
EXTERNAL AdvanceQuest(questID)
EXTERNAL FinishQuest(questID)

VAR currentSpeaker = "Dullahan"
// (quest id + "ID" for variable name)
// this should be the quest ID as in the "ID" field of the quest's scriptable object
VAR CollectHeadQuestID = "CollectHeadQuest"

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
Can't you see I'm busy?! Stop staring and go away!
-> END

= canStart
~currentSpeaker = "Dullahan"
// here goes all the dialogue that happens right before the player is prompted with dialogue
Come on…where is it?!
~currentSpeaker = "Player"
Hello? Sir? Is something wrong? #speaker:gatherer #sprite:neutral
~currentSpeaker = "Dullahan"
Huh? Oh, it's just some kids.
Go home, you can’t help me.
~currentSpeaker = "Player"
You heard him Aloe, let's go. #speaker:warden
Slow down V. #speaker:gatherer #sprite:sad
Well, we can’t help if we don’t know what’s wrong. #speaker:warden #sprite:neutral
~currentSpeaker = "Dullahan"
It’s my head. If you couldn’t tell, it's missing.
~currentSpeaker = "Player"
Oh…oh! Uhm, sorry I couldn’t tell. #speaker:gatherer
~currentSpeaker = "Dullahan"
It’s fine. It’s not like I was expecting much from a bunch of strangers.
~currentSpeaker = "Player"
That’s it, we’re leaving. We don’t need to help this guy Aloe. #speaker:Warden #sprite:angry
He’s probably just upset about his head. We could find it, couldn't we? #speaker:gatherer #sprite:neutral
(Sighs) #speaker:warden
Accept Quest?
//NOTE calling external functions breaks the inky viewer lmao, but everything else still works!
* [Yes]
    ~ StartQuest("CollectHeadQuest")
    Swell, Witches!
* [No]
- -> END

= inProgress
~currentSpeaker = "Dullahan"
Well, if you really want to help then you’ll need to keep your eye out for a pumpkin.
~currentSpeaker = "Player"
A pumpkin? That’s awfully vague- #speaker:warden #sprite:neutral
~currentSpeaker = "Dullahan"
Let me finish. It was carved by only the best artist in the land–Ciaran.
Anyway, it was carved specifically for me. I swear nothing else feels the same.
~currentSpeaker = "Player"
We understand, we’ll get it back for you! #speaker:gatherer #sprite:happy
Was it, by any chance, stolen by a knight in blood red armor? #speaker:warden #sprite:neutral
~currentSpeaker = "Dullahan"
Actually…it was. How did you know that?
~currentSpeaker = "Player"
You aren’t the first to have your things stolen by them. #speaker:warden
~currentSpeaker = "Dullahan"
Hmmm...
Well then, good luck. I suspect you’ll need it.
~currentSpeaker = "Player"
How generous of you. (Sarcasm) #speaker:warden
We’ll be sure to get it back for you! #speaker:gatherer #sprite:happy
~currentSpeaker = "Dullahan"
And be careful with it will you? The slightest dent or scratch and I’ll have your head instead.
~currentSpeaker = "Player"
Lovely… #speaker:warden #sprite:neutral
-> END

= canFinish
~currentSpeaker = "Player"
Dullahan!! We found it! #speaker:gatherer #sprite:happy
~currentSpeaker = "Dullahan"
Woah, woah! Be careful with that!
~currentSpeaker = "Player"
You could thank us, you know. #speaker:warden #sprite:angry
~currentSpeaker = "Dullahan"
I was getting to that part.
I didn’t believe you in the slightest. But, you did it. Thank you.
~currentSpeaker = "Player"
Aww of course! We’re always happy to help. #speaker:gatherer #sprite:happy
~currentSpeaker = "Dullahan"
This will be the only time I ask for your help.
Nevertheless, you have helped me greatly.
I bestow upon you, tiny witch, this powerful and ancient relic...
The Hellfire Booties!
~currentSpeaker = "Player"
Yippee!!! Thank you, Dullahan! #speaker:gatherer
How generous of you? #speaker:warden #sprite:neutral
~currentSpeaker = "Dullahan"
You're both welcome.
Don't expect this kindness from me again, though.
~ FinishQuest("CollectHeadQuest")

-> END

= finished
// put dialogue here that will play after the player has finished the quest
~currentSpeaker = "Dullahan"
Didn't I already give you those booties? Why are you still here?
-> END


-> END