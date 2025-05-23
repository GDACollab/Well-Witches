
EXTERNAL ShowAbilityUI()
VAR currentSpeaker = "Hex"

-> Dialogue
=== Dialogue ===
~currentSpeaker = "Hex"
Buh... (Would you like to change your abilities?) #sprite:confused
* [Yes]
    ~ ShowAbilityUI()
* [No]
- -> END