

VAR currentSpeaker = "Player"
VAR DialogueState = "ALOE_DAMAGE"

-> Dialogue
=== Dialogue ===
{ DialogueState :
    - "ALOE_DAMAGE": -> aloeDamage
    - "SHARING_IS_CARING": -> sharingIsCaring
    - "SOLAR_FLARE": -> solarFlare
    - "BUBBLE_BARRIER": -> bubbleBarrier
    - "FLING_COMBAT": -> flingCombat
    - "SOLES_OF_THE_DAMNED": -> solesOfTheDamned
    - "ALOE_VERA": -> aloeVera
    - "12_OZ_ESPRESSO": -> 12OzEspresso
    - "VERV_DAMAGE": -> vervDamage
    - "SPELL_BURST": -> spellBurst
    - "GOURD_FORGE": -> gourdForge
    - "DEVASTATION_BEAM": -> devastationBeam
    - "RESURRECTION_REGALIA": -> resurrectionRegalia
    - "SOUL_SIPHON": -> soulSiphon
    - "BOGGY_BULLETS": -> boggyBullets
    - "PHILLIP_QUEST_FISH_FOUND": -> phillipQuestFishFound
    - "PHILLIP_QUEST_FISH_ESCAPE": -> phillipQuestFishEscape
    - "PHILLIP_QUEST_FISH_DEAD": -> phillipQuestFishDead
    - "WISTERIA_QUEST_FIRST_GARLIC": -> wisteriaQuestFirstGarlic
    - "WISTERIA_QUEST_EIGHTH_GARLIC": -> wisteriaQuestEighthGarlic
    - "WISTERIA_QUEST_FAILED": -> wisteriaQuestFailed
    - "DULLAHAN_QUEST_FIRST_DAMAGE": -> dullahanQuestFirstDamage
    - "DULLAHAN_QUEST_SECOND_DAMAGE": -> dullahanQuestSecondDamage
    - "DULLAHAN_QUEST_FAILED": -> dullahanQuestFailed
    - "DULLAHAN_QUEST_TIMER_END": -> dullahanQuestTimerEnd
    - "FIRST_INGREDIENT_FOUND": -> firstIngredientFound
    - "FIFTH_INGREDIENT_FOUND": -> fifthIngredientFound
    - "EIGTH_INGREDIENT_FOUND": -> eigthIngredientFound
    - else: -> END
}


// Aloe Combat Dialogue
=  aloeDamage   // Aloe Combat Dialogue
    ~currentSpeaker = "Player"
    { shuffle:
	- 	OWWW!!! #speaker:gatherer #sprite:sad
        ALOE! #speaker:warden #sprite:neutral

    -   OW! Vervain, help! #speaker:gatherer #sprite:sad
        Hold on! #speaker:warden #sprite:neutral

    -   Be careful! #speaker:warden #sprite:neutral

    -   Hey, that’s rude! #speaker:gatherer #sprite:angry

    -   AH- Stop that! #speaker:gatherer #sprite:sad
        Leave her alone! #speaker:warden #sprite:angry

    -   Watch ou- #speaker:warden #sprite:neutral
        OOWW! #speaker:gatherer #sprite:sad

    -   Hex, are you okay?! #speaker:gatherer #sprite:neutral
        "Buh" - Hex
        I swear this cat… #speaker:warden #sprite:neutral
    }
    -> END
    
=  sharingIsCaring
    ~currentSpeaker = "Player"
    { shuffle:
	- 	Have a heal! #speaker:gatherer #sprite:happy

    -   Take this! #speaker:gatherer #sprite:happy
        Don’t give me too much of that stuff, alright? #speaker:warden #sprite:happy

    -   I’ve got your back! #speaker:gatherer #sprite:happy
    }
    -> END

=  solarFlare
    ~currentSpeaker = "Player"
    { shuffle:
	- 	Hiya! #speaker:gatherer #sprite:happy
	
	-   Take this! #speaker:gatherer #sprite:happy
	
	-   Solar Flare! #speaker:gatherer #sprite:happy
    }
    -> END
    
=  bubbleBarrier
    ~currentSpeaker = "Player"
    { shuffle:
	- 	Bubble Bubble BUBBLE!!! #speaker:gatherer #sprite:happy
	    Careful where you point that thing!!! #speaker:warden #sprite:neutral
	    
    -   They can’t hit me now! #speaker:gatherer #sprite:happy
        But you can hit me! Be careful! #speaker:warden #sprite:angry

    -   See I can protect myself! #speaker:gatherer #sprite:happy
    }
    -> END
    
=  flingCombat
    ~currentSpeaker = "Player"
    { shuffle:
	- 	Thank you, Aloe. #speaker:warden #sprite:happy
        You’re welcome! See, I can do stuff! #speaker:gatherer #sprite:happy
        Don’t push it… #speaker:warden #sprite:neutral

    -   Be careful, Verv! #speaker:gatherer #sprite:happy
        Thanks, Aloe. #speaker:warden #sprite:happy
    }
    -> END
    
