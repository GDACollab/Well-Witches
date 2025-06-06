
EXTERNAL ShowAbilityUI()
VAR currentSpeaker = "Hex"

{~-> Dialogue|->Hint}

=== Dialogue ===
~currentSpeaker = "Hex"
Buh... (Would you like to change your abilities?) #sprite:confused
* [Yes]
    ~ ShowAbilityUI()
* [No]
- -> END

=== Hint ===
~currentSpeaker = "Hex"
Buh? (Have you explored the village yet? You might learn something from a bush or find something interesting.) #sprite:neutral
-> Dialogue