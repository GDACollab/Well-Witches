
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
(You decide not to press further.)
-> END

= canFinish
~currentSpeaker = "Player"
Is this enough.
You hand her the garlic. #speaker:warden
~currentSpeaker = "Vampire"
Hmmmmm… enough? For now, perhaps. Thank you, you two
(She goes back to her tea, humming absentmindedly. You look at her
expectantly.)
Hmm, hmm, hm–? Are you expecting something? You look like you are.#sprite:happy
~currentSpeaker = "Player"
Wisteria, are you kidding m–. #speaker:warden #sprite:neutral
WHAT THEY MEAN TO SAY IS, do you have anything that could help us out? Anything at all? #speaker:gatherer #sprite:neutral
~currentSpeaker = "Vampire"
Hmmmm… well, since you asked so politely, I believe I could do something…
~currentSpeaker = "Player"
Thank you! #speaker:gatherer #sprite:happy
~currentSpeaker = "Vampire"
Ah, yes. Here it is.
(She takes out a steaming pot from seemingly nowhere, and adds a clove of garlic into the liquid inside. The steam that emerges is shaped like a heart.)
~currentSpeaker = "Player"
Erm… #speaker:gatherer
~currentSpeaker = "Vampire"
Oh, don’t worry, it’s not poisonous… probably… 78% sure. If it doesn’t kill you, it’ll… uh… well, we’ll see…
~currentSpeaker = "Player"
... Aloe, are you sure we should… ah, I see, you’re already drinking it… well then, I guess I should too… #speaker:warden
~currentSpeaker = "Vampire"
They are new abilities for you "Sharing is Caring" and "Soul Siphon".
“Well, will I be seeing you two again? Ah, maybe, maybe not, it doesn’t really matter. Do forgive me, my tea is getting cold, so I must attend to it this instant.”


~ FinishQuest("CollectGarlicQuest")

-> END

= finished
// put dialogue here that will play after the player has finished the quest
~currentSpeaker = "Vampire"
Thank you for getting my Garlic for me! Good Luck out there!
-> END


-> END