=  solesOfTheDamned
    ~currentSpeaker = "Player"
    { shuffle:
	- 	These boots are made for walking! #speaker:gatherer #sprite:happy

    -   Fireeeeeeee! #speaker:gatherer #sprite:happy

    -   Ahhhh! It's hot! It's hot! #speaker:gatherer #sprite:happy

    -   Take this…uh…fire trail! #speaker:gatherer #sprite:happy
    }
    -> END
    
=  aloeVera
    ~currentSpeaker = "Player"
    { shuffle:
	- 	All better now! #speaker:gatherer #sprite:happy

    -   That feels much better! #speaker:gatherer #sprite:happy

    -   I’m aliveeee! #speaker:gatherer #sprite:happy

    -   Yay, I have so much energy! #speaker:gatherer #sprite:happy

    }
    -> END
    
=  12OzEspresso
    ~currentSpeaker = "Player"
    { shuffle:
	- 	Weeeeeee!!!! #speaker:gatherer #sprite:happy

    -   Look at me go! #speaker:gatherer #sprite:happy

    -   Go go go!!! #speaker:gatherer #sprite:happy

    -   I’m off!! #speaker:gatherer #sprite:happy
    }
    -> END


// Vervain Combat Dialogue
=  vervDamage
    ~currentSpeaker = "Player"
    { shuffle:
	- 	Tch… #speaker:warden #sprite:angry

    -   Back off! #speaker:warden #sprite:angry

    -   Are you ok? #speaker:gatherer #sprite:sad
        Never better… #speaker:warden #sprite:neutral

    -   Are you- #speaker:gatherer #sprite:sad
        I’m fine. #speaker:warden #sprite:neutral

    -   You're gonna pay for that! #speaker:warden #sprite:angry

    -   Hmph. #speaker:warden #sprite:angry

    -   Why you! #speaker:warden #sprite:angry
    }
    -> END
    
=  spellBurst
    ~currentSpeaker = "Player"
    { shuffle:
    -   Take this! #speaker:warden #sprite:angry

    -   Don’t get too close! #speaker:warden #sprite:angry

    -   Try and dodge this! #speaker:warden #sprite:happy

    -   I’ve got you in my sights! #speaker:warden #sprite:neutral

    }
    -> END

=  gourdForge
    ~currentSpeaker = "Player"
    { shuffle:
    -   BURN! #speaker:warden #sprite:happy

    -   To cinders and ashes! #speaker:warden #sprite:happy

    -   BLAZE! #speaker:warden #sprite:happy
    }
    -> END
    
=  devastationBeam
    ~currentSpeaker = "Player"
    { shuffle:
    -   Big Beam ATTACK!!! #speaker:warden #sprite:angry

    -   Get back! #speaker:warden #sprite:angry

    -   Out of my way! #speaker:warden #sprite:angry

    -   Turn to dust! #speaker:warden #sprite:angry

    -   Not one more step! #speaker:warden #sprite:angry
    }
    -> END
    
=  resurrectionRegalia
    ~currentSpeaker = "Player"
    { shuffle:
    -   We’re not done yet! #speaker:warden #sprite:angry

    -   I won’t let it end like this! #speaker:warden #sprite:angry

    -   You can’t get rid of me that easily! #speaker:warden #sprite:angry

    -   Heh, back from the dead. #speaker:warden #sprite:neutral
    }
    -> END
    
=  soulSiphon
    ~currentSpeaker = "Player"
    { shuffle:
    -   I can keep going! #speaker:warden #sprite:angry

    -   Your soul is mine! #speaker:warden #sprite:happy

    -   I’ll put your soul to good use. #speaker:warden #sprite:neutral
    }
    -> END
    
=  boggyBullets
    ~currentSpeaker = "Player"
    { shuffle:
    -   Ha, slowpokes! #speaker:gatherer #sprite:happy

    -   Not so fast now! #speaker:warden #sprite:happy

    -   Slow your roll! #speaker:warden #sprite:happy
    }
    -> END
    
    
// Phillip Quest Related Dialogue
=  phillipQuestFishFound
    ~currentSpeaker = "Player"
    It wiggles! #speaker:gatherer #sprite:happy
    You know we have to kill it, right? #speaker:warden #sprite:neutral
    WHAT! #speaker:gatherer #sprite:sad
    -> END    
    
=  phillipQuestFishEscape
    ~currentSpeaker = "Player"
    { shuffle:
    -   It got away! #speaker:gatherer #sprite:neutral
        Don’t just stand there! Go after it! #speaker:warden #sprite:angry

    -   Run fishy run! #speaker:gatherer #sprite:happy
        ALOE, WE DON’T HAVE TIME FOR THIS! #speaker:warden #sprite:angry
        But…  #speaker:gatherer #sprite:neutral
        GET THE FISH! #speaker:warden #sprite:angry

    -   Whoa, it swims so fast! #speaker:gatherer #sprite:happy
        ALOE! #speaker:warden #sprite:angry
        Okay, okay, I’m going! #speaker:gatherer #sprite:sad
    }
    -> END
    
