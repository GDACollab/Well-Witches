<b>Well Witches: Ghostly Mailperson (Parcella) </b>

//Feel free to contact Brendan ("Naeggi" on the Well Witches discord) if you have any questions or concerns!

-> Start

== Start ==


VAR currentSpeaker = "Ghost"


-> GhostDialogue
=== GhostDialogue ===
{ GhostDialogue :
    - "BARKS": -> barks
	- "FIRST_DEATH": -> first_death
	- "MEET_HEX": -> meet_hex
	- "DEATH_DIALOGUE": -> death_dialogue 
    - else: -> END
}


//Programming department! Please check this code out! -> check_death_dialogue

= barks 
    // Short, reactive lines by Parcella. Catchphrases/lines you can rotate/randomize as Parcella's greeting to the players.
    ~currentSpeaker = "Parcella"
	"Special Boo-livery for you!"  
	#sprite:excited

	~currentSpeaker = "Parcella"
    "This ghost has your post!"
    #sprite:excited

	~currentSpeaker = "Parcella"
    "Signed, sealed, and spookily delivered!"
    #sprite:excited

	~currentSpeaker = "Parcella"
    "Witch way to the mailbox? Right here!"
    #sprite:excited

	~currentSpeaker = "Parcella"
    "Boo’s there? It’s me with your letter!"
    #sprite:excited

	~currentSpeaker = "Parcella"
    "A boo-tiful delivery just for you!"
    #sprite:excited

	~currentSpeaker = "Parcella"
    "Even in the afterlife I never <i>ghost</i> my deliveries!"
    #sprite:excited
- -> END


//I'm not too good at coding, so please help me fix/adjust this

VAR death_count = 0 //Number of deaths from Warden and Gatherer. Begins at 0

= death_dialogue 
//Not sure how the design department will set up things, but here's the code for how deaths would affect dialogue
~ death_count += 1 //Adds +1 to the death_count

{ death_count == 1: 
    -> GhostDialogue.first_death 
}
{ death_count == 2: 
    -> GhostDialogue.backstory 
}
{ death_count > 2: 
    -> GhostDialogue.repeated_deaths 
}

- -> END

// Dialogue 
//First time a player dies + ability gain
    = first_death
    
    <u> First death dialogue </u>
    
    <i>Warden and Gatherer appear back in the hub. Their eyes are met with a cheerful ghost in front of them</i>
    #sprite: neutral

	~currentSpeaker = "Player"
    “Who…are you?” #speaker:warden  #sprite: confused

	~currentSpeaker = "Parcella"
    “I have a special boo-livery for you! The name is Parcella, your friendly neighborhood ghost!”
    #sprite: excited

	~currentSpeaker = "Player"
    “A boo-livery?” #speaker:gatherer #sprite: surprised

	~currentSpeaker = "Parcella"
    <i>Enthusiastically.</i> "Uh-huh! When ya both fainted I found both of your belongings on the ground! I figured I’d bring them back to you."
    #sprite: pleased

	~currentSpeaker = "Player"
    <i>Looks at Parcella suspiciously.</i> "What’s the catch here?” #speaker:warden

	~currentSpeaker = "Parcella"
    <i>Parcella smiles innocently.</i> "Nothing!" 
    #sprite: excited

	~currentSpeaker = "Player"
    "…?" #speaker:warden

	~currentSpeaker = "Player"
    "…?!" #speaker:gatherer

	~currentSpeaker = "Parcella"
   “Anytime you lose your belongings I’ll be sure to return them to you! Here’s a gift to celebrate our first time meeting!"
   #sprite: excited

        <i>Warden received Death Defy!</i>
        <i>Gatherer received Heal Force!</i>

	~currentSpeaker = "Parcella"
    "Good luck out there!"
    
    <i> They both felt hesitant about this ghost at first, but she doesn't seem to be an enemy.</i>
    --> END
    
    
    = backstory
    //Gives some backstory to Parcella after second death (Death >1 and <3).
    
	~currentSpeaker = "Parcella"
    "Welcome back Warden and Gatherer!" #sprite: neutral

	~currentSpeaker = "Player"
    "Parcella? I'm curious to how you became a ghost mailperson?" #speaker:gatherer #sprite: thinking

	~currentSpeaker = "Parcella"
    "I did it because delivering mail helps people! Just like it helps you both!"
    #sprite: pleased
	~currentSpeaker = "Player"
    "Wait, but how did you become a ghost in the first place?" #speaker:warden #sprite: confused

	~currentSpeaker = "Parcella"
    "When I was human I contracted Soft Bones Fever. It’s a rare disease that makes your bones flexible, so my body was weak my whole human life."
    #sprite: thinking

	~currentSpeaker = "Player"
   <i>Gatherer frowns at her story.</i> "Oh Parcella! I’m sorry about that..." #speaker:gatherer #sprite: confused

	~currentSpeaker = "Parcella"
    "It’s okay! I became a ghost because I accidentally fell onto a pile of mail!" <i>She giggles.</i>
    #sprite: excited

    <i>Warden and Gatherer both laugh nervously</i>
    
    <i>Parcella just smiles, ear to ear. They should probably not overthink it...</i>
    
    <u> Repeated Death Dialogues </u>
    
    -> repeated_deaths
    
    = repeated_deaths
        //Used when Warden and Gatherer's death count is >2. Different lines spoken by Parcella that can be randomized.
		~currentSpeaker = "Parcella"
        "Saved you again! No need to thank me!"
        #sprite: pleased

		~currentSpeaker = "Parcella"
        "Back already? It seems the afterlife is ghosting you!"
        #sprite: pleased

		~currentSpeaker = "Parcella"
        "Special express revive! Try dodging next time!"
         #sprite: excited

		~currentSpeaker = "Parcella"
        "Lucky for you respawn shipping is free!"
         #sprite: excited
    - -> END
  
    
