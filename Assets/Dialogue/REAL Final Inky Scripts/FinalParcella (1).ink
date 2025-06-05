// <b>Well Witches: Ghostly Mailperson (Parcella) </b>

//Feel free to contact Brendan (Naeggi on the Well Witches discord) if you have any questions or concerns!
EXTERNAL FinishedSpeaking()
-> Start

== Start ==


VAR currentSpeaker = "Ghost"
-> barks


//Programming department! Please check this code out! -> check_death_dialogue

= barks 
 // Short, reactive lines by Parcella. Catchphrases/lines you can rotate/randomize as Parcella's greeting to the players.
  ~currentSpeaker = "Parcella"
  {~Special Boo-livery for you!|This ghost has your post!|Signed, sealed, and spookily delivered!|Witch way to the mailbox? Right here!|Boo’s there? It’s me with your letter!|A boo-tiful delivery just for you!|Even in the afterlife I never <i>ghost</i> my deliveries!} #sprite:excited
 
 -> death_dialogue

- -> END


//I'm not too good at coding, so please help me fix/adjust this

VAR death_count = 0 //Number of deaths from Warden and Gatherer. Begins at 0

= death_dialogue 
//Not sure how the design department will set up things, but here's the code for how deaths would affect dialogue

{ death_count == 1: 
 -> first_death
}
{ death_count == 2: 
 -> backstory
}
{ death_count > 2: 
 -> repeated_deaths
}

- -> END

// Dialogue 
//First time a player dies + ability gain
 = first_death
 #sprite: neutral

	~currentSpeaker = "Player"
 “Who…are you?” #speaker:warden  #sprite: confused

	~currentSpeaker = "Parcella"
 “I have a special boo-livery for you! The name is Parcella, your friendly neighborhood ghost!”
 #sprite: excited

	~currentSpeaker = "Player"
 “A boo-livery?” #speaker:gatherer #sprite: surprised

	~currentSpeaker = "Parcella"
Uh-huh! When ya both fainted I found both of your belongings on the ground! I figured I’d bring them back to you.
 #sprite: pleased

	~currentSpeaker = "Player"
 What’s the catch here?” #speaker:warden

	~currentSpeaker = "Parcella"
 Nothing! 
 #sprite: excited

	~currentSpeaker = "Player"
 …? #speaker:warden

	~currentSpeaker = "Player"
 …?! #speaker:gatherer

	~currentSpeaker = "Parcella"
“Anytime you lose your belongings I’ll be sure to return them to you! Here’s a gift to celebrate our first time meeting!
#sprite: excited

 Here Vervain defy death, unlike me, with Death Defy! And Aloe, help others with Heal Force!

	~currentSpeaker = "Parcella"
 Good luck out there!
 
 #<i> They both felt hesitant about this ghost at first, but she doesn't seem to be an enemy.</i>
 ~ FinishedSpeaking()
 --> END
 
 
 = backstory
 //Gives some backstory to Parcella after second death (Death >1 and <3).
 
	~currentSpeaker = "Parcella"
 Welcome back Warden and Gatherer! #sprite: neutral

	~currentSpeaker = "Player"
 Parcella? I'm curious to how you became a ghost mailperson? #speaker:gatherer #sprite: thinking

	~currentSpeaker = "Parcella"
 I did it because delivering mail helps people! Just like it helps you both!
 #sprite: pleased
 
	~currentSpeaker = "Player"
 Wait, but how did you become a ghost in the first place? #speaker:warden 
 #sprite: confused

	~currentSpeaker = "Parcella"
 When I was human I contracted Soft Bones Fever. It’s a rare disease that makes your bones flexible, so my body was weak my whole human life.
 #sprite: thinking

	~currentSpeaker = "Player"
Oh Parcella! I’m sorry about that... #speaker:gatherer #sprite: confused

	~currentSpeaker = "Parcella"
 It’s okay! I became a ghost because I accidentally fell onto a pile of mail! 
 #sprite: excited

 #<i>Warden and Gatherer both laugh nervously</i>
 
 #<i>Parcella just smiles, ear to ear. They should probably not overthink it...</i>
 
 #<u> Repeated Death Dialogues </u>
 
 -> repeated_deaths
 
 = repeated_deaths
  //Used when Warden and Gatherer's death count is >2. Different lines spoken by Parcella that can be randomized.
  ~currentSpeaker = "Parcella"
  {~Lucky for you respawn shipping is free|Special express revive! Try dodging next time!|Back already? It seems the afterlife is ghosting you!|Saved you again! No need to thank me!} #sprite:pleased
  ~ FinishedSpeaking()
 - -> END
 




 