=  phillipQuestFishDead
    ~currentSpeaker = "Player"
    Rest in peace, fishy… #speaker:gatherer #sprite:sad
    Vervain, say something! #speaker:gatherer #sprite:angry
    … uh, sorry… fish. #speaker:warden #sprite:neutral
    See? That wasn’t so hard. #speaker:gatherer #sprite:happy
    -> END    
    
    
// Wisteria Quest Related Dialogue
=  wisteriaQuestFirstGarlic
    ~currentSpeaker = "Player"
    Is this it? #speaker:gatherer #sprite:neutral
    Yuck, that stuff reeks! Get it away from me! #speaker:warden #sprite:angry

    -> END    
    
=  wisteriaQuestEighthGarlic
    ~currentSpeaker = "Player"
    How many do we have left? #speaker:gatherer #sprite:neutral
    2 more, keep your eyes peeled. #speaker:warden #sprite:neutral
    Okie dokie! #speaker:gatherer #sprite:happy
    -> END    
    
=  wisteriaQuestFailed
    ~currentSpeaker = "Player"
    { shuffle:
    -   Did we fail? #speaker:gatherer #sprite:sad
        If I smell one more garlic clove… #speaker:warden #sprite:sad

    -   I’m gonna hurl. #speaker:warden #sprite:sad
        Too much garlic… #speaker:gatherer #sprite:sad

    -   There’s always next run. #speaker:gatherer #sprite:sad
        Maybe we can choose a different quest… #speaker:warden #sprite:sad

    -   That went well… #speaker:warden #sprite:sad
        At least we tried! #speaker:gatherer #sprite:neutral
    }
    -> END
    
    
// Dullahan Quest Related Dialogue
=  dullahanQuestFirstDamage
    ~currentSpeaker = "Player"
    Careful, Aloe! Don’t drop it! #speaker:warden #sprite:neutral
    Ow-! OH, ew ew ew, I felt something squishy! #speaker:gatherer #sprite:sad
    Just suck it up a little longer. #speaker:warden #sprite:neutral
    -> END    
    
=  dullahanQuestSecondDamage
    ~currentSpeaker = "Player"
    AH- #speaker:gatherer #sprite:neutral
    Don’t drop it! #speaker:warden #sprite:neutral
    I’m trying! #speaker:gatherer #sprite:angry
    -> END    
    
=  dullahanQuestFailed
    ~currentSpeaker = "Player"
    { shuffle:
    -   VERVAIN, STOP THEM! #speaker:gatherer #sprite:angry
        CAN’T YOU SEE THAT’S WHAT I'M DOING?! #speaker:warden #sprite:angry
        … I dropped it…. #speaker:gatherer #sprite:sad
        … #speaker:warden #sprite:neutral

    -   Um… #speaker:gatherer #sprite:sad
        Aloe… #speaker:warden #sprite:sad
        I’m sorryyyyy! #speaker:gatherer #sprite:sad

    -   You dropped it, didn’t you… #speaker:warden #sprite:neutral
        ... #speaker:gatherer #sprite:sad
    }
    -> END
    
=  dullahanQuestTimerEnd
    ~currentSpeaker = "Player"
    Is it over? #speaker:gatherer #sprite:neutral
    Phew, that’s all of them. #speaker:warden #sprite:happy
    -> END    
    
    
// Ingredient Dialogue

=  firstIngredientFound
    ~currentSpeaker = "Player"
     I found it! #speaker:gatherer #sprite:happy
     Great, now 9 more ingredients. #speaker:warden #sprite:neutral
     This would be a lot easier if *I* had the list- #speaker:gatherer #sprite:angry
     No. You lost it. Within the second you held it. #speaker:warden #sprite:neutral
    -> END
    
=  fifthIngredientFound
    ~currentSpeaker = "Player"
    { shuffle:
    -   Are we there yet? #speaker:gatherer #sprite:happy
        Nope. #speaker:warden #sprite:neutral

    -   Found one! How many more do we have? #speaker:gatherer #sprite:happy
        A lot more. #speaker:warden #sprite:neutral
        … UGGHHHHHHHH- #speaker:gatherer #sprite:sad
    }
    -> END
    
=  eigthIngredientFound
    ~currentSpeaker = "Player"
    { shuffle:
    -   PLEASE tell me this is all of it. #speaker:gatherer #sprite:sad
        Almost. Don’t worry. #speaker:warden #sprite:happy
    -   YAYYY! #speaker:gatherer #sprite:happy

    -   Are we there yet? #speaker:gatherer #sprite:happy
        We’ll get there when we get there! #speaker:warden #sprite:angry
    }
    -> END

== Error ==
    Something went wrong -> END