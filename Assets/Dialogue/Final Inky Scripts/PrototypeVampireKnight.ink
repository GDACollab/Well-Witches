

EXTERNAL StartQuest(questID)
EXTERNAL AdvanceQuest(questID)
EXTERNAL FinishQuest(questID)

VAR currentSpeaker = "VampireKnight"
// (quest id + "ID" for variable name)
// this should be the quest ID as in the "ID" field of the quest's scriptable object
VAR DefeatBossQuestID = "DefeatBossQuest"

// quest states (quest id + "State" for variable name)
VAR DefeatBossQuestState = "FIRST_ENCOUNTER"
-> Dialogue
=== Dialogue ===
{ DefeatBossQuestState :
    - "FIRST_ENCOUNTER": -> firstEncounter
    - "CONTINUED_ENCOUNTER": -> continuedEncounter
    - "PLAYER_DEFEATED": -> playerDefeated    
    - "BOSS_DEFEATED": -> bossDefeated
    - else: -> END
}

= firstEncounter
~currentSpeaker = "VampireKnight"
Well, Witches have found their way into my humble… WHAT?! #speaker:vampireKnight #sprite:neutral
Hand my Precious over, or suffer MY WRATH! #speaker:vampireKnight #sprite:angry
~currentSpeaker = "Player"
Huh?? #speaker:gatherer #sprite:confused
Lay off or be offed! #speaker:warden #sprite:angry
-> END

= continuedEncounter
~currentSpeaker = "VampireKnight"
GIVE IT BACK!!! #speaker:VampireKnight #sprite:angry
~currentSpeaker = "Player"
Can it, jerk! #speaker:warden #sprite:angry
Yeah, jerk! #speaker:gatherer #sprite:angry
-> END

= playerDefeated
~currentSpeaker = "Player"
Ergh… no… #speaker:warden #sprite:sad
Not… like this… #speaker:gatherer #sprite:surprised
~currentSpeaker = "VampireKnight"
Witches like you ought to know your place. #speaker:VampireKnight #sprite:neutral
-> END

= bossDefeated
~currentSpeaker = "VampireKnight"
AAAAAARRRRHHHHHHHGGGG!!! #speaker:VampireKnight #sprite:angry
~currentSpeaker = "Player"
It’s finished. #speaker:warden #sprite:excited
~currentSpeaker = "VampireKnight"
But, Precious… #speaker:VampireKnight #sprite:surprised
~currentSpeaker = "Player"
Precious? #speaker:gatherer #sprite:confused
~ FinishQuest("CollectHeadQuest")
-> END

-> END