//If players are allowed to have conversations with Parcella besides mandatory deaths, then here are some fun dialogues created that can be used for conversations
 = meet_hex
    <i> Hex sneezes </i>

~currentSpeaker = "Player"
"Oh no! Hex is dying!!" #speaker:gatherer #sprite: confused

~currentSpeaker = "Parcella"
"HE'S DYING?!" #sprite: surpised

~currentSpeaker = "Player"
"Hex will be fine, it’s a cold." #speaker:warden #sprite: neutral

<i>Gatherer & Pareclla hugging Hex who now suffocating (Parcella is by Gatherer, phasing through him) </i>

~currentSpeaker = "Player"
"NOOOO!" #speaker:gatherer  #sprite: confused

~currentSpeaker = "Parcella"
"OHH NOOOO!" #sprite: surpised

~currentSpeaker = "Player"
"You're both suffocating Hex." #speaker:warden #sprite: anger1

~currentSpeaker = "Player"
"NO! We’re going to kill Hex!!" #speaker:gatherer #sprite: confused

~currentSpeaker = "Parcella"
"HEX IS DYING AGAIN?!" #sprite: surpised

<i>Gatherer & Parcella panicking in the background as Warden sighs. </i>

<u> Extra dialogue 2 </u>

<i> A certain threshold of items has been collected. </i>

~currentSpeaker = "Parcella"
"You two got a lot of baggage don’t ya?" #sprite: anger

~currentSpeaker = "Player"
"Too heavy for you, softbones? Gonna crumble on us? #speaker:warden #sprite: neutral

~currentSpeaker = "Parcella"
HA! I’m not talking about your <i>insert general item names they’re collecting. </i>
#sprite: anger


<u> Extra dialogue 3 </u>

<i> Warden and Gatherer die and return to spawn </i>

~currentSpeaker = "Parcella"
Can’t carry dead weight can I now? <i>She says in her overly optimistic tone. </i> 
#sprite: neutral

~currentSpeaker = "Player"
I’m surprised you can carry anything, Lincoln Logs. #speaker:warden #sprite: neutral

~currentSpeaker = "Parcella"
And I’m surprised you didn’t go to hell! <i> She says cheerfully. </i>
 #sprite: excited



- -> END






    



