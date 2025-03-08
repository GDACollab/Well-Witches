

EXTERNAL StartQuest(questID)
EXTERNAL AdvanceQuest(questID)
EXTERNAL FinishQuest(questID)

VAR currentSpeaker = "Vampire"
// (quest id + "ID" for variable name)
// this should be the quest ID as in the "ID" field of the quest's scriptable object
VAR CollectGarlicQuestID = "CollectGarlicQuest"

// quest states (quest id + "State" for variable name)
VAR CollectGarlicQuestState = "REQUIREMENTS_NOT_MET"
-> Dialogue
=== Dialogue ===
{ CollectGarlicQuestState :
    - "REQUIREMENTS_NOT_MET": -> requirementsNotMet
    - "CAN_START": -> canStart
    - "IN_PROGRESS": -> inProgress
    - "CAN_FINISH": -> canFinish
    - "FINISHED": -> finished
    - else: -> END
}

= requirementsNotMet
// here goes all the dialogue that should trigger ONLY if the player has yet to get the requirements for the current quest
I have something for you to do, but it seems like you are already helping someone else! Come back to me when you're avaliable and ready!
-> END

= canStart
~currentSpeaker = "Wysteria"
// here goes all the dialogue that happens right before the player is prompted with dialogue
Good morning… evening? Afternoon? Well, good day either way. What brings you two here?
~currentSpeaker = "Player"
“Do you need anything from us while we’re out?” #speaker:gatherer #sprite:happy
~currentSpeaker = "Wysteria"
“Hmmm…” 
The elegant lady pondered for a moment. And a moment.. And a moment… And-
~currentSpeaker = "Player"
“Could you please hurry up ma’am?” #speaker:warden #sprite:happy
~currentSpeaker = "Wysteria"
Ah! Hmm. Well, I could use more ingredients for my tea… I could fetch you a list, if you want.
~currentSpeaker = "Player"
Can you just tell us what you want? We’re in a bit of a rush. #speaker:gatherer
~currentSpeaker = "Wysteria"
Oh, hm? Uh, sure… lets see, I have enough nightshade and belladonna to last me a while, but… ah, right, I need some garlic.
~currentSpeaker = "Player"
Garlic?#speaker:gatherer
~currentSpeaker = "Wysteria"
Yeah, you haven’t heard of it? Small, white, bulb-y. It might be hard to come by this time of year, but I’m sure it’s out there… somewhere…
~currentSpeaker = "Player"
... We'll see what we can do.#speaker:warden
Accept Quest?
//NOTE calling external functions breaks the inky viewer lmao, but everything else still works!
* [Yes]
    ~ StartQuest("CollectGarlicQuest")
    Great! #sprite:happy
* [No]
- -> END

= inProgress
~currentSpeaker = "Player"
Could you tell us what these ingredients are for? #speaker:warden
~currentSpeaker = "Wysteria"
My tea, of course. What else would I use them for?
~currentSpeaker = "Player"
She sips her tea. The steam forms a skull shape. You have to ask. 
Aren’t vampires allergic to garlic? #speaker:gatherer
~currentSpeaker = "Wysteria"
Huh? Mmm, maybe some. It makes me feel sleepy. Or maybe that’s the
belladonna…
You decide not to press further. 
-> END

= canFinish
~currentSpeaker = "Vampire"
Thank you so much!! #sprite:happy
~ FinishQuest("CollectGarlicQuest")

-> END

= finished
// put dialogue here that will play after the player has finished the quest
~currentSpeaker = "Vampire"
Thank you for getting those for me!
[more dialogue to be added]
-> END


-> END