EXTERNAL StartQuest(questID)
EXTERNAL AdvanceQuest(questID)
EXTERNAL FinishQuest(questID)


VAR currentSpeaker = "Philip"
VAR CollectFishQuestID = "CollectFishQuest"
VAR CollectFishQuestState = "REQUIREMENTS_NOT_MET"

-> Dialogue

=== Dialogue ===
{ CollectFishQuestState :
     - "REQUIREMENTS_NOT_MET": -> requirementsNotMet
    - "CAN_START": -> canStart
    - "IN_PROGRESS": -> inProgress
    - "CAN_FINISH": -> canFinish
    - "FINISHED": -> finished
    - else: -> END
}


= requirementsNotMet
~currentSpeaker = "Philip"
Come back later. I'm currently eating. You should really try this plant, it’s yummy!
(It's probably poison. It'd be best to avoid this plant.) #sprite:neutral 
-> END


= canStart
~currentSpeaker = "Player"
We should fill up on water while— #speaker:gatherer #sprite:excited2
~currentSpeaker = "Philip"
Did somebody say my name? #sprite:thinking
~currentSpeaker = "Player"
Wah! Where did you come from? #speaker:gatherer #sprite:confused
~currentSpeaker = "Philip"
...the river? #sprite:thinking
Anyway, you should really try this plant. It’s yummy!
~currentSpeaker = "Player"
Erm, how do we know it's not poisonous? #speaker:gatherer #sprite:thinking
~currentSpeaker = "Philip"
You don’t. #sprite:glow
~currentSpeaker = "Player"
Aloe, step away from him. #speaker:warden #sprite:anger2
~currentSpeaker = "Player"
Relax, I’m sure he’s just a little confused. Hi there! My name’s Aloe, and this grumpy guy— #speaker:gatherer #sprite:excited1
~currentSpeaker = "Player"
Hey— #speaker:warden #sprite:neutral
~currentSpeaker = "Player"
—Is my best friend! We’re out here trying to find a cure for my cat, Hex. Who might you be? #speaker:gatherer #sprite:excited2
~currentSpeaker = "Philip"
Uhh, I’m Philip, Philip Ahn Water.
Best friend, huh. I lost my best friend... #sprite:bummed
~currentSpeaker = "Player"
Oh no that’s so sad! #speaker:gatherer #sprite:surprised
~currentSpeaker = "Player"
What happened to them? #speaker:warden #sprite:confused
~currentSpeaker = "Philip"
That stupid knight took her somewhere far away. 
I’d beat him up myself if I could. #sprite:anger
~currentSpeaker = "Player"
We'll help you reunite with them! #speaker:gatherer #sprite:excited2
~currentSpeaker = "Player"
Who are we looking for? #speaker:warden #sprite:confused 
~currentSpeaker = "Philip"
My lovely Al Gee... #sprite:happy
~currentSpeaker = "Player"
Excuse me, your what now? #speaker:warden #sprite:surprised
~currentSpeaker = "Philip"
Al Gee, she is a cute little fish. #sprite:glow
~currentSpeaker = "Player"
A fish, huh. Alright, we'll bring her back alive! #speaker:gatherer #sprite:excited1
~currentSpeaker = "Philip"
Actually, could you bring her back dead? #sprite:neutral 
~currentSpeaker = "Player"
Excuse me, what?! #speaker:gatherer #sprite:confused
~currentSpeaker = "Philip"
We were planning her death, so we could be undead together, forever, 
but then she was taken. #sprite:bummed
~currentSpeaker = "Player"
I'm so confused. #speaker:gatherer #sprite:confused
~currentSpeaker = "Player"
Whatever... #speaker:warden  #sprite:anger1
~currentSpeaker = "Philip"
Accept Quest? #sprite:glow
//NOTE calling external functions breaks the inky viewer lmao, but everything else still works!
* [Yes]
    ~ StartQuest("CollectFishQuest")
    Thank you so much! #sprite:happy
* [No]
- -> END

= inProgress
~currentSpeaker = "Player"
Uhh... Can you tell us more about Al Gee? #speaker:warden #sprite:confused
~currentSpeaker = "Philip"
Ah yes. She is a fish, she is small and... #sprite:happy
~currentSpeaker = "Player"
Yes, yes we know. 
We were wondering: how do you know Al Gee will come back...? #speaker:warden #sprite:confused
~currentSpeaker = "Philip"
Oh that, I gave her the same plant that brought me back to life. #sprite:shock
~currentSpeaker = "Player"
What plant? There's a plant that brings you back from the dead?!! #speaker:gatherer #sprite:confused
~currentSpeaker = "Philip"
Kind of, though you'd probably become something similar to me. #sprite:thinking
~currentSpeaker = "Player"
So undead... Yeah, no thanks. #speaker:warden #sprite:anger1
-> END

= canFinish
~currentSpeaker = "Philip"
Al Gee! Ah, my dearest friend! What happened to you? #sprite:thinking
(He stares at the now-dead Al Gee.)
Wait, you killed my friend?! #sprite:shock
~currentSpeaker = "Player"
You asked us to! #speaker:gatherer #sprite:confused
~currentSpeaker = "Philip"
Oh...I did? #sprite:thinking 
~currentSpeaker = "Player"
You know, so you two could live on together as ghosts? #speaker:warden  #sprite:anger1
~currentSpeaker = "Philip"
Ohhhh right, I remember now!
Did you hear that Al Gee? We’re gonna be together forever! #sprite:happy
(A tear rolls down Al Gee's eye. Actually, that might just be the water…)
~currentSpeaker = "Player"
So, anything for us? #speaker:warden  #sprite:confused
~currentSpeaker = "Philip"
Oh yes, of course, here:
These are the abilities "Bubble Barrier" and "Boggy Bullets". May they help you beat that stupid knight. #sprite:glow
~currentSpeaker = "Player"
Sweet! New abilities. #speaker:gatherer #sprite:excited2
~currentSpeaker = "Philip"
Thank you strangers! #sprite:happy
(Al Gee flops around happily.)
If you ever need anything just call for me!
~currentSpeaker = "Player"
We’re never seeing him again, are we? #speaker:warden  #sprite:neutral
~currentSpeaker = "Player"
Nope! #speaker:gatherer #sprite:neutral

~ FinishQuest("CollectFishQuest")

-> END

= finished
// put dialogue here that will play after the player has finished the quest
~currentSpeaker = "Philip"
Thanks for reuniting us! Now we get to eat these amazing plants together! By the way, still offering. #sprite:neutral
(Still looks poisonous)
-> END


-> END